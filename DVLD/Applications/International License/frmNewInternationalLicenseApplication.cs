using DVLD.Licenses;
using DVLD.Licenses.international_licenses;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private int _InternationalLicenseID = -1;
        clsInternationalLicense internationalLicense;
        clsLicense license; 
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
          
        }
        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            
        }
        private void _LoadApplicationBasicInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text = clsApplicationTypes.FindApp((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = Properties.Settings.Default.UserName;
            lblLocalLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseID.ToString();
            llShowLicenseInfo.Enabled= _InternationalLicenseID != -1;   
        }

        private void _ResetDefaultValues()
        {
            lblApplicationDate.Text = "[??/??/????]";
            lblIssueDate.Text = "[??/??/????]";
            lblExpirationDate.Text = "[??/??/????]";
            lblFees.Text = "[???]";
            lblCreatedByUser.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";
            llShowLicenseInfo.Enabled = false;
            btnIssueLicense.Enabled = false;
           
        }
         
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (ctrlDriverLicenseInfoWithFilter1.LicenseID ==-1)
            {
             // MessageBox.Show("Could not Find Licence ID " + ctrlDriverLicenseInfoWithFilter1.TxtLicenceValue(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }


            if (clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID) != -1)
            {
                MessageBox.Show("This Person Already has a International Licence", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassIfo.LicenseClassID != 3)
            {
                MessageBox.Show("This Person Should Have Licence Class 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }
           
            _LoadApplicationBasicInfo();
          
            btnIssueLicense.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
          frm.ShowDialog(); 
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypes.FindApp((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            InternationalLicense.CreatedByUserID = clsUsers.FindByUserName(Properties.Settings.Default.UserName).UserID;


            InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = clsUsers.FindByUserName(Properties.Settings.Default.UserName).UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);    
            frm.ShowDialog();   
        }
    }
}
