using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private void _RememberMe()
        {
            if (chRemmberMe.Checked)
            {
                Properties.Settings.Default.UserName = txtUserName.Text;
                Properties.Settings.Default.Password = txtPassword.Text;
            }
            else
            {
                Properties.Settings.Default.UserName = null;
                Properties.Settings.Default.Password = null;
            }
            Properties.Settings.Default.Save();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsUsers User = clsUsers.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (User == null)
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!User.IsActive)
            {
                MessageBox.Show("The account is not active", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _RememberMe();
            this.Hide();
            frmMain frmMain = new frmMain(this);
            frmMain.ShowDialog();

          
            }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
             e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            chRemmberMe.Checked = true;
            txtUserName.Text= Properties.Settings.Default.UserName;
              txtPassword.Text= Properties.Settings.Default.Password;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else {
                txtPassword.UseSystemPasswordChar = true;

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                pictureBox2.Image = Properties.Resources.ChatGPT_Image_00;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                pictureBox2.Image = Properties.Resources.ChatGPT_Image_12;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
