using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dal.Dapper;
using Models;
namespace Dal.Profile
{
    
    public class AddressBook : ConnectionProxy<Contact>
    {
        
  
        public DataTable AdminSeachAddressBook(String _detail, String uid)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Detail", SqlDbType.NVarChar);
                paramList[0].Value = _detail;
                paramList[1] = new SqlParameter("@uid", SqlDbType.NVarChar);
                paramList[1].Value = uid;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("AdminseachAddressBook", paramList);
            }
            catch (Exception)
            {

                return null;
            }

        }
        public List<Contact> GetContactsByListId(List<int> listAddress)
        {

            string addressList = string.Join(",", listAddress.Select(x=>x.ToString()).ToArray());
            Dictionary<string, object> ParamList = new Dictionary<string, object>();
            ParamList.Add("@addressList", addressList);                
            return Select("sp_getContactList",ParamList);
        }
        public List<Contact> SeachAddressBook(int currentPage, int pageSite, string keyWord,  out int record)
        {

            int beginRow = (currentPage-1) * pageSite ;
            int endRow = (currentPage - 1) * pageSite + pageSite-1;
            if (string.IsNullOrEmpty(keyWord)) { keyWord = ""; }
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@beginRow", beginRow);
                ParamList.Add("@endRow", endRow);
                ParamList.Add("@keyWord", keyWord);                
                return Select("getAllAddressOfStore",ParamList, "@output", out record);
            }
            catch (Exception ex)
            {
                SysNotify sn = new SysNotify();
                sn.AddSysNotify("Hệ thống phát hiện lỗi", "Hệ thống tự động phát hiện lỗi tại GetAllUser : " + ex.Message);
                record = 0;
                return null;
            }

        }
        public DataTable SeachAddressBook(String _detail, String uid)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Detail", SqlDbType.NVarChar);
                paramList[0].Value = _detail;
                paramList[1] = new SqlParameter("@uid", SqlDbType.NVarChar);
                paramList[1].Value = uid;
                

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("seachAddressBook", paramList);
            }
            catch (Exception)
            {

                return null;
            }

        }
        public int DelAddress(int id, int uid)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
                paramList[0].Value = id;
                paramList[1] = new SqlParameter("@uid", SqlDbType.Int);
                paramList[1].Value = uid;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("delAddress", paramList);
            }
            catch (Exception)
            {

                return 0;
            }

        }
        public int UpdateAddress(Models.Contact contact)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@id", contact.Id);
                ParamList.Add("@Phone", contact.Phone);
                ParamList.Add("@Name", contact.Name);
                ParamList.Add("@Mail", contact.Mail);
                ParamList.Add("@Address", string.IsNullOrEmpty(contact.Address)? contact.Address: contact.Address.Replace("\n",@"<br/>"));                
                ParamList.Add("@TaxCode", contact.TaxCode);
                ParamList.Add("@locationId", contact.LocationId);
                ParamList.Add("@Province", contact.Province);
                ParamList.Add("@District", contact.District);
                ParamList.Add("@IdCard", contact.IdCard);                
                return (int)base.ExecuteScalar("sp_UpdateAddress",ParamList);

            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                var x= ms.SendMessagesToRoleAsync("admin",false, "Error from updateAddress",ex.Message,"0").Result;
                return 0;
            }

        }
        /// <summary>
        /// id uid=0 to get all user
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //public Models.Contact GetAddressByIdAndUid(int id,int agenID)
        //{
        //    try
        //    {
        //        SqlParameter[] paramList = new SqlParameter[2];
        //        paramList[0] = new SqlParameter("@agenID", SqlDbType.Int);
        //        paramList[0].Value = uid;
        //        paramList[1] = new SqlParameter("@Id", SqlDbType.Int);
        //        paramList[1].Value = id;             
        //        DataTable table = ds.executeSelect("sp_getAddressById", paramList);
        //    }
        //    catch (Exception)
        //    {
        //         return new Models.Contact()                
        //    }

        //}
        public Contact GetAddressById(int id)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@id", id);                
                return  base.SelectSingle("sp_getAddressById",ParamList);
            }
            catch (Exception)
            {
               // throw;
                return new Models.Contact();
            }
        }
        public Contact GetAddressByIdCard(string id)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@id", id);
                
                return base.SelectSingle("sp_getAddressByIdCard",ParamList);
            }
            catch (Exception)
            {
                return new Models.Contact();
            }
        }
        public DataTable GetAddressByUser(int uid, int st)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@uid", SqlDbType.Int);
                paramList[0].Value = uid;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getAddressByUser", paramList);

            }
            catch (Exception)
            {

                return null;
            }

        }
       
        public int AddOrUppdate(Contact contact)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@phone",string.IsNullOrEmpty(contact.Phone)?string.Empty: contact.Phone.Replace(" ","").Trim());
                ParamList.Add("@Name", contact.Name);
                ParamList.Add("@mail", contact.Mail);
                ParamList.Add("@address", contact.Address);
                ParamList.Add("@iud", contact.UserId);
                ParamList.Add("@taxCode", contact.TaxCode);
                ParamList.Add("@idCard", string.IsNullOrEmpty(contact.IdCard) ? string.Empty : contact.IdCard.Replace(" ", "").Trim());
                ParamList.Add("@locationId", contact.LocationId);
                ParamList.Add("@District", contact.District);
                ParamList.Add("@Province", contact.Province);
                
                var obj = base.ExecuteScalar("sp_AddAddress",ParamList);
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {

                return 0;
            }
          
        }
        public int AddAddress(int uid, string name, string mail, string phone, string address,string taxCode)
        {
            try
            {
                if (string.IsNullOrEmpty(taxCode))
                {
                    taxCode = "";
                }
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@phone", phone);
                ParamList.Add("@Name", name);
                ParamList.Add("@mail", mail);
                ParamList.Add("@address", address);
                ParamList.Add("@iud", uid);
                ParamList.Add("@taxCode", taxCode);
                return base.ExecuteProc("AddAddress",ParamList);
            }
            catch (Exception)
            {

                return 0;
            }

        }
        #region contructer
        //string taxCode, userCode, paymentCard, identityCard, name, phone, maill, address, phoneActiveToken, mailActiveToken;
        //int isActive, id, uid, phoneActiveSST, mailActiveSST;
        //public string MailActiveToken
        //{
        //    get { return mailActiveToken; }
        //    set { mailActiveToken = value; }
        //}
        //public string PhoneActiveToken
        //{
        //    get { return phoneActiveToken; }
        //    set { phoneActiveToken = value; }
        //}
        //public int MailActiveSST
        //{
        //    get { return mailActiveSST; }
        //    set { mailActiveSST = value; }
        //}
        //public int PhoneActiveSST
        //{
        //    get { return phoneActiveSST; }
        //    set { phoneActiveSST = value; }
        //}
        //public string TaxCode
        //{
        //    get { return taxCode; }
        //    set { taxCode = value; }
        //}
        //public String UserCode
        //{
        //    get { return userCode; }
        //    set { userCode = value; }
        //}
        //public String PaymentCard
        //{
        //    get { return paymentCard; }
        //    set { paymentCard = value; }
        //}       
        //public String IdentityCard
        //{
        //    get { return identityCard; }
        //    set { identityCard = value; }
        //}
        //public int IsActive
        //{
        //    get { return isActive; }
        //    set { isActive = value; }
        //}        
        //public int Id
        //{
        //    get { return id; }
        //    set { id = value; }
        //}        
        //public String Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}
        
        //public String Phone
        //{
        //    get { return phone; }
        //    set { phone = value; }
        //}
        
        //public String Maill
        //{
        //    get { return maill; }
        //    set { maill = value; }
        //}
      
        //public String Address
        //{
        //    get { return address; }
        //    set { address = value; }
        //}
        //public int Uid
        //{
        //    get { return uid; }
        //    set { uid = value; }
        //}
        #endregion
        //public string updatePhoneAndMailActive(int addId,int sst,string type,string uid)
        //{
        //    try
        //    {
        //        SqlParameter[] paramList = new SqlParameter[5];
        //        paramList[0] = new SqlParameter("@addId", SqlDbType.Int);
        //        paramList[0].Value = addId;
        //        paramList[1] = new SqlParameter("@sst", SqlDbType.Int);
        //        paramList[1].Value = sst;
        //        paramList[2] = new SqlParameter("@type", SqlDbType.VarChar,10);
        //        paramList[2].Value = type;
        //        paramList[3] = new SqlParameter("@uids", SqlDbType.VarChar, 10);
        //        paramList[3].Value = uid;
        //        paramList[4] = new SqlParameter("@time", SqlDbType.VarChar, 14);
        //        paramList[4].Value = Ultil.Times.GetyyyyMMddhhmmNow();
        //        Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
        //        return ds.executeScalaryString("updatePhoneAndMailActive", paramList);
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }

        //}

        // public int ActivePhone(int addId,int sst,string type,string uid,string token)
        //{
        //    try
        //    {
        //        SqlParameter[] paramList = new SqlParameter[5];
        //        paramList[0] = new SqlParameter("@addId", SqlDbType.Int);
        //        paramList[0].Value = addId;
        //        paramList[1] = new SqlParameter("@sst", SqlDbType.Int);
        //        paramList[1].Value = sst;
        //        paramList[2] = new SqlParameter("@type", SqlDbType.VarChar,10);
        //        paramList[2].Value = type;
        //        paramList[3] = new SqlParameter("@uids", SqlDbType.VarChar, 10);
        //        paramList[3].Value = uid;
        //        paramList[4] = new SqlParameter("@Token", SqlDbType.VarChar, 14);
        //        paramList[4].Value =token;
        //        Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
        //        return ds.executeUpdate("ActivePhone", paramList);
        //    }
        //    catch (Exception)
        //    {

        //        return 0;
        //    }

        //}
    }

}


