using MySql.Data.MySqlClient;
using System.Data;

namespace vKitchenv
{
    public partial class ReceptKartica : UserControl
    {
        private readonly string currentUser;
        public int ReceptID { get; }

        /* eventi koje slusa forma Recepti */
        public event EventHandler<int>? ProvjeriKliknut;
        public event EventHandler? ReceptUredjen;

        public ReceptKartica(int id, string naslov, string autor,
                             string opis, string currentUser)
        {
            InitializeComponent();

            this.ReceptID = id;
            this.currentUser = currentUser;

            label1.Text = naslov;
            label2.Text = $"Autor: {autor}";
            label3.Text = opis;

            bool jaSamAutor = (autor == currentUser);

            button3.Visible = jaSamAutor;   // Uredi
            button4.Visible = jaSamAutor;   // Obrisi

            button1.Click += (_, __) => ProvjeriKliknut?.Invoke(this, ReceptID);
            button2.Click += Button2_Napravi_Click;
            button3.Click += Button3_Uredi_Click;
            button4.Click += Button4_Obrisi_Click;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;

            checkBox1.Checked = DaLiJeOmiljen();
            UcitajSastojke();
            UcitajUpute();
        }

        private bool DaLiJeOmiljen()
        {
            using var conn = DbHelper.GetConnection();
            using var cmd = new MySqlCommand("sp_JeOmiljeniRecept", conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@p_korisnik", currentUser);
            cmd.Parameters.AddWithValue("@p_receptID", ReceptID);
            var outP = new MySqlParameter("@p_is", MySqlDbType.Bit)
            { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outP);
            cmd.ExecuteNonQuery();
            return Convert.ToBoolean(outP.Value);
        }

        private void CheckBox1_CheckedChanged(object? s, EventArgs e)
        {
            string proc = checkBox1.Checked ? "sp_DodajOmiljeniRecept"
                                            : "sp_UkloniOmiljeniRecept";

            using var conn = DbHelper.GetConnection();
            using var cmd = new MySqlCommand(proc, conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@p_korisnik", currentUser);
            cmd.Parameters.AddWithValue("@p_receptID", ReceptID);
            cmd.ExecuteNonQuery();
            ReceptUredjen?.Invoke(this, EventArgs.Empty);
        }

        private void Button2_Napravi_Click(object? s, EventArgs e)
        {
            if (MessageBox.Show("Napraviti recept?", "Potvrda",
                                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("sp_PripremiRecept", conn)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@pKorisnik", currentUser);
                cmd.Parameters.AddWithValue("@pReceptID", ReceptID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Recept napravljen i sacuvan u istoriju");
            }
            catch (MySqlException ex)
            { MessageBox.Show("Greska: " + ex.Message); }

            ReceptUredjen?.Invoke(this, EventArgs.Empty);
        }

        private void Button3_Uredi_Click(object? s, EventArgs e)
        {
            button3.Enabled = false;
            using var f = new UrediRecept(this, ReceptID);
            f.ShowDialog();
            button3.Enabled = true;
        }

        private void Button4_Obrisi_Click(object? s, EventArgs e)
        {
            if (MessageBox.Show("Obrisati recept?", "Potvrda",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("sp_ObrisiRecept", conn)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@p_receptID", ReceptID);
                cmd.Parameters.AddWithValue("@p_user", currentUser);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Recept obrisan");
                ReceptUredjen?.Invoke(this, EventArgs.Empty);
            }
            catch (MySqlException ex)
            { MessageBox.Show("Greska: " + ex.Message); }
        }

        private void UcitajSastojke()
        {
            listBox2.Items.Clear();
            using var conn = DbHelper.GetConnection();
            using var cmd = new MySqlCommand("sp_PrikaziSastojkeZaRecept", conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@p_receptID", ReceptID);
            using var r = cmd.ExecuteReader();
            while (r.Read())
                listBox2.Items.Add(
                    $"{r["NAMIRNICA_NamirnicaIme"]} - {r["Kolicina"]} {r["JedinicaID"]}");
        }

        private void UcitajUpute()
        {
            listBox1.Items.Clear();
            using var conn = DbHelper.GetConnection();
            using var cmd = new MySqlCommand("sp_PrikaziUputeZaRecept", conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@p_receptID", ReceptID);
            using var r = cmd.ExecuteReader();
            if (r.Read())
            {
                string u = r["UputeZaPripremu"].ToString() ?? "";
                foreach (var lin in u.Split('\n'))
                    if (!string.IsNullOrWhiteSpace(lin))
                        listBox1.Items.Add(lin.Trim());
            }
        }
    }
}
