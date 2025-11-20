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

namespace DVLD.Manage_TestTypes
{
    public partial class frmEditTestTypes : Form
    {
        clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        clsTestTypes _Test;
        public frmEditTestTypes(clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void _LoadData()
        {
            _Test = clsTestTypes.FindTest(_TestTypeID);

            if (_Test != null) 
            {
                txtTitle.Focus();
                lblID.Text= ((int)_TestTypeID).ToString();
                txtDescription.Text= _Test.TestTypeDescription;
                txtFees.Text=_Test.TestTypeFees.ToString();
                txtTitle.Text= _Test.TestTypeTitle; 
            }
            else
            {
                txtFees.Text = "???";
                txtTitle.Text ="???";
                txtDescription.Text = "???";
                lblID.Text = "???";
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                //   e.Cancel = true;
                errorProvider1.SetError(txtFees, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                //   e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                //   e.Cancel = true;
                errorProvider1.SetError(txtDescription, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }



        private void frmEditTestTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
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
          
            _Test.TestTypeFees = Convert.ToDecimal(txtFees.Text);
            _Test.TestTypeDescription=txtDescription.Text;  
            _Test.TestTypeTitle=txtTitle.Text;
            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
