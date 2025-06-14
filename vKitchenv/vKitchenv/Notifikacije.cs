using MySql.Data.MySqlClient;

namespace vKitchenv
{
    public partial class Notifikacije : Form
    {
        private readonly Form parentForm;
        private readonly string username;

        public Notifikacije(Form parent, string user)
        {
            InitializeComponent();
            parentForm = parent;
            username = user;

            this.Load += Notifikacije_Load;

            this.FormClosing += (_, __) => parentForm.Show();

            buttonNazad.Click += (_, __) => Close();

            dataGridViewNotifikacije.CellValueChanged += DataGridViewNotifikacije_CellValueChanged;
            // Da checkbox reacta odmah kad se klikne
            dataGridViewNotifikacije.CurrentCellDirtyStateChanged += DataGridViewNotifikacije_CurrentCellDirtyStateChanged;

            // Dvostrukim klikom open profil autora
            dataGridViewNotifikacije.CellDoubleClick += DataGridViewNotifikacije_CellDoubleClick;
        }

        private void Notifikacije_Load(object? sender, EventArgs e)
        {
            UcitajSveNotifikacije();
        }

        private void UcitajSveNotifikacije()
        {
            dataGridViewNotifikacije.Rows.Clear();
            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand("sp_PrikaziSveNotifikacijeZaKorisnika", conn)
                { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@p_korisnik", username);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int notifID = reader.GetInt32("ObavjestenjeID");
                    DateTime dt = reader.GetDateTime("DatumVrijeme");
                    string tekst = reader.GetString("Tekst");
                    bool procitano = reader.GetBoolean("Procitano");
                    string autor = reader.GetString("Autor");

                    int rowIndex = dataGridViewNotifikacije.Rows.Add(
                        procitano,
                        dt.ToString("dd.MM.yyyy HH:mm"),
                        tekst,
                        autor,
                        notifID);

                    // neprocitane zute, procitane bijele
                    if (!procitano)
                    {
                        dataGridViewNotifikacije.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LemonChiffon;
                    }
                    else
                    {
                        dataGridViewNotifikacije.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri dohvaćanju notifikacija:\n" + ex.Message);
            }
        }

        private void DataGridViewNotifikacije_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (dataGridViewNotifikacije.IsCurrentCellDirty &&
                dataGridViewNotifikacije.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridViewNotifikacije.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Kad se promijeni checkbox (read/unread), upisi u bazu i promijeni boju reda
        private void DataGridViewNotifikacije_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                dataGridViewNotifikacije.Columns[e.ColumnIndex].Name != "colProcitano")
            {
                return;
            }

            var row = dataGridViewNotifikacije.Rows[e.RowIndex];
            bool noviStatus = Convert.ToBoolean(row.Cells["colProcitano"].Value);
            int notifID = Convert.ToInt32(row.Cells["colNotifID"].Value);

            string proc = noviStatus ? "sp_OznaciProcitano" : "sp_OznaciNeprocitano";

            try
            {
                using var conn = DbHelper.GetConnection();
                using var cmd = new MySqlCommand(proc, conn)
                { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@p_notifID", notifID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri oznacavanju notifikacije:\n" + ex.Message);
                return;
            }

            // boji pozadinu: zuto = unread, bijelo = read
            if (noviStatus)
                row.DefaultCellStyle.BackColor = Color.White;
            else
                row.DefaultCellStyle.BackColor = Color.LemonChiffon;

            // Ako je Form1 jos negdje prikazan, refresh broj neprocitanih
            if (parentForm is Form1 f1)
            {
                f1.OsvjeziBrojNotifikacija();
            }
        }

        private void DataGridViewNotifikacije_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridViewNotifikacije.Rows[e.RowIndex];
            var autorObj = row.Cells["colAutor"].Value;
            if (autorObj == null)
            {
                // Nema autora – ne ucitavamo profil
                return;
            }

            string autor = autorObj.ToString()!;
            this.Hide();
            new PregledProfila(parentForm, username, autor).Show();
        }

    }
}
