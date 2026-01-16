using AModul.Dapper;
using Models;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AModul.Product
{
  
    public class OrderControl : ConnectionProxy<OrderModel>
    {
        public int UpdateOD(int id, int sst, int isDel, string detail)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@Id", id);
                paramlist.Add("@sst", sst); 
                paramlist.Add("@isDelete", isDel);
                paramlist.Add("@Detail", detail);                
                return base.ExecuteProc("updateOD",paramlist);

            }
            catch (Exception)
            {
                return 0;
            }

        }

        public int GetOderTableAnalytic(string createdId, String id, string isCheckEd, string status, string dateFrom, String dateTo, String customId)
        {
            if (String.IsNullOrEmpty(id))
            {

                id = "%";
            }
            if (String.IsNullOrEmpty(dateFrom))
            {

                dateFrom = Ultil.Times.GetFistDayOfMonthNow_yyyyMMddhhmm(false);
            }
            else
            {

                dateFrom = Ultil.Times.GetyyyyMMddhhmm(dateFrom, true);
            }
            if (String.IsNullOrEmpty(dateTo))
            {

                dateTo = Ultil.Times.GetyyyyMMddhhmmNow(false);
            }
            else
            {
                dateTo = Ultil.Times.GetyyyyMMddhhmm(dateTo, false);
            }
            if (String.IsNullOrEmpty(customId))
            {

                customId = "%";
            }
            if (String.IsNullOrEmpty(status))
            {

                status = "%";
            }
            if (String.IsNullOrEmpty(isCheckEd))
            {

                isCheckEd = "%";
            }
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@isCheckEd", SqlDbType.VarChar, 2);
                param[0].Value = isCheckEd;
                param[1] = new SqlParameter("@status", SqlDbType.VarChar, 2);
                param[1].Value = status;
                param[2] = new SqlParameter("@dateFrom", SqlDbType.VarChar, 14);
                param[2].Value = dateFrom;
                param[3] = new SqlParameter("@dateTo", SqlDbType.VarChar, 14);
                param[3].Value = dateTo;
                param[4] = new SqlParameter("@customId", SqlDbType.VarChar, 10);
                param[4].Value = customId;
                param[5] = new SqlParameter("@Id", SqlDbType.VarChar, 10);
                param[5].Value = id;
                param[6] = new SqlParameter("@createdId", SqlDbType.VarChar, 15);
                param[6].Value = createdId;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeScalary("GetOderTableAnalytic", param);
            }
            catch (Exception)
            {

                return 0;
            }


        }
        public List<OrderModel> GetOderTable(string id, string status, string dateFrom, String dateTo, int curentPage, int pageSite, String customId, string seria, out int rs)
        {

            if (!string.IsNullOrEmpty(dateFrom))
            {

                dateFrom = Ultil.Times.GetyyyyMMddhhmm(dateFrom, true);
            }            
            if (!string.IsNullOrEmpty(dateTo))
            {

                dateTo = Ultil.Times.GetyyyyMMddhhmm(dateTo, false);
            }            
            try
            {
                int beginRow = (curentPage - 1) * pageSite + 1;
                int endRow = (curentPage - 1) * pageSite + 1 + pageSite;
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@status", status??string.Empty);
                paramlist.Add("@dateFrom", dateFrom ?? string.Empty);
                paramlist.Add("@dateTo", dateTo ?? string.Empty);
                paramlist.Add("@beginRow", beginRow);
                paramlist.Add("@endRow", endRow);
                paramlist.Add("@customId", customId ?? string.Empty);
                paramlist.Add("@Id", id ?? string.Empty);
                paramlist.Add("@seria", seria ?? string.Empty);
                
               return base.Select("[sp_SearchOder]","@output",out rs,paramlist);
            }
            catch (Exception) 
            {
                rs = 0;
                return null;
            }


        }
       

        public  OrderModel GetOdById(int id)
        {
            OrderModel model= new OrderModel();
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Id", id);
            model= SelectSingle("sp_GetOrderById",paramlist);
            if (model != null)
            {
                Dal.Profile.AddressBook ad = new Dal.Profile.AddressBook();
                var contact = ad.GetAddressById(model.CustomId);
                if (contact != null)
                {
                    model.CustomName = contact.Name;
                    model.CusTomPhone = contact.Phone;
                    model.CustomMail = contact.Mail;
                    model.CustomAddres = contact.Address;
                }
                try
                {
                    
                }
                catch (Exception)
                {

                }
            }
            return model;
        }
      
        public int CreateOrder(String CustomID, string dateCreate, string staff, string sessionId, String status, int tranfertype, decimal shopCost, string detail, string storeId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                param[0] = new SqlParameter("@CustomID", SqlDbType.VarChar, 10);
                param[0].Value = CustomID;
                param[1] = new SqlParameter("@DateCreate", SqlDbType.VarChar, 14);
                param[1].Value = dateCreate;
                param[2] = new SqlParameter("@staff", SqlDbType.Int);
                param[2].Value = staff;              
                param[4] = new SqlParameter("@sst", SqlDbType.Int);
                param[4].Value = status;
                param[5] = new SqlParameter("@tranfertype", SqlDbType.Int);
                param[5].Value = tranfertype;
                param[6] = new SqlParameter("@ship", SqlDbType.Decimal);
                param[6].Value = shopCost;
                param[7] = new SqlParameter("@Detail", SqlDbType.NVarChar, 300);
                param[7].Value = detail;
                param[8] = new SqlParameter("@token", SqlDbType.NVarChar, 300);
                String token = Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
                if (token.Length > 32) { token = token.Substring(0, 32); }
                param[8].Value = token;

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeScalary("addOD", param);
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                ms.SendMessagesToRole("admin", false, "Lỗi thu thập từ addOd", ex.Message, "0");
                return 0;
            }

        }
        public int CreateOrderV2(int CustomID, string dateCreate, string staff, string status, int shippingMethod, int paymentMethod, decimal shippingFee, string detail, int agenId)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@CustomID", CustomID);
                paramlist.Add("@DateCreate", dateCreate);
                paramlist.Add("@staff", staff);
                paramlist.Add("@st", agenId);
                paramlist.Add("@sst", status);
                paramlist.Add("@shippingMethod", shippingMethod);
                paramlist.Add("@ship", shippingFee);
                paramlist.Add("@Detail", detail);
                string token = Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
                if (token.Length > 32) { token = token.Substring(0, 32); }
                paramlist.Add("@token", token);
                paramlist.Add("@paymentMethod", paymentMethod);
                int insertId = 0;
                base.ExecuteProc("addOD_2", "@insertID",out insertId, paramlist);
                return insertId;
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                ms.SendMessagesToRole("admin", false, "Lỗi thu thập từ addOd_2", ex.Message, "0");
                return 0;
            }

        }

   
    }
}
