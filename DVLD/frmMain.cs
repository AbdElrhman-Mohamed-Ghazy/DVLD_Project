using DVLD.Applications.International_License;
using DVLD.Applications.Local_Driving_License;
using DVLD.Applications.Local_Driving_License.Renew_Local_License;
using DVLD.Applications.Realse_Loal_Licence;
using DVLD.Applications.ReplaceLostOrDamagedLicense;
using DVLD.Applications.Rlease_Detained_License;
using DVLD.Drivers;
using DVLD.Licenses.Detain_License;
using DVLD.Manage_Application_Types;
using DVLD.Manage_TestTypes;
using DVLD.UserControls;
using DVLD.Users;
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

namespace DVLD
{
    public partial class frmMain : Form
    {

        clsUsers CurrentUser = clsUsers.FindByUserName(Properties.Settings.Default.UserName);
        frmLoginScreen _frm;
        private bool _isSigningOut = false;

        public frmMain(frmLoginScreen frm )
        {

            InitializeComponent();
   
            _frm = frm;


        }
       
        private void tsbProple_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isSigningOut = true;
            _frm.Show();    
            this.Close();
        }

        private void tsbUsers_Click(object sender, EventArgs e)
        {
            frmManageUsers frmManageUsers = new frmManageUsers();   
            frmManageUsers.ShowDialog();    
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserCard frmUserCard = new frmUserCard(CurrentUser.UserID);
            frmUserCard.ShowDialog();   
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frmChangeUserPassword  = new frmChangeUserPassword(CurrentUser.UserID);   
            frmChangeUserPassword.ShowDialog(); 
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isSigningOut)
                Application.Exit(); // يقفل البرنامج فقط لو مش بيعمل SignOut

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frmManageApplicationTypes = new frmManageApplicationTypes();
            frmManageApplicationTypes.ShowDialog();
        }

        private void manageApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
   
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frmManageTestTypes = new frmManageTestTypes();
            frmManageTestTypes.ShowDialog();    
        }

        private void ucThemeSwitcher1_Load(object sender, EventArgs e)
        {

        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frmListLocalDrivingLicesnseApplications = new frmListLocalDrivingLicesnseApplications();    
            frmListLocalDrivingLicesnseApplications.ShowDialog();   
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frmAddUpdateLocalDrivingLicesnseApplication=new frmAddUpdateLocalDrivingLicesnseApplication();  
            frmAddUpdateLocalDrivingLicesnseApplication.ShowDialog();   
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frmListLocalDriving = new frmListLocalDrivingLicesnseApplications();
            frmListLocalDriving.ShowDialog();   
        }

        private void tsbDrivers_Click(object sender, EventArgs e)
        {
            frmListDrivers listDrivers = new frmListDrivers();  
            listDrivers.ShowDialog();   
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication application = new frmNewInternationalLicenseApplication();    
            application.ShowDialog();   
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicesnseApplications frm =new frmListInternationalLicesnseApplications();
            frm.ShowDialog();   
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frmRenew = new frmRenewLocalDrivingLicenseApplication();
            frmRenew.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frmReplaceLostOrDamagedLicenseApplication =new frmReplaceLostOrDamagedLicenseApplication();   
            frmReplaceLostOrDamagedLicenseApplication.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frmReleaseDetainedLicenseApplication =new frmReleaseDetainedLicenseApplication();
            frmReleaseDetainedLicenseApplication.ShowDialog();  
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frmListDetainedLicenses =new frmListDetainedLicenses(); 
            frmListDetainedLicenses.ShowDialog();
        }
    }
}
