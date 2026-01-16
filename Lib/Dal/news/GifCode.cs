using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    public class GifCode
    {
        Dal.DatabaseAccess ds;
        /// <summary>
        /// thêm một code
        /// </summary>
        /// <param name="newId">Gắn vào một tin </param>
        /// <param name="tittle">tiêu đề của mã gif</param>
        /// <param name="endDate">ngày hết hạn</param>
        /// <param name="emmail">email người nhận</param>
        /// <param name="code">code</param>
        /// <param name="stt">trạng thái</param>
        /// <returns></returns>
        public int AddGifCode(int newId,String tittle,String endDate,String emmail,String code)
        {
            try
            {    makeRandom random = new makeRandom();
                
                 SqlParameter[] paramList = new SqlParameter[8];
                 paramList[0] = new SqlParameter("@NEW_ID", SqlDbType.Int, 32);
                 paramList[0].Value = newId;
                 paramList[1] = new SqlParameter("@Tittle", SqlDbType.NVarChar, 500);
                 paramList[1].Value = tittle;
                 paramList[2] = new SqlParameter("@END_DT", SqlDbType.BigInt);
                 paramList[2].Value = endDate;
                 paramList[3] = new SqlParameter("@LOCK_DT", SqlDbType.BigInt);
                 paramList[3].Value = Dal.Times.GetyyyyMMddhhmmNow();
                 paramList[4] = new SqlParameter("@Email", SqlDbType.VarChar,300);
                 paramList[4].Value = emmail;
                 paramList[5] = new SqlParameter("@Code", SqlDbType.VarChar,300);
                 paramList[5].Value = code;
                 paramList[6] = new SqlParameter("@ActiveCode", SqlDbType.VarChar,300);
                 String rd = random.makeRanDom();
                 paramList[6].Value = rd;
                 paramList[7] = new SqlParameter("@G_STT", SqlDbType.Int, 32);
                 paramList[7].Value = "0";
                 Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("addGifCode", paramList);

            }
            catch (Exception)
            {

                return 0;
            }
        }
        /// <summary>
        /// cập nhật gifcode email và thời gian hiện tại cho người dùng active gifcode qua email
        /// </summary>
        /// <param name="newID">id của tin tức</param>
        /// <param name="email">email của người dùng</param>
        /// <returns>nếu thành con trả về > 1</returns>
        public String updateGifToWaiting1Active(int newID, String email,String ip,String cpu_Name) {        
                    
                 SqlParameter[] paramList = new SqlParameter[5];
                 paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                 paramList[0].Value = newID;
                 paramList[1] = new SqlParameter("@Email", SqlDbType.NVarChar, 500);
                 paramList[1].Value = email;
                 paramList[2] = new SqlParameter("@curentTime", SqlDbType.BigInt);
                 paramList[2].Value = Dal.Times.GetyyyyMMddhhmmNow();
                 paramList[3] = new SqlParameter("@Ip", SqlDbType.VarChar,300);
                 paramList[3].Value =ip;
                 paramList[4] = new SqlParameter("@CPU_NAME", SqlDbType.VarChar, 300);
                 paramList[4].Value = cpu_Name;
                 Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                 return ds.executeScalaryString("updateGifToWaiting1Active", paramList);



        }
        /// <summary>
        /// trả về nhứng gifcode chưa active và chưa tạm khóa trong 10 phút
        /// </summary>
        /// <param name="N_ID"></param>
        /// <returns></returns>
        public int CountActiveGifByNewId(int N_ID)
        {
           
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = N_ID;
            paramList[1] = new SqlParameter("@CR_Time", SqlDbType.BigInt, 32);
            paramList[1].Value = Dal.Times.GetyyyyMMddhhmmNow();
            ds = new Dal.DatabaseAccess();
            return ds.executeScalary("CountActiveGifByNewId", paramList);

        }
        /// <summary>
        /// lấy gifcode và cập nhật trạng thái của gif thành 1
        /// </summary>
        /// <param name="id">nhập vào mã active</param>
        /// <returns></returns>
        public DataTable getGifCodeByActiveCode(String id)
        {
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@Id", SqlDbType.VarChar,500);
            paramList[0].Value = id;
           
            ds = new Dal.DatabaseAccess();
            return ds.executeSelect("GetGifCodeByActiveCode", paramList);

        }
        
        /// <summary>
        /// kiểm tra xem người dùng có đăng ký và chờ active không 
        /// </summary>
        /// <param name="tacelogi"></param>
        /// <returns></returns>
        public int isUserWaitingActive(int tacelogi, String U_ID)
        {
            
            SqlParameter[] paramList = new SqlParameter[3];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = tacelogi;
            paramList[1] = new SqlParameter("@CR_Time", SqlDbType.BigInt, 32);
            paramList[1].Value = Dal.Times.GetyyyyMMddhhmmNow();
            paramList[2] = new SqlParameter("U_ID", SqlDbType.NVarChar, 300);
            paramList[2].Value = U_ID;
            ds = new Dal.DatabaseAccess();
            return ds.executeScalary("isUserWaitingActive", paramList);
        }
        /// <summary>
        /// Kiểm tra xem ip này đã lấy gifcode của tin này chưa
        /// </summary>
        /// <param name="tacelogi">id của tin chứa gifcode</param>
        /// <param name="U_IP">Ip hiện tại của người dùng</param>
        /// <returns></returns>
          public int isIpActive(int tacelogi, String U_IP)
        {
           
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = tacelogi;
            paramList[1] = new SqlParameter("@Ip", SqlDbType.VarChar, 300);
            paramList[1].Value = U_IP;
            ds = new Dal.DatabaseAccess();
            return ds.executeScalary("isIpGetGifCode", paramList);
        }


        public int isUserGetGifCode(int NewID, String U_ID)
        {
           
            SqlParameter[] paramList = new SqlParameter[3];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = NewID;
            paramList[1] = new SqlParameter("@CR_Time", SqlDbType.BigInt, 32);
            paramList[1].Value = Dal.Times.GetyyyyMMddhhmmNow();
            paramList[2] = new SqlParameter("U_ID", SqlDbType.NVarChar, 300);
            paramList[2].Value = U_ID;
            ds = new Dal.DatabaseAccess();
            return ds.executeScalary("isUserGetGifCode", paramList);
        }
        public String getActiveCodeByMail(String N_ID)
        {
           
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@Email", SqlDbType.VarChar,300);
            paramList[0].Value = N_ID;
            paramList[1] = new SqlParameter("@CR_Time", SqlDbType.BigInt, 32);
            paramList[1].Value = Dal.Times.GetyyyyMMddhhmmNow();
            ds = new Dal.DatabaseAccess();
            DataTable table= ds.executeSelect("getActiveCodeByMail", paramList);
            return table.Rows[0]["activeCode"].ToString();
        }

    
    }

}
