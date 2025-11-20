using DVLD_DataAcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsUsers
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public bool IsActive { get; set; }


        public clsUsers()
        {
            UserName = "";
            Password = "";
            UserID = -1;
            PersonID = -1;
            IsActive = false;
           Mode = enMode.AddNew;    
        }

        private clsUsers( string userName, string password, int userID, int personID, bool isActive )
        {
           
            UserName = userName;
            Password = password;
            UserID = userID;
            PersonID = personID;
            this.PersonInfo = clsPerson.FindPerson(PersonID);
            IsActive = isActive;
            Mode = enMode.Update;
        }
        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();  
        }
        public static clsUsers FindByUsernameAndPassword(string UserName,string Password)
        {
            int userID = -1, personID = -1;
            bool isActive = false;
            if (!clsUsersData.GetUserInfoByUsernameAndPassword(UserName, Password, ref isActive, ref personID, ref userID))
            {
                return null;
            }
            return new clsUsers(UserName, Password, userID, personID, isActive);

        }
        public static clsUsers FindByUserName(string UserName)
        {
            string password="";
            int userID = -1, personID = -1;
            bool isActive = false;
            if (!clsUsersData.GetUserInfoByUserName(UserName,ref password,ref isActive,ref personID,ref userID))
            {
                return null;
            }
            return new clsUsers(UserName,  password, userID,  personID, isActive);

        }
        public static clsUsers FindByUserID(int UserID)
        {
            string password = "", UserName="";
            int  personID = -1;
            bool isActive = false;
            if (!clsUsersData.GetUserInfoByUserID(UserID, ref password, ref isActive, ref personID, ref UserName))
            {
                return null;
            }
            return new clsUsers(UserName, password, UserID, personID, isActive);

        }
        public static clsUsers FindByPersonID(int PersonID)
        {
            string password = "", UserName = "";
            int UserID = -1;
            bool isActive = false;
            if (!clsUsersData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref password, ref isActive))
            {
                return null;
            }
            return new clsUsers(UserName, password, UserID, PersonID, isActive);

        }
        public static bool IsUserExistForUserName(string UserName)
        {
            if (clsUsersData.IsUserExistForUserName(UserName))
            {
                return true;
            }
            return false;
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            if (clsUsersData.IsUserExistForPersonID(PersonID))
            {
                return true;
            }
            return false;
        }

        public static bool IsUserExistForUserID(int UserID)
        {
            if (clsUsersData.IsUserExistForUserID(UserID))
            {
                return true;
            }
            return false;
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUsersData.DeleteUser(UserID); 
        }

      private bool _AddNewUser()
        {
            this.UserID = clsUsersData.AddNewUser(this.UserName, this.Password, this.IsActive, this.PersonID);
            return (this.UserID != -1);
                
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(this.UserID,this.UserName, this.Password, this.IsActive, this.PersonID);
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }


    }
}
