using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AModul.Dapper;
using Models.Modul.Common;


namespace AModul.Common
{
    public class ContactControl: ConnectionProxy<ContactItem>
    {

        /// <summary>
        /// if reader =-1 get all, contactType can be like %
        /// </summary>
        /// <returns></returns>
        public List<ContactItem> GetContact(int reader, string contactType, int currentPage, int pageSite, out int count)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@reader", reader);
            paramlist.Add("@contactType", contactType);
            paramlist.Add("@currentPage", currentPage);
            paramlist.Add("@pageSite", pageSite);
            
            return base.Select("sp_SearchContact", "output",out count, paramlist);
        }        

        public ContactItem GetConTactByID(int id,int st)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("Id", id);
                
                return base.SelectSingle("[sp_getContactById]",  paramlist);
            }
            catch (Exception)
            {
                return null;
            }

        }        

        public int DelContact(int id)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("ids", id);            
            return base.ExecuteProc("DelContact",paramlist);

        }
        public int UpdateContactToReader(int id,int st)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("Id", id);
            paramlist.Add("st", st);
            return base.ExecuteProc("updateContact",paramlist);
        }
       
        public int AddContact(ContactItem item)
        {            
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@Name", item.Name);
                paramlist.Add("@Email", item.Email);
                paramlist.Add("@adres", item.Address);
                paramlist.Add("@contact", item.Contacts);
                paramlist.Add("@phone", item.Phone);
                paramlist.Add("@clienceIp", item.ClienceIp);
                paramlist.Add("@contactType", "lh");                
                return base.ExecuteProc("AddContact", paramlist);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
  
}
