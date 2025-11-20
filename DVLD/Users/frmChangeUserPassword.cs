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
    public partial class frmChangeUserPassword : Form
    {
        private int _UserID;
        private clsUsers _User;
        public frmChangeUserPassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;   
        }

        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }
        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            _User = clsUsers.FindByUserID(_UserID);
            if (_User == null)
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }
            ctrlUserInfo1.LoadUserInof(_UserID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
            //    e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }


            if (txtCurrentPassword.Text != _User.Password)
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Wrong Password!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
              //  e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
              //  e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password !");
                return;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _User.Password = txtNewPassword.Text;
            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefualtValues();
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        }

    }
}
