using MySql.Data.MySqlClient;
using System.Data;

namespace vKitchenv
{
    public partial class IstorijaRecepata : Form
    {
        private readonly Form parentForm;
        private readonly string username;

        public IstorijaRecepata(Form parent, string user)
        {
            InitializeComponent();
            parentForm = parent;
            username = user;

            flowLayoutPanel1.AutoScroll = true;

            button1.Click += (_, __) => { parentForm.Show(); Close(); };
            FormClosing += (_, __) => parentForm.Show();

            UcitajIstoriju();
        }

        private void UcitajIstoriju()
        {
            flowLayoutPanel1.Controls.Clear();

            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("sp_PrikaziIstorijuRecepata", conn)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@p_user", username);

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var kart = new IstorijaKartica(
                        r.GetInt32("ReceptID"),
                        r.GetString("Naslov"),
                        r.GetDateTime("DatumIVrijemePravljenja"),
                        r.GetString("Autor_KorisnickoIme"),
                        username);

                    flowLayoutPanel1.Controls.Add(kart);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri dohvacanju istorije:\n" + ex.Message);
            }
        }
    }
}
