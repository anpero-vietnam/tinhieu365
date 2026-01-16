using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dal
{
    class StoreHepper
    {
    }
    public abstract class RoleName
    {
        public static int IsAdmin = 1;
        public static int canViewCashBook = 2;
        public static int canUpdateCashBook = 3;
        public static int canUpdateNewsContent = 4;
        public static int canUseMailSystem = 5;
        public static int canAddProduct = 6;
        public static int canSale = 7;
        public static int canDelOrder = 8;
     
    }
}
