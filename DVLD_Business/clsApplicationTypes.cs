using DVLD_DataAcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplicationTypes
    {

       public int ApplicationTypeID{ get; set; }
        public float ApplicationFees { get; set; }
        public string ApplicationTypeTitle { get; set; }

       public clsApplicationTypes(int applicationTypeID, float applicationFees, string applicationTypeTitle   )
        {
            ApplicationTypeID = applicationTypeID;
            ApplicationFees = applicationFees;
            ApplicationTypeTitle = applicationTypeTitle;
         
        }

         private bool _UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID,this.ApplicationFees,this.ApplicationTypeTitle);    
        }

        public  bool Save()
        {
            if (_UpdateApplicationType())
            {
                return true;
            }
            return false;
        }
        public static clsApplicationTypes FindApp(int ApplicationTypeID)
        {
            float Fees =0;
            string Title="";
            if (!clsApplicationTypesData.FindApp(ApplicationTypeID,ref Fees,ref Title))
            {
                return null;    
            }
            return new clsApplicationTypes(ApplicationTypeID,Fees,Title);   
        }


        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();    
        }
    }
}
