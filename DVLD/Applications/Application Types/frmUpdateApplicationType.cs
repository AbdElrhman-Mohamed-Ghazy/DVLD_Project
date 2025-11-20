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

namespace DVLD.Manage_Application_Types
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _ApplicatinID;
        clsApplicationTypes _ApplicationTypes;
        public frmUpdateApplicationType(int ApplicatinID)
        {
            InitializeComponent();
            _ApplicatinID = ApplicatinID;   
        }

        private void _LoadData()
        {
            _ApplicationTypes = clsApplicationTypes.FindApp(_ApplicatinID);
            if (_ApplicationTypes!=null)
            {
                txtApplicationTitle.Focus();    
            lblApplicationID.Text = _ApplicationTypes.ApplicationTypeID.ToString();
            txtApplicationFees.Text = _ApplicationTypes.ApplicationFees.ToString(); 
            txtApplicationTitle.Text = _ApplicationTypes.ApplicationTypeTitle.ToString();
                
            }
            else
            {
                lblApplicationID.Text = "???";
                txtApplicationFees.Text = "???";
                txtApplicationTitle.Text = "???";
            }

        }


        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }



        private void txtApplicationFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtApplicationFees.Text.Trim()))
            {
                //   e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtApplicationFees, null);
            }
        }

        private void txtApplicationTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtApplicationTitle.Text.Trim()))
            {
                //   e.Cancel = true;
                errorProvider1.SetError(txtApplicationTitle, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtApplicationTitle, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            _ApplicationTypes.ApplicationFees =Convert.ToUInt64(txtApplicationFees.Text.Trim());   
            _ApplicationTypes.ApplicationTypeTitle = txtApplicationTitle.Text.Trim();
            if (_ApplicationTypes.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtApplicationFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
