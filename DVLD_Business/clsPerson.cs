using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DataAcess;

namespace DVLD_Business
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string Phone { get; set; }
        public int NationalityCountryID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {SecondName} {ThirdName} {LastName}".Trim();
            }
        }
        public DateTime DateOfBirth { get; set; }
        public string Gendor { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        private string _ImagePath;
        public string ImagePath {

            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsCountry CountryInfo;

         public  clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Gendor = "";
            this.NationalNo = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }
         private clsPerson( int personID, string nationalNo, string phone, int nationalityCountryID, string firstName, string secondName,
              string thirdName, string lastName, DateTime dateOfBirth, string gendor, string address, string email, string imagePath)
        {
            PersonID = personID;
            NationalNo = nationalNo;
            Phone = phone;
            NationalityCountryID = nationalityCountryID;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gendor = gendor;
            Address = address;
            Email = email;
            ImagePath = imagePath;
            Mode = enMode.Update;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
        }

        public static clsPerson FindPerson(string ColumName, object Value)
        {
            int personID = -1, nationalityCountryID = -1;
            DateTime dateOfBirth = DateTime.Now;
            string nationalNo = "", phone = "", firstName = "", secondName = "",
               thirdName = "", lastName = "", gendor = "", address = "", email = "", imagePath = "";

            if (clsPersonData.FindPerson(ColumName, Value,ref personID, ref nationalNo, ref phone, ref nationalityCountryID, ref firstName, ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address, ref email, ref imagePath))
            {
                return new clsPerson(personID, nationalNo, phone, nationalityCountryID, firstName, secondName, thirdName,
                    lastName, dateOfBirth, gendor, address, email, imagePath);
            }
            else
            { return null; }
            }
        public static clsPerson FindPerson(int PersonID)
        {
          int nationalityCountryID = -1;
            DateTime dateOfBirth = DateTime.Now;
            string nationalNo = "", phone = "", firstName = "", secondName = "",
               thirdName = "", lastName = "", gendor = "", address = "", email = "", imagePath = "";

            if (clsPersonData.FindPerson(PersonID, ref nationalNo, ref phone, ref nationalityCountryID, ref firstName, ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address, ref email, ref imagePath))
            {
                return new clsPerson(PersonID,nationalNo, phone, nationalityCountryID, firstName, secondName, thirdName, lastName,
                dateOfBirth, gendor, address, email, imagePath);
            }
            else
            { return null; }

        }

        public static clsPerson FindPerson(string NationalNo)
        {
            int PersonID = -1, nationalityCountryID=-1;
            DateTime dateOfBirth = DateTime.Now;
            string  phone = "", firstName = "", secondName = "",
               thirdName = "", lastName = "", gendor = "", address = "", email = "", imagePath = "";

            if (clsPersonData.FindPerson(NationalNo, ref PersonID, ref phone, ref nationalityCountryID, ref firstName, ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address, ref email, ref imagePath))
            {
                return new clsPerson(PersonID, NationalNo, phone, nationalityCountryID, firstName, secondName, thirdName, lastName,
                dateOfBirth, gendor, address, email, imagePath);
            }
            else
            { return null; }

        }

        public static DataTable FilterBy(string ColumName , object Value)
        {
            return clsPersonData.FilterBy(ColumName, Value);
        }
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }
        public static DataTable GetAllPerson()
        {
            return clsPersonData.GetAllPerson();
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }
        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.Phone, this.NationalityCountryID, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Email, this.ImagePath);

            return (this.PersonID != -1);
        }


        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.Phone, this.NationalityCountryID, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Email, this.ImagePath);
        }
        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }




            return false;
        }
    }
}

