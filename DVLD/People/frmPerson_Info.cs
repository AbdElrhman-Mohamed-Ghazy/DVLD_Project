using DVLD.UserControls;
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

namespace DVLD
{
    public partial class frmPerson_Info : Form
    {
        public frmPerson_Info(int PersonID)
        {
            InitializeComponent();

            ctrlPersonInformation1.LoadPersonInfo(PersonID);
        }

        public frmPerson_Info(string  NationalNo)
        {
            InitializeComponent();

            ctrlPersonInformation1.LoadPersonInfo(NationalNo);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
        this.Close();

        }
    }
}


