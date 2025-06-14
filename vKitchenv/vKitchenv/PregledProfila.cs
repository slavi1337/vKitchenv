using MySql.Data.MySqlClient;
using System.Data;

namespace vKitchenv
{
    public partial class PregledProfila : Form
    {
        private readonly Form parentForm;
        private readonly string currentUser;
        private readonly string targetUser;
        private bool pratimGa;

        public PregledProfila(Form parent, string me, string target)
        {
            InitializeComponent();
            parentForm = parent;
            currentUser = me;
            targetUser = target;

            button2.Click += (_, __) => { parentForm.Show(); Close(); };
            button1.Click += Button1_ToggleFollow_Click;

            UcitajProfil();
            UcitajRecepteTargeta();
        }

        private void UcitajProfil()
        {
            try
            {
                using var conn = DbHelper.GetConnection();

                // osnovni podaci + followeri
                using (var cmd = new MySqlCommand("sp_GetProfilKorisnika", conn)
                { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@p_user", targetUser);

                    using var r = cmd.ExecuteReader();
                    if (!r.Read())
                    { MessageBox.Show("Korisnik ne postoji"); Close(); return; }

                    label1.Text = r["KorisnicnoIme"].ToString();
                    label4.Text = r["Ime"].ToString();
                    label5.Text = r["Prezime"].ToString();
                    label8.Text = r["BrojFollowera"].ToString();
                    label10.Text = r["BrojPratim"].ToString();
                }

                // bre cepata
                using (var cmd = new MySqlCommand(
                    "SELECT BrojRecepata FROM vw_BrojRecepataAutora WHERE Autor=@u", conn))
                {
                    cmd.Parameters.AddWithValue("@u", targetUser);
                    object? o = cmd.ExecuteScalar();
                    label3.Text = (o == null) ? "0" : o.ToString();

                }

                using (var cmd = new MySqlCommand("sp_DaLiPratim", conn)
                { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@p_me", currentUser);
                    cmd.Parameters.AddWithValue("@p_target", targetUser);
                    var outP = new MySqlParameter("@p_pratim", MySqlDbType.Bit)
                    { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outP);
                    cmd.ExecuteNonQuery();
                    pratimGa = Convert.ToBoolean(outP.Value);
                }

                button1.Text = pratimGa ? "Otprati" : "Zaprati";
                button1.Enabled = currentUser != targetUser;   // ne mze sam sebe flw
            }
            catch (Exception ex)
            { MessageBox.Show("Greska aaaaaaaaaaaaaaaaa: " + ex.Message); }
        }

        private void Button1_ToggleFollow_Click(object? s, EventArgs e)
        {
            string proc = pratimGa ? "sp_OtpratiKorisnika" : "sp_PratiKorisnika";
            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand(proc, conn)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@p_me", currentUser);
                cmd.Parameters.AddWithValue("@p_target", targetUser);
                cmd.ExecuteNonQuery();

                pratimGa = !pratimGa;
                button1.Text = pratimGa ? "Otprati" : "Zaprati";
                int delta = pratimGa ? 1 : -1;
                label8.Text = (int.Parse(label8.Text) + delta).ToString();
            }
            catch (Exception ex)
            { MessageBox.Show("Greska: " + ex.Message); }
        }

        private void UcitajRecepteTargeta()
        {
            flowLayoutPanel1.Controls.Clear();
            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("sp_PrikaziRecepteAutora", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@p_autor", targetUser);

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var autorIzBaze = r.GetString("Autor_KorisnickoIme");

                    var kart = new ReceptKartica(
                        r.GetInt32("ReceptID"),
                        r.GetString("Naslov"),
                        autorIzBaze,
                        r.GetString("Opis"),
                        currentUser);

                    flowLayoutPanel1.Controls.Add(kart);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska kod ucitavanja recepata za autora: " + ex.Message);
            }
        }
    }
}
