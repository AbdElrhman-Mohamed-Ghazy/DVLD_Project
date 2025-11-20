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
    public partial class frmUserCard : Form
    {
      private  int  _UserID;
        public frmUserCard(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;   
        }

        private void frmUserCard_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.LoadUserInof(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
