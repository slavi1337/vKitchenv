using MySql.Data.MySqlClient;
using System.Data;

namespace vKitchenv
{
    public partial class Form1 : Form
    {
        private readonly string username;
        private DataTable originalData = new();   // cuva pocetno stanje

        public Form1(string username)
        {
            this.username = username;
            InitializeComponent();

            button1.Click += Button1_Recepti_Click;
            button2.Click += Button2_Snimi_Click;

            this.Load += (_, __) => OsvjeziBrojNotifikacija();

        }

        public void OsvjeziBrojNotifikacija()
        {
            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand(
                    @"SELECT COUNT(*) 
                    FROM vw_NeprocitaneNotifikacije 
                   WHERE Primatelj_KorisnickoIme = @kor",
                    conn);
                cmd.Parameters.AddWithValue("@kor", username);

                int broj = Convert.ToInt32(cmd.ExecuteScalar());
                buttonNotifikacije.Text = $"Notifikacije ({broj})";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri dohvacanju broja notifikacija:\n" + ex.Message);
            }
        }

        private void PopuniNamirnice()
        {
            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("sp_PrikaziNamirniceKorisnika", conn)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@p_username", username);

                using var da = new MySqlDataAdapter(cmd);
                originalData.Clear();
                da.Fill(originalData);

                dataGridView1.DataSource = originalData.Copy();  //da se ne mijenja direktno
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri pristupu bazi:\n" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = $"Dobrodosao, {username}!";

            PopuniNamirnice();
            OsvjeziBrojNotifikacija();
        }

        private void Button1_Recepti_Click(object? s, EventArgs e)
        {
            Hide();
            OsvjeziBrojNotifikacija();
            PopuniNamirnice();
            new Recepti(this, username).Show();
        }

        private void Button2_Snimi_Click(object? s, EventArgs e)
        {
            OsvjeziBrojNotifikacija();
            if (dataGridView1.DataSource is not DataTable gridData) return;

            try
            {
                using var conn = DbHelper.GetConnection();
                using var tr = conn.BeginTransaction();

                foreach (DataRow r in gridData.Rows)
                {
                    if (r.RowState == DataRowState.Deleted) continue;  // jos ne brisem

                    string nam = r["Namirnica"].ToString() ?? "";
                    decimal kol = Convert.ToDecimal(r["Kolicina"]);
                    string jed = r["JedinicaID"].ToString() ?? "";

                    bool postojala =
                        originalData.AsEnumerable()
                                    .Any(o => o["Namirnica"].ToString() == nam);

                    using var cmd = new MySqlCommand
                    (
                        postojala ? "sp_UpdateNamirnicuKorisniku"
                                  : "sp_DodajNamirnicuKorisniku",
                        conn, tr
                    )
                    { CommandType = CommandType.StoredProcedure };

                    cmd.Parameters.AddWithValue("@p_user", username);
                    cmd.Parameters.AddWithValue("@p_namirnica", nam);
                    cmd.Parameters.AddWithValue("@p_kol", kol);
                    cmd.Parameters.AddWithValue("@p_jed", jed);
                    cmd.ExecuteNonQuery();
                }

                //sad brisanje
                var imenaUBazi = gridData.AsEnumerable()
                                         .Where(r => r.RowState != DataRowState.Deleted)
                                         .Select(r => r["Namirnica"].ToString())
                                         .ToHashSet();

                foreach (DataRow o in originalData.Rows)
                {
                    string nam = o["Namirnica"].ToString() ?? "";
                    if (!imenaUBazi.Contains(nam))
                    {
                        using var cmd = new MySqlCommand("sp_ObrisiNamirnicuKorisniku", conn, tr)
                        { CommandType = CommandType.StoredProcedure };
                        cmd.Parameters.AddWithValue("@p_user", username);
                        cmd.Parameters.AddWithValue("@p_namirnica", nam);
                        cmd.ExecuteNonQuery();
                    }
                }

                tr.Commit();
                MessageBox.Show("Stanje namirnica je sacuvano");

                /* up originalData poslije uspjesnog savea */
                originalData = gridData.Copy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri snimanju:\n" + ex.Message);
            }
            PopuniNamirnice();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OsvjeziBrojNotifikacija();
            PopuniNamirnice();
            try
            {
                string trg = textBox1.Text.Trim();
                if (string.IsNullOrWhiteSpace(trg)) { MessageBox.Show("Unesite korisnicko ime"); return; }

                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("SELECT COUNT(*) FROM vw_KorisnikBasic WHERE KorisnicnoIme=@u", conn);
                cmd.Parameters.AddWithValue("@u", trg);
                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                { MessageBox.Show("Korisnik nije pronadjen"); return; }

                Hide();
                new PregledProfila(this, username, trg).Show();
            }
            catch (Exception ex)
            { MessageBox.Show("Greska bbbbbbbbbbbbbbb: " + ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OsvjeziBrojNotifikacija();
            PopuniNamirnice();
            this.Hide();
            var profil = new PregledProfila(this, username, username);
            profil.Show();
        }

        private void buttonNotifikacije_Click(object sender, EventArgs e)
        {
            OsvjeziBrojNotifikacija();
            PopuniNamirnice();
            this.Hide();
            var f = new Notifikacije(this, username);
            f.Show();
        }
    }
}
