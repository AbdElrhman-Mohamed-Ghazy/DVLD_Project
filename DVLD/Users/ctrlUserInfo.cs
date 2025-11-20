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
    public partial class ctrlUserInfo : UserControl
    {
        private int _UserID;
        private clsUsers _User;
        public int UserID
        {
            get { return _UserID; }
        }

        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        private void _RsetValues()
        {
            lblIsActive.Text = "[????]";
            lblUserID.Text = "[????]";
            lblUserName.Text = "[????]";
            ctrlPersonInformation1.ResetPersonInfo();
        }

        private void _FillUserInfo()
        {
            ctrlPersonInformation1.LoadPersonInfo(_User.PersonID);
            lblUserName.Text = _User.UserName;
            lblUserID.Text = _UserID.ToString();
            if (_User.IsActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }
        }

        public void LoadUserInof(int UserID)
        {
            _UserID = UserID;
            _User = clsUsers.FindByUserID(_UserID);

            if (_User != null)
            {
                _FillUserInfo();

            }
            else
            {
              _RsetValues();
                
            }

        }
        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {
           // LoadUserInof();
         }         


        }
    }

