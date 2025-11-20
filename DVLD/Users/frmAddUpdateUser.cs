using DVLD.People.UserControls;
using DVLD.Validation;
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
    public partial class frmAddUpdateUser : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _UserID = -1;
        clsUsers _User;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;  
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;   
        }

       private void _ResetValues()
        {
            if (_Mode==enMode.Update)
            {
                lblMode.Text = "Update User";
                this.Text = lblMode.Text;
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
             }
            else
            {
                lblMode.Text = "Add New User";
                this.Text = lblMode.Text;
                _User = new clsUsers();
                tpLoginInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            _User = clsUsers.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_User==null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblUserID.Text = _UserID.ToString();    
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;    
            txtUserName.Text = _User.UserName;
            chkIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

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
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;
            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();

                _Mode = enMode.Update;
                lblMode.Text = "Update User";
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

               if (clsUsers.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                    
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                    return;
                }
                
            }

            else
            {
                MessageBox.Show("Error: .Please Set a Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }


        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetValues();
            if (_Mode==enMode.Update)
            {
                _LoadData();
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }


            if (_Mode == enMode.AddNew)
            {
                if (clsUsers.IsUserExistForUserName(txtUserName.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "The User Name Is Aready Exist!");
                    
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }          
            }

            else
            {
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUsers.IsUserExistForUserName(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                    
                }
            }


        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
          
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password !");
                return;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
            ;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
