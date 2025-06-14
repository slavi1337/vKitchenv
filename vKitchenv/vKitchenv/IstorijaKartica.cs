using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace vKitchenv
{
    public partial class IstorijaKartica : UserControl
    {
        public int ReceptID { get; }

        private readonly string korisnik;

        public IstorijaKartica(int id,
                               string naslov,
                               DateTime kada,
                               string autor,
                               string korisnik)
        {
            InitializeComponent();

            this.ReceptID = id;
            this.korisnik = korisnik;

            label1.Text = naslov;
            label2.Text = kada.ToString("dd.MM.yyyy HH:mm");
            label3.Text = $"Autor: {autor}";


            UcitajNamirnice();
            UcitajUpute();
        }

        private void UcitajNamirnice()
        {
            var sb = new StringBuilder();

            using var conn = DbHelper.GetConnection();
            using var cmd = new MySqlCommand("sp_PrikaziSastojkeZaRecept", conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@p_receptID", ReceptID);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                string nam = r.GetString("NAMIRNICA_NamirnicaIme");
                string kol = r["Kolicina"].ToString() ?? "0";
                string jed = r.GetString("JedinicaID");
                sb.AppendLine($"{nam}: {kol} {jed}");
            }
            textBox2.Text = sb.ToString();
        }

        private void UcitajUpute()
        {
            using var conn = DbHelper.GetConnection();
            using var cmd = new MySqlCommand("sp_PrikaziUputeZaRecept", conn)
            { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@p_receptID", ReceptID);

            using var r = cmd.ExecuteReader();
            if (r.Read())
                textBox1.Text = r["UputeZaPripremu"].ToString();
        }
    }
}
