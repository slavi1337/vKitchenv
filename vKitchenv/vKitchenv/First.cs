namespace vKitchenv
{
    public partial class First : Form
    {
        public First()
        {
            InitializeComponent();
        }

        private void prijavaBtn_Click(object sender, EventArgs e)
        {
            var loginForm = new Prijava(this);
            loginForm.Show();
            this.Hide();
        }

        private void registracijaBtn_Click(object sender, EventArgs e)
        {
            var regForm = new Registracija(this);
            regForm.Show();
            this.Hide();
        }
    }
}
