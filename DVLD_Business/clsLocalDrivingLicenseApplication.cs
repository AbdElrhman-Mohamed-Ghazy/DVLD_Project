using DVLD_DataAcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAcess;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
    
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        
        public int LocalDrivingLicenseApplicationID { get; set; } 
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;

        public string PersonFullName
        {
            get { return base.PersonInfo.FullName; }
        }
                
      

        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
            Mode = enMode.AddNew;


        }

              private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int LicenseClassID, int applicationID, int applicationTypeID,
                    enApplicationStatus applicationStatus, DateTime lastStatusDate, int createdByUserID, int applicationPersonID,float paidFees, DateTime applicationDate)
                   : base(applicationID, applicationTypeID, applicationStatus, lastStatusDate, createdByUserID, applicationPersonID, paidFees, applicationDate)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enMode.Update;
        }


        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            // 
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassID, ApplicationID, Application.ApplicationTypeID,
                                                    Application.ApplicationStatus, Application.LastStatusDate, Application.CreatedByUserID, Application.ApplicantPersonID,
                                                    Application.PaidFees, Application.ApplicationDate);
            }
            else
                return null;


        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenceID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassID, ApplicationID, Application.ApplicationTypeID,
                                                 Application.ApplicationStatus, Application.LastStatusDate, Application.CreatedByUserID, Application.ApplicantPersonID,
                                                 Application.PaidFees, Application.ApplicationDate);
            }
            else
            {
                return null;
            }

        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID=clsLocalDrivingLicenseApplicationData._AddNewLocalDrivingLicenseApplication(this.ApplicationID,this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);

        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID,
                                                                                                                                                             this.ApplicationID,this.LicenseClassID);
        }

        public bool Save()
        {

            if (!base.Save())
                return false;


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                     
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public static int IsLocalDrivingLicenseApplicationWihttheSameLicenseClassExist(int ApplicantPersonID, int LicenseClassID)
        {
            return(clsLocalDrivingLicenseApplicationData.IsLocalDrivingLicenseApplicationWihttheSameLicenseClassExist(ApplicantPersonID, LicenseClassID));
        }

        public static  DataTable GetAllLocalDrivingLicesnseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicesnseApplications();
        }


        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public  bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            IsLocalDrivingApplicationDeleted=clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);
            if (!IsLocalDrivingApplicationDeleted)
            {
                return false;
            }

            IsBaseApplicationDeleted = base.DeleteBaseApplication();

            return IsBaseApplicationDeleted;    
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetCompleted();

                return License.LicenseID;
            }

            else
                return -1;
        }
        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

    }
}
