using DVLD.Licenses;
using DVLD.Licenses.international_licenses;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Applications.Local_Driving_License.Renew_Local_License
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {

        private int _NewLicenseID=-1;

        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            lblApplicationDate.Text = "[??/??/????]";
            lblApplicationFees.Text = "???";
            lblApplicationID.Text = "???";
            lblCreatedByUser.Text = "???";  
            lblExpirationDate.Text = "[??/??/????]"; 
            lblIssueDate.Text = "[??/??/????]";
            lblLicenseFees.Text = "???";
            lblOldLicenseID.Text = "???";
            lblRenewedLicenseID.Text = "???";
            lblTotalFees.Text = "???";
            llShowLicenseInfo.Enabled = false;
            btnRenewLicense.Enabled = false;
        }

        private void _Loadinfo()
        {
            lblApplicationDate.Text =DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationTypes.FindApp((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
             lblCreatedByUser.Text = Properties.Settings.Default.UserName;
            lblExpirationDate.Text = DateTime.Now.AddYears(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassIfo.
                                                      DefaultValidityLength).ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblLicenseFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassIfo.ClassFees.ToString();
             lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text=ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Notes.ToString();
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (ctrlDriverLicenseInfoWithFilter1.LicenseID == -1)
            {
                // MessageBox.Show("Could not Find Licence ID " + ctrlDriverLicenseInfoWithFilter1.TxtLicenceValue(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate > DateTime.Now)
            {
             MessageBox.Show("This  Licence Will be Expired At " + ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate.ToLongDateString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 _ResetDefaultValues();
                return;
            }
            if (! ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("This  Licence Is not Active" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }
            _Loadinfo();
            btnRenewLicense.Enabled = true;
        }

        private void btnRenweLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense
                                     (txtNotes.Text.Trim(), clsUsers.FindByUserName(Properties.Settings.Default.UserName).UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
          
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           frmShowLicenseInfo frmShowLicenseInfo = new frmShowLicenseInfo(_NewLicenseID);
            frmShowLicenseInfo.ShowDialog();    
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void frmRenewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {

        }
    }
}
