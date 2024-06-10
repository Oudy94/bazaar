using System.Security.Cryptography.X509Certificates;
using SharedLibrary.Classes;

namespace TheSandwichMakersHardwareStoreSolution
{
    public partial class Main : Form
    {
        public LoginFormControl LoginFormControl { get; }
        public MainControl MainControl { get; }

        public Main()
        {
            InitializeComponent();

            this.LoginFormControl = new LoginFormControl(this);
            this.MainControl = new MainControl();

            LoginFormControl.Dock = DockStyle.Fill;
            Controls.Add(LoginFormControl);
        }

        public void HandleLogin(string email)
        {
            Controls.Clear();
            MainControl.Dock = DockStyle.Fill;
            Controls.Add(MainControl);
            MainControl.AuthenticatedEmployee(email);
        }
    }
}
