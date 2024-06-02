using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedLibrary.Helpers;
using SharedLibrary.Classes;

namespace TheSandwichMakersHardwareStoreSolution
{
    public partial class LoginFormControl : UserControl
    {
        public Main main { get; }
        public Login Login { get; set; }

        public LoginFormControl(Main main)
        {
            InitializeComponent();

            this.main = main;
            Login = new Login();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Login succefully.");
            main.HandleLogin("saoud@test.com");
            return;

            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            try
            {
                if (Login.AuthenticateAdmin(email, password) != false)
                {
                    MessageBox.Show("Login succefully.");
                    main.HandleLogin(email);
                }
                else
                {
                    MessageBox.Show("Sorry, your email or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
    }
}
