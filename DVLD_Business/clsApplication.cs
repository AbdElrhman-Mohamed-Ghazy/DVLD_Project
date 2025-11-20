using DVLD_DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplication
    {
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationStatus { New = 1,  Cancelled= 2, Completed=3};
        public  clsApplication.enApplicationStatus ApplicationStatus {  get; set; }
        public string StatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }     
        public DateTime LastStatusDate { get; set; }

        public string ApplicantFullName
        {
            get
            {
                return clsPerson.FindPerson(ApplicantPersonID).FullName;
            }
        }
        public int ApplicationID { get; set; }

        public int ApplicationTypeID { get; set; }
        public clsApplicationTypes ApplicationTypeInfo;
       
        public int CreatedByUserID { get; set; }
        public clsUsers CreatedByUserInfo;


        public int ApplicantPersonID { get; set; }
        public clsPerson PersonInfo;

        public float PaidFees { get; set; }
        public DateTime ApplicationDate { get; set; }

        public clsApplication()
        {
            ApplicationID = -1;
            ApplicationTypeID = -1;
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            ApplicationDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            ApplicantPersonID = -1;
            Mode = enMode.AddNew;
        }

        public clsApplication(int applicationID,int applicationTypeID,enApplicationStatus applicationStatus,DateTime lastStatusDate, int createdByUserID,
                                            int applicationPersonID, float paidFees, DateTime applicationDate )
        {
            ApplicationID = applicationID;
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeInfo=clsApplicationTypes.FindApp( applicationTypeID );   
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            CreatedByUserID = createdByUserID;
            ApplicantPersonID = applicationPersonID;
            PaidFees = paidFees;
            ApplicationDate = applicationDate;
            CreatedByUserInfo =clsUsers.FindByUserID(CreatedByUserID);
            PersonInfo=clsPerson.FindPerson(ApplicantPersonID);
            Mode = enMode.Update;
         
        }



        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = -1;

            bool IsFound = clsApplicationData.GetApplicationInfoByID
                                (
                                    ApplicationID, ref ApplicantPersonID,
                                    ref ApplicationDate, ref ApplicationTypeID,
                                    ref ApplicationStatus, ref LastStatusDate,
                                    ref PaidFees, ref CreatedByUserID
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsApplication(ApplicationID, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, CreatedByUserID,
                                                                   ApplicantPersonID,  PaidFees,  ApplicationDate );
            else
                return null;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicationTypeID, this.LastStatusDate,
                                                                                     this.CreatedByUserID,(byte)this.ApplicationStatus, this.ApplicantPersonID, this.PaidFees, this.ApplicationDate);

            return (this.ApplicationID != -1);


        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID,this.ApplicationTypeID, this.LastStatusDate,
                                                                                     this.CreatedByUserID, (byte)this.ApplicationStatus, this.ApplicantPersonID, this.PaidFees, this.ApplicationDate);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 2);
        }

        public bool SetCompleted()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }
        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;
        }

        public bool DeleteBaseApplication()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);    
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }


    }
}
