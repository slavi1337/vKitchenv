using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using System.Text.Json;

namespace vKitchenv
{
    public partial class UrediRecept : Form
    {
        private readonly UserControl parentCard;
        private readonly int receptID;

        public event EventHandler? ReceptUredjen;   // za refresh

        public UrediRecept(UserControl parent, int id)
        {
            InitializeComponent();
            parentCard = parent;
            receptID = id;

            button1.Click += Button1_Sacuvaj_Click;
            button2.Click += (_, __) => { parentCard.Show(); Close(); };
            FormClosing += (_, __) => parentCard.Show();

            PopuniFormu();
        }

        private void PopuniFormu()
        {
            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("sp_GetReceptZaEdit", conn)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@p_receptID", receptID);

                using var da = new MySqlDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 1)
                {
                    var row = ds.Tables[0].Rows[0];
                    textBox1.Text = row["Naslov"].ToString();
                    textBox2.Text = row["Opis"].ToString();
                    textBox3.Text = row["UputeZaPripremu"].ToString();
                }

                dataGridView1.Rows.Clear();
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    dataGridView1.Rows.Add(
                        r["Namirnica"],
                        r["Kolicina"],
                        r["JedinicaID"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri dohvacanju: " + ex.Message);
            }
        }

        private void Button1_Sacuvaj_Click(object? sender, EventArgs e)
        {
            string naslov = textBox1.Text.Trim();
            string opis = textBox2.Text.Trim();
            string upute = textBox3.Text.Trim();

            if (string.IsNullOrWhiteSpace(naslov) ||
                string.IsNullOrWhiteSpace(opis) ||
                string.IsNullOrWhiteSpace(upute))
            {
                MessageBox.Show("Sva tri polja moraju biti popunjena");
                return;
            }

            string json;
            try
            {
                json = BuildJsonFromGrid();      // baca Exception ako je nesto prazno
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            using var conn = DbHelper.GetConnection();
            using var tr = conn.BeginTransaction();
            try
            {
                using (var cmd = new MySqlCommand("sp_UpdateRecept", conn, tr)
                { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@p_receptID", receptID);
                    cmd.Parameters.AddWithValue("@p_naslov", naslov);
                    cmd.Parameters.AddWithValue("@p_opis", opis);
                    cmd.Parameters.AddWithValue("@p_upute", upute);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new MySqlCommand("sp_DodajSastojkeJSON", conn, tr)
                { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@p_receptID", receptID);
                    cmd.Parameters.AddWithValue("@p_JSON", json);
                    cmd.ExecuteNonQuery();
                }

                tr.Commit();
                MessageBox.Show("Recept uspjesno ayuriran");
                parentCard.Show();
                Close();
                ReceptUredjen?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show("Greska pri cuvanju Recepta: " + ex.Message);
            }
        }

        //helper: JSON iz DataGridViewa
        private string BuildJsonFromGrid()
        {
            var list = new List<object>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                string nam = row.Cells[0].Value?.ToString() ?? "";
                string kol = row.Cells[1].Value?.ToString() ?? "";
                string jed = row.Cells[2].Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(nam) ||
                    string.IsNullOrWhiteSpace(kol) ||
                    string.IsNullOrWhiteSpace(jed))
                    throw new Exception("Svaki sastojak mora imati sva tri polja popunjena");

                if (!decimal.TryParse(kol, out var dec))
                    throw new Exception($"Neispravan broj za '{nam}'");

                list.Add(new object[] { nam,
                                        dec.ToString(CultureInfo.InvariantCulture),
                                        jed });
            }

            return JsonSerializer.Serialize(list);
        }
    }
}
