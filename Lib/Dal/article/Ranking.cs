using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    public class ranking
    {
        int tintucid;

        public int Tintucid
        {
            get { return tintucid; }
            set { tintucid = value; }
        }
        String tittle;

        public String Tittle
        {
            get { return tittle; }
            set { tittle = value; }
        }
        String detail;

        public String Detail
        {
            get { return detail; }
            set { detail = value; }
        }
        String userId;

        public String UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        String dates;

        public String Dates
        {
            get { return dates; }
            set { dates = value; }
        }
        String good;

        public String Good
        {
            get { return good; }
            set { good = value; }
        }
        int like;

        public int Like
        {
            get { return like; }
            set { like = value; }
        }
        int unlike;

        public int Unlike
        {
            get { return unlike; }
            set { unlike = value; }
        }
        String sendIp;

        public String SendIp
        {
            get { return sendIp; }
            set { sendIp = value; }
        }
        bool publish;

        public bool Publish
        {
            get { return publish; }
            set { publish = value; }
        }
        String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public ranking()
        {
            //
            // TODO: Add constructor logic here
            //
            //conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //conn.Open();
        }
  
        /// <summary>
        /// cập nhật comment
        /// </summary>
        /// <param name="id">id của commetn</param>
        /// <param name="good">trạng thái báo xấu</param>
        /// <param name="like">lượt like sẽ được cộng thêm</param>
        /// <param name="unlike">lượt unlide sẽ được cộng thêm</param>
        /// <param name="publish">trạng thái công cộng</param>
        /// <returns></returns>
      public int UpdateRank(int New_ID,String U_ID,String U_IP, int rate)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[4];
           
      
                paramList[0] = new SqlParameter("@u_id", SqlDbType.NVarChar,300);
                paramList[0].Value = U_ID;

                paramList[1] = new SqlParameter("@u_ip", SqlDbType.NVarChar, 300);
                paramList[1].Value = U_IP;
                
                paramList[2] = new SqlParameter("@like", SqlDbType.Int,32);
                paramList[2].Value = rate;                
                
                paramList[3] = new SqlParameter("@n_ID", SqlDbType.Int,32);
                paramList[3].Value = New_ID;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("UpdateRanking", paramList);


            }
            catch (Exception)
            {
                return 0;
            }
        }
   

    }
}
