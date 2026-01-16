using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEnum
{
    public class UserRole
    {
        public static int Admin = 1;
        public static int CanUpdateProduct = 2;
        public static int CanUpdateAndAddNew = 3;
        public static int CanSale = 4;
        // config web
        public static int CanAddScriptToWeb = 5;        
        public static int CanUpdateTheme = 7;
        public static int CanViewAnalytic = 8;
        public static int CanViewAllCustomer = 26;

        public static int GetRoleIdByName(string name)
        {
            switch (name.ToLower())
            {
                case "admin":
                    return 1;
                case "canupdateproduct":
                    return 2;
                case "canupdateandaddnew":
                    return 3;
                case "cansale":
                    return 4;
                case "canaddscriptoweb":
                    return 5;
                
                case "canupdatetheme":
                    return 7;
                case "canviewanalytic":
                    return 8;
                case "canviewallcustomer":
                    return 26;
                default:
                    return 0;
                    
            }
            
        }
    }
}

