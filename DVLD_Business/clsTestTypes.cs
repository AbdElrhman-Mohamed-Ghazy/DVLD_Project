using DVLD_DataAcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestTypes
    {
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestTypes.enTestType ID { set; get; }
        public decimal TestTypeFees { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }

   
        
        public clsTestTypes(clsTestTypes.enTestType ID, decimal TestTypeFees, string TestTypeTitle, string TestTypeDescription  )
        {
            this.ID = ID;
            this.TestTypeFees = TestTypeFees;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
        }

        private bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType((int)this.ID, this.TestTypeFees, this.TestTypeTitle,this.TestTypeDescription);
        }

        public bool Save()
        {
            if (_UpdateTestType())
            {
                return true;
            }
            return false;
        }
        public static clsTestTypes FindTest(clsTestTypes.enTestType ID)
        {
            decimal Fees = 0;
            string Title = "", Description = "";
            if (!clsTestTypesData.FindTest((int)ID, ref Fees, ref Title,ref Description))
            {
                return null;
            }
            return new clsTestTypes(ID, Fees, Title, Description);
        }


        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
    }
}
