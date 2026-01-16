using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace Dal.orderMng
{
    public class productStatus
    {
        public int addProductStatus(String name)
        {
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@Name", SqlDbType.NVarChar);
            paramList[0].Value = name;

            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("addProductStatus", paramList);
        }
        public int delProductStatus(int id)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
                paramList[0].Value = id;

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("delProductStatus", paramList);
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                return ms.SendMessagesToRoleAsync("addmin", false, "Lỗi thu thập", ex.Message, "0").Result;
                
            }

        }
        public int UpdateProductSSt(int id, String name)
        {
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
            paramList[0].Value = id;
            paramList[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            paramList[1].Value = name;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("updateProductStatus", paramList);
        }
        public DataTable getProductSSt()
        {

            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeSelect("getProductStatus");
        }
        public productStatus()
        {
            Name = null;
            Id = 0;
        }
        String name;
        int id;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }    
   
 
}
