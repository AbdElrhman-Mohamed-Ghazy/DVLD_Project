using DVLD.Tests.Controls;
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

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        clsTestTypes.enTestType TestType;
        private int _ApplicationID = -1;
        int _AppoinmentID = -1;
        public frmScheduleTest(int ApplicationID, clsTestTypes.enTestType testType,int AppoinmentID = -1)
        {
            InitializeComponent();
            _ApplicationID = ApplicationID;
            TestType = testType;
            _AppoinmentID =AppoinmentID;

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestType = TestType;  
            ctrlScheduleTest1.LoadData(_ApplicationID, _AppoinmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }


        //private void _Vision()
        //{
        //    pbTestTypeImage.Image = Properties.Resources.Vision_512;
        //    lblTitle.Text = "Vision Test Appointments";
        //    this.Text = lblTitle.Text;
        //}
        //private void _Written()
        //{
        //    pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
        //    lblTitle.Text = "Written Test Appointments";
        //    this.Text = lblTitle.Text;
        //}
        //private void _Streat()
        //{
        //    pbTestTypeImage.Image = Properties.Resources.driving_test_512;
        //    lblTitle.Text = "Streat Test Appointments";
        //    this.Text = lblTitle.Text;
        //}
    }
}
