using AModul.Dapper;
using Models.Modul.Common;
using System.Collections.Generic;

namespace AModul.Common
{
    public class ContactAnalyticControl : ConnectionProxy<ContactAnalyticItem>
    {
        public ContactAnalyticItem ContactAnalytic()
        {   
            return base.SelectSingle("[sp_AnalyticContact]");
        }
    }
}
