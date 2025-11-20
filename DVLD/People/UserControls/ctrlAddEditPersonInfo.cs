using DVLD.Validation;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DVLD.Global_Classes;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlAddEditPersonInfo : UserControl
    {
        public ctrlAddEditPersonInfo()
        {
            InitializeComponent();
        }

        clsPerson _Person;
        int _PersonID;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        // داخل DVLD.UserControls.ctrlAddEditPersonInfo

        public void LoadPersonInfo(int PersonID)
        {
            _PersonID = PersonID;

            if (_PersonID == -1) 
            {
                _Mode = enMode.AddNew;
                _Person = new clsPerson(); 
                lblMode.Text = "Add New Person";
            }
            else 
            {
                _Mode = enMode.Update;
                _Person = clsPerson.FindPerson(_PersonID);

                if (_Person == null)
                {
                    MessageBox.Show("This form will be closed because No Person with ID = " + _PersonID);
                    return;
                }

                lblMode.Text = "Edit Person ID " + _PersonID;
                _LoadData(); // تحميل بيانات الشخص من الكائن
            }
        }



        private void _LoadData()
        {

            _FillCountriesInComoboBox();
            cbCountry.SelectedIndex = cbCountry.FindString("Egypt");
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
            llRemoveImage.Visible = false;

            if (_Person != null)
            {
                lbPersonID.Text = _Person.PersonID.ToString();
                txtNationalNo.Text = _Person.NationalNo;
                txtPhone.Text = _Person.Phone;
                txtFirstName.Text = _Person.FirstName;
                txtSecondName.Text = _Person.SecondName;
                txtThirdName.Text = _Person.ThirdName;
                txtLastName.Text = _Person.LastName;
                dateTimePicker1.Value = _Person.DateOfBirth;
                txtAddress.Text = _Person.Address;
                txtEmail.Text = _Person.Email;

                if (_Person.Gendor == "Male")
                {
                    rdMale.Checked = true;
                }
                else
                {
                    rdFemale.Checked = true;
                }
                if (File.Exists(_Person.ImagePath))
                {
                    pbPersonImage.ImageLocation = _Person.ImagePath;
                    llRemoveImage.Visible = true;
                }


                //this will select the country in the combobox.
                cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);

            }
        }
        public void SaveData()
        {
            //  bool IsSave=false;
            int NationalityCountryID = clsCountry.Find(cbCountry.Text.ToString()).ID;

            _Person.Email = txtEmail.Text;
            _Person.Address = txtAddress.Text;
            _Person.DateOfBirth = dateTimePicker1.Value;
            _Person.LastName = txtLastName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.Phone = txtPhone.Text;
            _Person.NationalNo = txtNationalNo.Text;
            _Person.NationalityCountryID = NationalityCountryID;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            if (rdMale.Checked == true)
            {
                _Person.Gendor = "Male";
            }
            else
            {
                _Person.Gendor = "Female";
            }
            try
            {
                if (_Person.Save())
                {
                    MessageBox.Show("Saved successfully!");
                }
                else
                {
                    MessageBox.Show("Save failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            _Mode = enMode.Update;
            lbPersonID.Text = _Person.PersonID.ToString();
            lblMode.Text = "Edit Person ID " + _Person.PersonID.ToString();

        }
        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(temp, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(temp, null);
            }
        }



        private void ctrlAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _LoadData();

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;
            if (!clsValidation.ValidateEmail(txtEmail.Text.ToString()))
            {

                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "inValid Format");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtNationalNum_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "The Field Is required");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
            if (txtNationalNo.Text != _Person.NationalNo && clsPerson.IsPersonExist(txtNationalNo.Text.ToString()))// System.NullReferenceException: 'Object reference not set to an instance of an object.'

            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "The Field Is Exist");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }


        private void rdMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMale.Checked)
            {
                pbPersonImage.Image = Properties.Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Properties.Resources.Female_512;
            }
        }

        private void llOpenFileDialog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.ImageLocation = selectedFilePath;
                llRemoveImage.Visible = true;
            }
        }
        public bool HandelPersonImage()
        {
            if (_HandelPersonImage())
            {
                return true;
            }
            return false;
        }
        private bool _HandelPersonImage()
        {
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }
                if (pbPersonImage.ImageLocation != null)
                {

                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();
                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
                return true;
        }
        

            
        
            public bool ValidateAllFields()
        {
            bool isValid = true;
            if (!this.ValidateChildren())
            {
                return isValid = false;
            }

             return isValid;
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            llRemoveImage.Visible = false;
            if (rdMale.Checked)
            {
                pbPersonImage.Image = Properties.Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Properties.Resources.Female_512;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}

