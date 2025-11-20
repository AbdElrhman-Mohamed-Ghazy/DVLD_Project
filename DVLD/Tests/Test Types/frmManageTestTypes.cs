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
    public partial class frmManageTestTypes : Form
    {
        DataTable dtAllTestTypes;

        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void _Refresh()
        {
            dtAllTestTypes = clsTestTypes.GetAllTestTypes();
            dgvTestType.DataSource = dtAllTestTypes;
            lblRecordsCount.Text = dgvTestType.Rows.Count.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            dtAllTestTypes = clsTestTypes.GetAllTestTypes();
            dgvTestType.DataSource = dtAllTestTypes;
            lblRecordsCount.Text = dgvTestType.Rows.Count.ToString();
            if (dgvTestType.Rows.Count > 0)// to ensure Data and Remane ColumHeader
            {
                dgvTestType.Columns[0].HeaderText = "ID";
                dgvTestType.Columns[0].Width = 100;

                dgvTestType.Columns[1].HeaderText = "Title";
                dgvTestType.Columns[1].Width = 250;

                dgvTestType.Columns[2].HeaderText = "Description";
                dgvTestType.Columns[2].Width = 400;

                dgvTestType.Columns[3].HeaderText = "Fees";
                dgvTestType.Columns[3].Width = 100;
                dgvTestType.DataSource = dtAllTestTypes;
            }
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestTypes frmEditTestTypes = new frmEditTestTypes((clsTestTypes.enTestType)dgvTestType.CurrentRow.Cells[0].Value); 
            frmEditTestTypes.ShowDialog();
            _Refresh();
        }
    }
}
