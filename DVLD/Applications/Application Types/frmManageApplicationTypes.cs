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

namespace DVLD.Manage_Application_Types
{
    public partial class frmManageApplicationTypes : Form
    {

        DataTable dtAllApplicatinTypes;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void _Refresh()
        {
            dtAllApplicatinTypes = clsApplicationTypes.GetAllApplicationTypes();
            dgvAppType.DataSource = dtAllApplicatinTypes;
            lblRecordsCount.Text = dgvAppType.Rows.Count.ToString();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm = new frmUpdateApplicationType((int)dgvAppType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            dtAllApplicatinTypes = clsApplicationTypes.GetAllApplicationTypes();
            dgvAppType.DataSource = dtAllApplicatinTypes;
            lblRecordsCount.Text = dgvAppType.Rows.Count.ToString();
            if (dgvAppType.Rows.Count > 0)// to ensure Data and Remane ColumHeader
            {
                dgvAppType.Columns[0].HeaderText = "ID";
                dgvAppType.Columns[0].Width = 100;

                dgvAppType.Columns[1].HeaderText = "Title";
                dgvAppType.Columns[1].Width = 360;

                dgvAppType.Columns[2].HeaderText = "Fees";
                dgvAppType.Columns[2].Width = 100;
                dgvAppType.DataSource = dtAllApplicatinTypes;
            }
        }
    }
}
