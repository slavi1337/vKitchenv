using MySql.Data.MySqlClient;
using System.Data;

namespace vKitchenv
{
    public partial class Registracija : Form
    {
        private Form parentForm;
        private bool zatvaranjeBezGasenja = false;

        public Registracija(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.FormClosing += Registracija_FormClosing;
        }

        private void Registracija_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!zatvaranjeBezGasenja)
            {
                parentForm.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            zatvaranjeBezGasenja = true;
            this.Close();
        }

        private void registrujSeBtn_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string lozinka = textBox2.Text.Trim();
            string ponoviLozinku = textBox3.Text.Trim();
            string ime = textBox4.Text.Trim();
            string prezime = textBox5.Text.Trim();

            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(lozinka) ||
                string.IsNullOrEmpty(ponoviLozinku) ||
                string.IsNullOrEmpty(ime) ||
                string.IsNullOrEmpty(prezime))
            {
                MessageBox.Show("Sva polja su obavezna", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lozinka != ponoviLozinku)
            {
                MessageBox.Show("Lozinke se ne poklapaju", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conn = DbHelper.GetConnection())
                using (var cmd = new MySqlCommand("sp_DodajKorisnika", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_username", username);
                    cmd.Parameters.AddWithValue("@p_lozinka", lozinka);
                    cmd.Parameters.AddWithValue("@p_ime", ime);
                    cmd.Parameters.AddWithValue("@p_prezime", prezime);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Greska: " + ex.Message, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Registracija uspjesna!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                parentForm.Show();
                zatvaranjeBezGasenja = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri radu sa bazom:\n" + ex.Message, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
            textBox3.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
