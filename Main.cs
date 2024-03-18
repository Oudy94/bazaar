using System.Security.Cryptography.X509Certificates;
using TheSandwichMakersHardwareStoreSolution.Classes;

namespace TheSandwichMakersHardwareStoreSolution
{
    public partial class Main : Form
    {
        public LoginFormControl LoginFormControl { get; }
        public MainControl MainControl { get; }
        public List<Role> GetRoles() => MainControl.GetRoles();
        public List<Department> GetDepartments() => MainControl.GetDepartments();

        public Main()
        {
            InitializeComponent();

            this.LoginFormControl = new LoginFormControl(this);
            this.MainControl = new MainControl();

            pnlMain.Controls.Add(LoginFormControl);
        }

        public void HandleLogin(string email)
        {
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(MainControl);
        }
    }
}
