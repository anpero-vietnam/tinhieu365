using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    public class Comment
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

        public Comment()
        {
            //
            // TODO: Add constructor logic here
            //
            //conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //conn.Open();
        }
        public int insertComment(String commentDetail, String userID, String newid, String tittle, String email)
        {
            try
            {

                int ids = Convert.ToInt32(newid);
                SqlParameter[] paramList = new SqlParameter[5];
                paramList[0] = new SqlParameter("@TinId", SqlDbType.Int, 32);
                paramList[0].Value = newid;
                paramList[1] = new SqlParameter("@Title", SqlDbType.NVarChar, 4000);
                paramList[1].Value = tittle;
                paramList[2] = new SqlParameter("@comment", SqlDbType.NText, 8000);
                paramList[2].Value = commentDetail;
                paramList[3] = new SqlParameter("@userId", SqlDbType.NVarChar, 300);
                paramList[3].Value = userID;
                paramList[4] = new SqlParameter("@Email", SqlDbType.NVarChar, 300);
                paramList[4].Value = email;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                ds.executeUpdate("insertComment", paramList);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        public DataTable GetAllComentByNews(int newsId, int currentPage, int pageSite)
        {
            int beginRow = (currentPage - 1) * pageSite + currentPage;
            int endRow = (currentPage - 1) * pageSite + currentPage + pageSite;
            int ids = Convert.ToInt32(newsId);
            SqlParameter[] paramList = new SqlParameter[3];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = ids;
            paramList[1] = new SqlParameter("@beginRow", SqlDbType.Int, 32);
            paramList[1].Value = beginRow;
            paramList[2] = new SqlParameter("@EndRow", SqlDbType.Int, 32);
            paramList[2].Value = endRow;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable tables = ds.executeSelect("getAllCommentByNews", paramList);
            return tables;
        }
        public DataTable top5Commetn()
        {
            try
            {             
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("top5Commetn");
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int BadcommentUpdate(int id)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = id;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("BadcommentUpdate", paramList);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int DelCommetnById(int id)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = id;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("delCommentByid", paramList);
            }
            catch (Exception)
            {
                return 0;
            }
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
        public int updateComment(int id,Boolean good,int like, int unlike,Boolean publish)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[5];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = id;
      
                paramList[1] = new SqlParameter("@good", SqlDbType.Bit);
                paramList[1].Value = good;
                
                paramList[2] = new SqlParameter("@likes", SqlDbType.Int, 32);
                paramList[2].Value = like;
                
                paramList[3] = new SqlParameter("@unlikes", SqlDbType.Int, 32);
                paramList[3].Value = unlike;
                
                paramList[4] = new SqlParameter("@publish", SqlDbType.Bit);
                paramList[4].Value = publish;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("SPUpdateComment", paramList);

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int getCommetnById(int id)
        {
            try
            {

                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = id;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getCommentById", paramList);
                Tintucid = Convert.ToInt32(tables.Rows[0]["tinTucId"]);
                Tittle = tables.Rows[0]["Title"].ToString();
                Detail = tables.Rows[0]["Detail"].ToString();
                UserId = tables.Rows[0]["UserId"].ToString();
                Dates = tables.Rows[0]["dates"].ToString();
                Good = tables.Rows[0]["good"].ToString();
                Like = Convert.ToInt32(tables.Rows[0]["likes"]);
                Unlike = Convert.ToInt32(tables.Rows[0]["unlikes"]);
                SendIp = tables.Rows[0]["senderip"].ToString();
                Publish = Convert.ToBoolean(tables.Rows[0]["publish"].ToString());
                Email = tables.Rows[0]["Email"].ToString();
                return tables.Rows.Count;
            }
            catch (Exception)
            {
                return 0;
            }

        }
      public int UpdateLike(int CM_ID,String U_ID,String U_IP, int isLike,int N_ID)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[5];
                paramList[0] = new SqlParameter("@cm_id", SqlDbType.Int, 32);
                paramList[0].Value = CM_ID;
      
                paramList[1] = new SqlParameter("@u_id", SqlDbType.NVarChar,300);
                paramList[1].Value = U_ID;

                paramList[2] = new SqlParameter("@u_ip", SqlDbType.NVarChar, 300);
                paramList[2].Value = U_IP;
                
                paramList[3] = new SqlParameter("@like", SqlDbType.Int,32);
                paramList[3].Value = like;
                
                paramList[4] = new SqlParameter("@n_ID", SqlDbType.Int,32);
                paramList[4].Value = N_ID;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("UpdateLike", paramList);

            }
            catch (Exception)
            {
                return 0;
            }
        }
   

    }
}
