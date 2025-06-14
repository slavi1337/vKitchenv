using MySql.Data.MySqlClient;
using System.Data;

namespace vKitchenv
{
    public partial class Recepti : Form
    {
        private readonly Form parentForm;
        private readonly string username;

        public Recepti(Form parentForm, string korisnik)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.username = korisnik;

            checkBox1.CheckedChanged += Filter_CheckedChanged;   // dostupni
            checkBox2.CheckedChanged += Filter_CheckedChanged;   // moji
            checkBox3.CheckedChanged += Filter_CheckedChanged;   // omiljeni

            button1.Click += Button1_DodajRecept_Click;

            button2.Click += (_, __) => { parentForm.Show(); Close(); };
        }

        private void Recepti_Load(object sender, EventArgs e)
        {
            UcitajRecepte();
        }

        private void Filter_CheckedChanged(object? s, EventArgs e) => UcitajRecepte();

        private void Button1_DodajRecept_Click(object? s, EventArgs e)
        {
            Hide();
            using var f = new DodajRecept(this, username);
            f.ShowDialog();
            Show();
            UcitajRecepte();
        }

        public void UcitajRecepte()
        {
            flowLayoutPanel1.Controls.Clear();

            // Ako je u textBox1 nesto uneseno, prvo se radi search na osnovu tog
            string pretraga = textBox1.Text.Trim();

            try
            {
                using var conn = DbHelper.GetConnection();

                MySqlCommand cmd;
                if (!string.IsNullOrEmpty(pretraga))
                {
                    cmd = new MySqlCommand("sp_PretraziReceptePoNaslovu", conn)
                    { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("@p_term", pretraga);
                }
                else
                {
                    cmd = new MySqlCommand("sp_FiltrirajRecepte", conn)
                    { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("@p_korisnik", username);
                    cmd.Parameters.AddWithValue("@p_dostupni_only", checkBox1.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@p_moji_only", checkBox2.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@p_omiljeni_only", checkBox3.Checked ? 1 : 0);
                }

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int receptID = reader.GetInt32("ReceptID");
                    string naslov = reader.GetString("Naslov");
                    string autor = reader.GetString("Autor_KorisnickoIme");
                    string opis = reader.IsDBNull(reader.GetOrdinal("Opis"))
                                         ? ""
                                         : reader.GetString("Opis");

                    var kartica = new ReceptKartica(receptID, naslov, autor, opis, username);
                    kartica.ProvjeriKliknut += Kartica_ProvjeriKliknut;
                    kartica.ReceptUredjen += (_, __) => UcitajRecepte();

                    flowLayoutPanel1.Controls.Add(kartica);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri dohvatanju recepata:\n" + ex.Message);
            }
        }

        private void Kartica_ProvjeriKliknut(object? s, int receptId)
        {
            listBox1.Items.Clear();
            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("sp_GetShoppingList", conn)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@pKorisnik", username);
                cmd.Parameters.AddWithValue("@pReceptID", receptId);

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string nam = r.GetString("Namirnica");
                    string ned = r["Nedostaje"].ToString() ?? "0";
                    string jed = r.GetString("BaznaJed");
                    listBox1.Items.Add($"{nam}: {ned} {jed}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri provjeri: " + ex.Message);
            }
        }

        private void Recepti_FormClosing(object? s, FormClosingEventArgs e) => parentForm.Show();

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var f = new IstorijaRecepata(this, username);
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UcitajRecepte();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            UcitajRecepte();
        }
    }
}
