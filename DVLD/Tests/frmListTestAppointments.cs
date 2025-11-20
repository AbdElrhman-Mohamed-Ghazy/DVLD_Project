using DVLD.Properties;
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
using static DVLD_Business.clsTestTypes;

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private int _LocalDrivingLicenceApplicationID = -1;

        clsTestTypes.enTestType TestType = clsTestTypes.enTestType.VisionTest;

         DataTable _dtTestAppoiments;


        public frmListTestAppointments(int LocalDrivingLicenceApplicationID, clsTestTypes.enTestType testType)
        {
            InitializeComponent();
            _LocalDrivingLicenceApplicationID = LocalDrivingLicenceApplicationID;
            TestType = testType;
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (TestType)
            {

                case clsTestTypes.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestTypes.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestTypes.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenceApplicationID);
            _dtTestAppoiments = clsTestAppointments.GetTestAppointmentsForCuurentTest((int)TestType, _LocalDrivingLicenceApplicationID);

            dgvLicenseTestAppointments.DataSource = _dtTestAppoiments;

            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Columns.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 120;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 120;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;

                btnAddNewAppointment.Enabled = !clsTestAppointments.IsPassedTest((int)TestType, _LocalDrivingLicenceApplicationID);
           
                
            }
        }
    
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
     
        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointments.IsThereActiveAppoinment((int)TestType, _LocalDrivingLicenceApplicationID))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingLicenceApplicationID, TestType);
            frmScheduleTest.ShowDialog();
            frmListTestAppointments_Load(null,null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;
             frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingLicenceApplicationID, TestType,TestAppointmentID);  
            frmScheduleTest.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frmTakeTest = new frmTakeTest( TestType,(int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value);
            frmTakeTest.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }
    }
}
