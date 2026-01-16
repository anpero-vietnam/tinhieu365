using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
   public class Rate
    {
        string rfId, customerName, ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string RfId
        {
            get { return rfId; }
            set { rfId = value; }
        }
        int avgRate;

        public int AvgRate
        {
            get { return avgRate; }
            set { avgRate = value; }
        }
       public Rate()
       {
           RfId = null;
           CustomerName = null;
           Ip = null;
           AvgRate = 0;
       }
        public DataTable getRate(String RfID) {           
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@rfId", SqlDbType.VarChar, 20);
            paramList[0].Value = RfID;           
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable tables = ds.executeSelect("getRate", paramList);
            if (tables!=null && tables.Rows.Count > 0)
            {

                AvgRate = Convert.ToInt32(tables.Rows[0]["avgRate"]);               
            }
            return tables;
        
        }
        public int DelRate(String RfID)
        {
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@rfId", SqlDbType.VarChar, 20);
            paramList[0].Value = RfID;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("delRate", paramList);            
        }
        public int updateRate(string rfId, string customerName,String tittle,String ip,decimal ratePoin)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[5];
                paramList[0] = new SqlParameter("@rfid", SqlDbType.VarChar, 20);
                paramList[0].Value = rfId;
                paramList[1] = new SqlParameter("@Cname", SqlDbType.NVarChar, 30);
                paramList[1].Value = customerName;
                paramList[2] = new SqlParameter("@content", SqlDbType.NVarChar, 500);
                paramList[2].Value = tittle;
                paramList[3] = new SqlParameter("@ip", SqlDbType.VarChar, 30);
                paramList[3].Value = ip;
                paramList[4] = new SqlParameter("@p", SqlDbType.Decimal);
                paramList[4].Value = ratePoin;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return  ds.executeUpdate("ADDRATE", paramList);
            }
            catch (Exception ex)
            {                
                Dal.errorManager er = new Dal.errorManager();
                er.insertError(ex.Message, "from Dal.updateRate");
                return 0;
            }
        }
    }
 
}
