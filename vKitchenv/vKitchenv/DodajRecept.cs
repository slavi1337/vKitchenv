using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace vKitchenv
{
    public partial class DodajRecept : Form
    {
        private readonly Form parentForm;
        private readonly string username;

        public DodajRecept(Form parent, string korisnik)
        {
            InitializeComponent();
            parentForm = parent;
            username = korisnik;

            button1.Click += Button1_Dodaj_Click;
            button2.Click += (_, __) => { parentForm.Show(); Close(); };
        }

        private void Button1_Dodaj_Click(object? sender, EventArgs e)
        {
            string naslov = textBox1.Text.Trim();
            string opis = textBox2.Text.Trim();
            string upute = textBox3.Text.Trim();

            if (string.IsNullOrWhiteSpace(naslov) ||
                string.IsNullOrWhiteSpace(opis) ||
                string.IsNullOrWhiteSpace(upute))
            {
                //namirnice ne trebaju biti ispunjene jer je 1:0..n tako da se moze recept napraviti bez namirnica
                //pa da se one poslije dodaju sa "uredi" dugmetom
                MessageBox.Show("Sva tri polja moraju biti ispunjena");
                return;
            }

            // JSOn za sastojke
            var sb = new StringBuilder("[");
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                string nam = row.Cells[0].Value?.ToString() ?? "";
                string kol = row.Cells[1].Value?.ToString() ?? "";
                string jed = row.Cells[2].Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(nam) ||
                    string.IsNullOrWhiteSpace(kol) ||
                    string.IsNullOrWhiteSpace(jed))
                {
                    MessageBox.Show("Svaki sastojak mora imati sva tri polja popunjena");
                    return;
                }

                sb.Append($"[\"{nam}\",{kol},\"{jed}\"],");
            }
            if (sb[^1] == ',') sb.Length--;
            sb.Append(']');

            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("sp_DodajReceptSaNamirnicama", conn)
                { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@pAutor", username);
                cmd.Parameters.AddWithValue("@pNaslov", naslov);
                cmd.Parameters.AddWithValue("@pOpis", opis);
                cmd.Parameters.AddWithValue("@pUpute", upute);
                cmd.Parameters.AddWithValue("@pJSON", sb.ToString());

                cmd.ExecuteNonQuery();

                MessageBox.Show("Recept uspjesno dodan");
                parentForm.Show();
                Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Greska pri dodavanju recepta: " + ex.Message);
            }
        }

        private void DodajRecept_FormClosing(object sender, FormClosingEventArgs e)
            => parentForm.Show();
    }
}
