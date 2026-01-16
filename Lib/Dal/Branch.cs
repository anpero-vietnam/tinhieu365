using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
  public class Branch
    {
        string name;
        int id ;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public Branch()
        {
            Name = null;
            Id = 0;
        }
        public Branch(string Name,int ID)
        {
            this.Name = Name;
            Id = ID;
        }
        public int AddBranch(string name, int StoreId)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@name", SqlDbType.NVarChar, 300);
                paramList[0].Value = name;
                paramList[1] = new SqlParameter("@storeId", SqlDbType.Int);
                paramList[1].Value = StoreId;            
                DatabaseAccess ds = new DatabaseAccess();
                return ds.executeScalary("addBranch", paramList);

            }
            catch (Exception)
            {
                return 0;
            }

        }
        // chưa làm xong
  
        public List<Branch> GetBranchByStoreID(int StoreId)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];              
                paramList[0] = new SqlParameter("@STOREID", SqlDbType.Int);
                paramList[0].Value = StoreId;
                DatabaseAccess ds = new DatabaseAccess();
                DataTable table= ds.executeSelect("GET_BRANCH_BY_STORE_ID", paramList);
                List<Branch> list = new List<Branch>();
                if(table!=null && table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        list.Add(new Branch(table.Rows[i]["name"].ToString(), Convert.ToInt32(table.Rows[i]["id"])));
                    }
                    return list;
                }else { return null; }
               

            }
            catch (Exception)
            {

                return null; 
            }
        }
        public int UpdateBanch(string name,int storeId,int branchID)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[3];
                paramList[0] = new SqlParameter("@name", SqlDbType.NVarChar,300);
                paramList[0].Value = name;
                paramList[1] = new SqlParameter("@storeId", SqlDbType.Int);
                paramList[1].Value = storeId;
                paramList[2] = new SqlParameter("@id", SqlDbType.Int);
                paramList[2].Value = branchID;
                DatabaseAccess ds = new DatabaseAccess();
                return ds.executeUpdate("UpdateBranch", paramList);                
            }
            catch (Exception)
            {

                return 0;
            }

        }
        public int DeleteBanch(int storeId, int branchID)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
            
                paramList[1] = new SqlParameter("@storeId", SqlDbType.Int);
                paramList[1].Value = storeId;
                paramList[0] = new SqlParameter("@branchId", SqlDbType.Int);
                paramList[0].Value = branchID;
                DatabaseAccess ds = new DatabaseAccess();
                return ds.executeUpdate("DeleteBranch", paramList);
            }
            catch (Exception)
            {

                return 0;
            }

        }
    }
}
