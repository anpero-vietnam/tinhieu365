using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dal;
using Models;
using Web.Mvc.Controllers;

namespace admin.Controllers
{
    public class ExportController :BaseController
    {   // GET: Export
       
        public void ExportCustomer()
        { 
            if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId, AEnum.UserRole.CanViewAnalytic))
            {
                Dal.Profile.AddressBook ad = new Dal.Profile.AddressBook();
                int outPut = 0;
                List<Contact> listContact = ad.SeachAddressBook(1, 999999, "", out outPut);
                Ultil.IConvertHelper<Contact> cv = new Ultil.IConvertHelper<Contact>();
                DataTable table = cv.CreateDataTable(listContact);
                table.Columns.Remove("UserId");                
                table.Columns.Remove("LocationId");
                string mappig = @"District>Quận / Huyện,Province>Tỉnh thành";
                base.Export("Danh sách khách hàng năm " + DateTime.Now.Year, table, DateTime.MinValue, DateTime.Now,null, mappig);
            }
        }
      
    
    }
}