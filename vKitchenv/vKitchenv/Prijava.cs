using MySql.Data.MySqlClient;
using System.Data;

namespace vKitchenv
{
    public partial class Prijava : Form
    {

        private Form parentForm;
        private bool zatvaranjeBezGasenja = false;

        public Prijava(Form parentForm)
        {
            this.parentForm = parentForm;
            this.FormClosing += Prijava_FormClosing;
            InitializeComponent();
        }

        private void Prijava_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!zatvaranjeBezGasenja)
            {
                parentForm.Close();
            }
        }

        private void prijaviSeBtn_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text.Trim();
            string pass = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Unesite oba polja", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DbHelper.GetConnection())
                using (var cmd = new MySqlCommand("sp_ProvjeriPrijavu", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_username", user);
                    cmd.Parameters.AddWithValue("@p_lozinka", pass);

                    var outParam = new MySqlParameter("@p_valid", MySqlDbType.Bit);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.ExecuteNonQuery();

                    bool validan = Convert.ToBoolean(outParam.Value);
                    if (validan)
                    {
                        MessageBox.Show("Prijava uspjesna", "Dobrodosli", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var glavna = new Form1(user);
                        glavna.FormClosed += (s, args) => parentForm.Close();
                        glavna.Show();

                        zatvaranjeBezGasenja = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Pogresno korisnicko ime ili lozinka",
                                        "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri pristupu bazi:\n" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            zatvaranjeBezGasenja = true;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }

}
