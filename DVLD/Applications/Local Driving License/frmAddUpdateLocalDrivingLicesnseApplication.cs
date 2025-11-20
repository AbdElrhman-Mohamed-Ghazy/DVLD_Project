using DVLD.People.UserControls;
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

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;

        private int _LocalDrivingLicenseApplicationID=-1;

        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        
        }

        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _Mode = enMode.Update;
        }
        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }

        private void _ResetValues()
        {
            _FillLicenseClassesInComoboBox();

            if (_Mode == enMode.Update)
            {
                lblMode.Text = "Update Local Driving Licence";
                this.Text = lblMode.Text;
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;

            }
            else
            {
                lblMode.Text = "Add New Local Driving Licence";
                this.Text = lblMode.Text;
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                tpApplicationInfo.Enabled = false;
                ctrlPersonCardWithFilter11.FilterFocus();
                cbLicenseClass.SelectedIndex = 2;
                lblApplicationFees.Text = clsApplicationTypes.FindApp((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedBy.Text = Properties.Settings.Default.UserName;
            }

        }

        private void _LoadData()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenceID(_LocalDrivingLicenseApplicationID);
            ctrlPersonCardWithFilter11.FilterEnabled = false;

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No LocalDriving License Application with ID = " + _LocalDrivingLicenseApplicationID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToShortDateString();
            lblApplicationFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedBy.Text = _LocalDrivingLicenseApplication.CreatedByUserInfo.UserName;
            lblDrivingLiAppID.Text= _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();    
            ctrlPersonCardWithFilter11.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetValues();
            if (_Mode==enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnNexxt_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcLocalDrivingInfo.SelectedTab = tcLocalDrivingInfo.TabPages["tpApplicationInfo"];
                return;

            }

            if (ctrlPersonCardWithFilter11.PersonID != -1)
            {

                    btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcLocalDrivingInfo.SelectedTab = tcLocalDrivingInfo.TabPages["tpApplicationInfo"];
                    return;
                
            }

            else
            {
                MessageBox.Show("Error: .Please Set a Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter11.FilterFocus();

            }
     
        }

        private bool CheckPersonAge()
        {
            int AllowedAge= clsLicenseClass.Find(cbLicenseClass.Text.ToString()).MinimumAllowedAge;
            int PersonAge=  DateTime.Now.Year - clsPerson.FindPerson(ctrlPersonCardWithFilter11.PersonID).DateOfBirth.Year;

            if (AllowedAge <= PersonAge)
                return true;    

            return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;


            int ActiveApplicationID = clsLocalDrivingLicenseApplication.IsLocalDrivingLicenseApplicationWihttheSameLicenseClassExist
                                                                                  (_LocalDrivingLicenseApplication.ApplicantPersonID, LicenseClassID);
            if (ActiveApplicationID != -1)
            {
               
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            else
            {
                if (CheckPersonAge())
                {
                   _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
                   _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
                   _LocalDrivingLicenseApplication.ApplicationStatus =clsApplication.enApplicationStatus.New;
                   _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewDrivingLicense;
                   _LocalDrivingLicenseApplication.CreatedByUserID = clsUsers.FindByUserName(Properties.Settings.Default.UserName).UserID;
                   _LocalDrivingLicenseApplication.PaidFees = (float)clsApplicationTypes.FindApp((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees;
                   _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter11.PersonID;
                   _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
                   
                         if (_LocalDrivingLicenseApplication.Save())
                         {
                             lblDrivingLiAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                             _Mode = enMode.Update;
                             lblMode.Text = "Update Local Driving License";
                           this.Text = lblMode.Text;   
                             ctrlPersonCardWithFilter11.FilterEnabled = false;
                             MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                         else
                             MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       
                }
                else
                    MessageBox.Show($"Your age is too Young. You cannot issue a license of type  {cbLicenseClass.Text}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter11.FilterFocus();
        }
    }
    }

