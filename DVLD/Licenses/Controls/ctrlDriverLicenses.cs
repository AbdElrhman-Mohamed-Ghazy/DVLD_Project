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

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {

        private int _DriverID;
        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicensesHistory;
        private DataTable _dtDriverInternationalLicensesHistory;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadInternationalLicenseInfo()
        {
            _dtDriverInternationalLicensesHistory = clsDriver.GetInternationalLicenses(_DriverID);
            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicensesHistory;

            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();
            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "International License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 200;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 120;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "License ID.";
                dgvInternationalLicensesHistory.Columns[2].Width = 120;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 120;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 120;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "IsActive";
                dgvInternationalLicensesHistory.Columns[5].Width = 100;
            }
        }

              private void _LoadLocalLicenseInfo()
        {

                _dtDriverLocalLicensesHistory = clsDriver.GetLicenses(_DriverID);


               dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;
               lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();
              
               if (dgvLocalLicensesHistory.Rows.Count > 0)
               {
                   dgvLocalLicensesHistory.Columns[0].HeaderText = "Local License ID";
                   dgvLocalLicensesHistory.Columns[0].Width = 110;
              
                   dgvLocalLicensesHistory.Columns[1].HeaderText = "Application ID";
                   dgvLocalLicensesHistory.Columns[1].Width = 110;
              
                   dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                   dgvLocalLicensesHistory.Columns[2].Width = 270;
              
                   dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                   dgvLocalLicensesHistory.Columns[3].Width = 170;
              
                   dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                   dgvLocalLicensesHistory.Columns[4].Width = 170;
              
                   dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                   dgvLocalLicensesHistory.Columns[5].Width = 110;
              
               }
        }

        public void LoadData(int DriverID)
        {
            _DriverID = DriverID;   
            _Driver=clsDriver.FindByDriverID(DriverID);
            if (_Driver == null)
            {
                MessageBox.Show("There is no Driver with id = " + DriverID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
    
            _LoadInternationalLicenseInfo();
            _LoadLocalLicenseInfo();
        }

        public void LoadInfoByPersonID(int PersonID)
        {

            _Driver = clsDriver.FindByPersonID(PersonID);
            if (_Driver == null)
            {
                MessageBox.Show("There is no Person Link With Driver with id = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             return;
             }
            _DriverID = _Driver.DriverID;

             _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }

        public void Clear()
        {
            _dtDriverLocalLicensesHistory.Clear();
            _dtDriverInternationalLicensesHistory.Clear();
        }

    

      

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }
    }
    }

