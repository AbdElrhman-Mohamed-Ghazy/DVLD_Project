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

namespace DVLD.Users
{
    public partial class frmManageUsers : Form
    {

        private static DataTable _dtAllUsers;

        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUsers.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            lblRecordsCount.Text=dgvUsers.Rows.Count.ToString();

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUsers.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
            if (dgvUsers.Rows.Count > 0)// to ensure Data and Remane ColumHeader
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 110;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 300;


                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 120;

                dgvUsers.Columns[4].HeaderText = "IsActive";
                dgvUsers.Columns[4].Width = 100;


            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text=="IsActive")
            {
                cbIsActive.Visible = true;
                txtFilterValue.Visible = false;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }
            else
            {
              txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "IsActive":
                    FilterColumn = "IsActive";
                    break;

                case "User Name":
                    FilterColumn = "UserName";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }
            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

   

            if (FilterColumn == "PersonID"|| FilterColumn=="UserID")
                //in this case we deal with integer not string.

                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "PersonID" ||cbFilterBy.Text== "UserID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterValue_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValueIsActive = "";

            if (cbIsActive.Text == "Yes")
                FilterValueIsActive = "True";
            else if (cbIsActive.Text == "No")
                FilterValueIsActive = "False";
            else
            {
                _dtAllUsers.DefaultView.RowFilter = ""; // All
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return; // مهم علشان ما يكملش السطر اللي بعده
            }

            _dtAllUsers.DefaultView.RowFilter = string.Format("[IsActive] = {0}", FilterValueIsActive);
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frmAddNewUser = new frmAddUpdateUser();
            frmAddNewUser.ShowDialog();
            _RefreshUsersList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frmAddUpdateUser = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value); 
            frmAddUpdateUser.ShowDialog();  
           _RefreshUsersList();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frmAddNewUser = new frmAddUpdateUser();
            frmAddNewUser.ShowDialog();
            _RefreshUsersList();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void sendEmailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete User [" + dgvUsers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsUsers.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.");
                    _RefreshUsersList();

                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void showDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserCard frmUserCard = new frmUserCard((int)dgvUsers.CurrentRow.Cells[0].Value);    
            frmUserCard.ShowDialog();
        }

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            frmUserCard frmUserCard = new frmUserCard((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmUserCard.ShowDialog();
        }

        private void ChangePasswodrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frmChangeUserPassword = new frmChangeUserPassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmChangeUserPassword.ShowDialog();
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }
