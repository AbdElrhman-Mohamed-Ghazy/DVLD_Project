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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {

        int _AppoinmentID = -1;
        clsTestTypes.enTestType _TestType;
        clsTest _Test;
        private int _TestID = -1;
        public frmTakeTest( clsTestTypes.enTestType testType,int TestAppoinmentID)
        {
            InitializeComponent();
            _AppoinmentID = TestAppoinmentID;
            _TestType = testType;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest1.TestTypeID = _TestType;
            ctrlSecheduledTest1.LoadData(_AppoinmentID);

            if (ctrlSecheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

             _TestID = ctrlSecheduledTest1.TestID;
            if (_TestID != -1)
            {
                _Test = clsTest.FindByTestID(_TestID);

                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNotes.Text = _Test.Notes;
                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
              txtNotes.Enabled = false;
                btnSave.Enabled = false;
            }

            else
                _Test = new clsTest();
          

        }
              

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                     "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            _Test.Notes=txtNotes.Text;
            _Test.CreatedByUserID = clsUsers.FindByUserName(Properties.Settings.Default.UserName).UserID;
            _Test.TestResult = rbPass.Checked;
           _Test.TestAppointmentID = _AppoinmentID;

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
