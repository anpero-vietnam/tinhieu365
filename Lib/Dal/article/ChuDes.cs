using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    /// <summary>
    /// Summary description for ChuDe
    /// </summary>
    public class ChuDes_back
    {
        String name;
        String userIds;
        String ngayTaos;
        String anhDaiDiens;
        String loaitin;
        public ChuDes_back()
        {
            name = null;
            userIds = null;
            ngayTaos = null;
            anhDaiDiens = null;
            loaitin = null;
        }
        public String Loaitin
        {
            get { return loaitin; }
            set { loaitin = value; }
        }
        /// <summary>
        /// Đây là lớp chủ đề khác với ChuDe trong dataclassDataContex
        /// </summary>
     
        /// <summary>
        /// trar về thông tin của loại tin nhomtin => loaitin=>=>chude=> tintuc
        /// </summary>
        /// <param name="ChuDeId">id của loại tin</param>
        /// <returns>miêu tả của loại tin</returns>

        public String getNhomTinTheoChuDeId(int ChuDeId)
        {

            try
            {
                int ids = Convert.ToInt32(ChuDeId);
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = ids;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getLoaiTinTheoChuDe", paramList);
                return tables.Rows[0]["MieuTa"].ToString();

            }
            catch (Exception)
            {
                //  throw;

                return null;
            }
        }

        /// <summary>
        /// Lấy các chủ đề mới nhất
        /// </summary>
        public DataTable getTopNewesChuDe()
        {
            try
            {
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getTopChuDe");
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy các gian hàng nhất
        /// </summary>
        public DataTable getTopNewesShop()
        {
            try
            {
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getTopGianHang");
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy các chủ đề mới nhất
        /// </summary>
        public DataTable getRanDomForum()
        {
            try
            {
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getRanDomForum");

            }
            catch (Exception)
            {
                return null;
            }
        }
        public int countNewInchude(int ids)
        {

            try
            {
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = ids;

                DataTable table = ds.executeSelect("CountNewsInChuDe", paramList);
                int i = Convert.ToInt32(table.Rows[0]["counts"].ToString());
                return i;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        /// <summary>
        /// get thread by news id chu de con
        /// </summary>
        public void setChuDes(int id)
        {
            int ids = Convert.ToInt32(id);
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = ids;

            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable tables = ds.executeSelect("GetChuDeById", paramList);
            Name = tables.Rows[0]["tenChuDe"].ToString();
            UserIds = tables.Rows[0]["userId"].ToString();
            AnhDaiDiens = tables.Rows[0]["AnhDaiDien"].ToString();
            NgayTaos = tables.Rows[0]["NgayTao"].ToString();
            Loaitin = tables.Rows[0]["LoaiTinId"].ToString();

        }
        /// <summary>
        /// get thread by thread Id
        /// </summary>
        public ChuDes_back(int id)
        {
            int ids = Convert.ToInt32(id);
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = ids;

            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable tables = ds.executeSelect("GetChuDeById", paramList);
            Name = tables.Rows[0]["tenChuDe"].ToString();
            UserIds = tables.Rows[0]["userId"].ToString();
            AnhDaiDiens = tables.Rows[0]["AnhDaiDien"].ToString();
            NgayTaos = tables.Rows[0]["NgayTao"].ToString();
            Loaitin = tables.Rows[0]["LoaiTinId"].ToString();

        }
        public ChuDes_back(int id, Guid userId)
        {
            int ids = Convert.ToInt32(id);
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@chudeId", SqlDbType.Int, 32);
            paramList[0].Value = ids;
            paramList[1] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            paramList[1].Value = userId;


            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable tables = ds.executeSelect("getAllChuDeByUserIdAndId", paramList);

            Name = tables.Rows[0]["tenChuDe"].ToString();
            UserIds = tables.Rows[0]["userId"].ToString();
            AnhDaiDiens = tables.Rows[0]["AnhDaiDien"].ToString();
            NgayTaos = tables.Rows[0]["NgayTao"].ToString();
            Loaitin = tables.Rows[0]["LoaiTinId"].ToString();

        }
        /// <summary>
        /// lấy tất cả các chủ đề con của một loại chu de, chủ đề này có trạng thái là publish
        /// </summary>
        public DataTable AllChuDe(int id)
        {
            try
            {

                int ids = Convert.ToInt32(id);
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = ids;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("GetChuDe", paramList);

            }
            catch (Exception)
            {

                return null;
            }


        }
        public String NgayTaos
        {
            get { return ngayTaos; }
            set { ngayTaos = value; }
        }
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        public String UserIds
        {
            get { return userIds; }
            set { userIds = value; }
        }
        public String AnhDaiDiens
        {
            get { return anhDaiDiens; }
            set { anhDaiDiens = value; }
        }
        /// <summary>
        /// lấy tất cả các chủ đề của một userId
        /// </summary>
        public DataTable AllChuDeByIserId(Guid id)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
                paramList[0].Value = id;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getAllChuDeByUserId", paramList);

            }
            catch (Exception)
            {

                return null;
            }


        }
        /// <summary>
        /// lấy tất cả các chủ đề của một userId
        /// </summary>
        public DataTable AllgianHangByIserId(Guid id)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
                paramList[0].Value = id;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getAllshopByUserId", paramList);

            }
            catch (Exception)
            {

                return null;
            }


        }
        /// <summary>
        /// lấy tất cả các chủ đề của một userId, và loại tin dùng cho phần thêm tin
        /// </summary>
        public DataTable AllChuDeByIserIdAndLoaiTin(Guid id, int loaiTinId)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                paramList[0].Value = id;
                paramList[1] = new SqlParameter("@LoaiTin", SqlDbType.Int, 32);
                paramList[1].Value = loaiTinId;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getAllChuDeByUserIdAndLoaiTin", paramList);

            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// Lấy link hiện hành cho các ảnh với type img cho ảnh, và video cho youtube
        /// </summary>
        public DataTable getLinkOfNew(Guid usrId, String sessionId, String type)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[3];
                paramList[0] = new SqlParameter("@authen", SqlDbType.UniqueIdentifier);
                paramList[0].Value = usrId;
                paramList[1] = new SqlParameter("@session", SqlDbType.VarChar, 150);
                paramList[1].Value = sessionId;
                paramList[2] = new SqlParameter("@TypeOfLink", SqlDbType.VarChar, 150);
                paramList[2].Value = type;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getLinkBySessionAndUserId", paramList);

            }
            catch (Exception)
            {

                return null;
            }


        }
        //lấy tất cả các link của session hiện tại
        public DataTable getAllLinkOfNew(String sessionId, String type)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@session", SqlDbType.VarChar, 150);
                paramList[0].Value = sessionId;
                paramList[1] = new SqlParameter("@TypeOfLink", SqlDbType.VarChar, 150);
                paramList[1].Value = type;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getAllLinkBySessionId", paramList);

            }
            catch (Exception)
            {

                return null;
            }


        }
        /// <summary>
        /// lấy chủ đề cùng một nhóm tin, nhóm tin lấy theo id của tin hiện hành
        /// </summary>
        public DataTable AllChuDeByNhomTin(String id)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = Convert.ToInt32(id);
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getChuDeByNewId", paramList);

            }
            catch (Exception)
            {
            //    errorManager er = new errorManager();
            //    er.insertError(ex.ToString(), "lỗi từ hàm AllChuDeByNhomTin");
            return null;
            }


        }
    }
}
