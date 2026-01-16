using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;


namespace Dal
{
    public class News
    {
        #region khởi tạo các biến

        String subCatID;
        String id;
        String shotDesc;
        String raTe;

        String subCategoryName;
        String tittle;
        String newsDesc;
        String imgThumb;
        String ngayDang;
        String uuTien;
        String viewTime;
        String tag;
        String price;

        public String Price
        {
            get { return price; }
            set { price = value; }
        }

        public String Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        String tagKhoDau;

        public String TagKhoDau
        {
            get { return tagKhoDau; }
            set { tagKhoDau = value; }
        }

        public String SubCategoryName
        {
            get { return subCategoryName; }
            set { subCategoryName = value; }
        }


        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public String ShotDesc
        {
            get { return shotDesc; }
            set { shotDesc = value; }
        }

        public String Tittle
        {
            get { return tittle; }
            set { tittle = value; }
        }

        public String NewsDesc
        {
            get { return newsDesc; }
            set { newsDesc = value; }
        }

        String chuDeId;
        String tacGia;

        #endregion

        public String SubCatID
        {
            get { return subCatID; }
            set { subCatID = value; }
        }

        public String ChuDeId
        {
            get { return chuDeId; }
            set { chuDeId = value; }
        }

        public String ImgThumb
        {
            get { return imgThumb; }
            set { imgThumb = value; }
        }

        public String NgayDang
        {
            get { return ngayDang; }
            set { ngayDang = value; }
        }

        public String UuTien
        {
            get { return uuTien; }
            set { uuTien = value; }
        }

        public String ViewTime
        {
            get { return viewTime; }
            set { viewTime = value; }
        }

        public String RaTe
        {
            get { return raTe; }
            set { raTe = value; }
        }

        public String TacGia
        {
            get { return tacGia; }
            set { tacGia = value; }
        }
        public int GetNewsByID(string id, string publish)
        {
            try
            {
                int ids = Convert.ToInt32(id);
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = ids;
                paramList[1] = new SqlParameter("@publish", SqlDbType.VarChar, 2);
                paramList[1].Value = publish;

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("GetNewById", paramList);
                if (tables != null && tables.Rows.Count > 0)
                {

                    ShotDesc = tables.Rows[0]["shortDesc"].ToString();
                    Tittle = tables.Rows[0]["Tittle"].ToString();
                    // UuTien = tables.Rows[0]["UuTien"].ToString();
                    ViewTime = tables.Rows[0]["viewTime"].ToString();
                    // RaTe = tables.Rows[0]["rate"].ToString();
                    ImgThumb = tables.Rows[0]["thumb"].ToString();
                    NewsDesc = tables.Rows[0]["content"].ToString();
                    NgayDang = tables.Rows[0]["datepost"].ToString();
                    Id = tables.Rows[0]["Id"].ToString();
                    String s = tables.Rows[0]["author"].ToString();
                    SubCatID = tables.Rows[0]["Subcategory"].ToString();
                    Tag = tables.Rows[0]["tag"].ToString();
                    TagKhoDau = tables.Rows[0]["tagKhongDau"].ToString();
                    SubCategoryName = tables.Rows[0]["SubCatName"].ToString();
                    //SimpleMembershipProvider provider = (SimpleMembershipProvider)Membership.Provider;
                    //TacGia = provider.GetUserNameFromId(Convert.ToInt32(s));
                    return 1;
                }
                else { return 0; }


            }
            catch (Exception)
            {
                return 0;
                // throw;
            }
        }       
        /// <summary>
        /// get all media. images, video of news by id
        /// </summary>
        /// <param name="id">newid</param>
        /// <returns>DataTable of news id</returns>
        public DataTable getAllMediaOfNews(int id)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@newsid", SqlDbType.Int, 32);
                paramList[0].Value = id;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getAllMediaOfNews", paramList);
                return tables;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int dltNewByUser(int newid, String author)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            param[0].Value = newid;
            param[1] = new SqlParameter("@author", SqlDbType.Int, 32);
            param[1].Value = author;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("dltNewByUser", param);
        }

        public News()
        {
            id = "";
            ShotDesc = "";
            raTe = "";
            Tittle = "";
            NewsDesc = "";
            ImgThumb = "";
            ngayDang = "";
            uuTien = "";
            viewTime = "";

            TacGia = "";
            ChuDeId = "";
            Price = "0";
        }

        /// <summary>
        /// lấy loại tin theo id chu de trong phần tạo diễn đàn
        /// </summary>
        /// 
        public int updateReport(int code, int newId)
        {
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = newId;
            paramList[1] = new SqlParameter("@code", SqlDbType.Int, 32);
            paramList[1].Value = code;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("updateReport", paramList);
        }

        /// <summary>
        /// Lấy tất cả các tin của một người dùng
        /// </summary>
        /// 
        public DataTable getAllNewsByUserId(String uerId, int currentPage, int pageSite)
        {
            try
            {
                int beginRow = (currentPage - 1)*pageSite + currentPage;
                int endRow = (currentPage - 1)*pageSite + currentPage + pageSite;

                SqlParameter[] paramList = new SqlParameter[3];
                paramList[0] = new SqlParameter("@UID", SqlDbType.NVarChar, 300);
                paramList[0].Value = uerId;
                paramList[1] = new SqlParameter("@beginRow", SqlDbType.Int, 32);
                paramList[1].Value = beginRow;
                paramList[2] = new SqlParameter("@EndRow", SqlDbType.Int, 32);
                paramList[2].Value = endRow;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getAllNewsOfUser", paramList);
            }
            catch (Exception)
            {
                return null;
            }
        }
      
        public int reportNews(String id, int code)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = id;
                paramList[1] = new SqlParameter("@code", SqlDbType.Int, 32);
                paramList[1].Value = code;

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("reportNews", paramList);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Tim kiem mot tin
        /// </summary>
        /// 
        public DataTable TimKiem(String detail)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Detail", SqlDbType.NVarChar, 150);
                paramList[0].Value = detail;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("seachs", paramList);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// get lastes new
        /// </summary>
        /// 
        public int DellLinkByMod(String linkid, String session, String uid)
        {
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = linkid;
            paramList[1] = new SqlParameter("@userId", SqlDbType.NVarChar, 300);
            paramList[1].Value = uid;
            return ds.executeUpdate("deleteNewsImagesForMod", paramList);
        }

        public int UpdatePriotyLinkByMod(String linkid, String pryoty, String uid)
        {
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            SqlParameter[] paramList = new SqlParameter[3];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
            paramList[0].Value = linkid;
            paramList[1] = new SqlParameter("@userId", SqlDbType.NVarChar, 300);
            paramList[1].Value = uid;
            paramList[2] = new SqlParameter("@Prioty", SqlDbType.Int, 32);
            paramList[2].Value = pryoty;
            return ds.executeUpdate("updateLinkPriotyByMod", paramList);
        }

        public DataTable getnewByViewTime()
        {
            try
            {
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getTopView");
            }
            catch (Exception)
            {
                throw;
            }
        }

        String nguoidang;

        public String Nguoidang
        {
            get { return nguoidang; }
            set { nguoidang = value; }
        }

        /// <summary>
        /// trả về một bảng chứa 1 tin theo id nhập vào
        /// Hàm này đồng thời khởi tạo luôn đối tượng Tin có giá trị tương đương
        /// </summary>
        public int GetNewsByUID(int id,int st)
        {
            try
            {
                int ids = Convert.ToInt32(id);
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = ids;
                paramList[1] = new SqlParameter("@st", SqlDbType.Int);
                paramList[1].Value = st;

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("GetNewById", paramList);
                if (tables != null && tables.Rows.Count > 0)
                {
                    ShotDesc = tables.Rows[0]["ShortDesc"].ToString();
                    Tittle = tables.Rows[0]["Tittle"].ToString();
                    UuTien = tables.Rows[0]["prioty"].ToString();
                    ViewTime = tables.Rows[0]["viewTime"].ToString();
                    // RaTe = tables.Rows[0]["rate"].ToString();
                    ImgThumb = tables.Rows[0]["Thumb"].ToString();
                    NewsDesc = tables.Rows[0]["content"].ToString();
                    NgayDang = tables.Rows[0]["datepost"].ToString();
                    Id = tables.Rows[0]["Id"].ToString();
                    String s = tables.Rows[0]["author"].ToString();
                    SubCatID = tables.Rows[0]["Subcategory"].ToString();
                    Tag = tables.Rows[0]["tag"].ToString();
                    TagKhoDau = tables.Rows[0]["tagKhongDau"].ToString();
                    SubCategoryName = tables.Rows[0]["SubCatName"].ToString();

                    TacGia = s;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
                // throw;
            }
        }

        /// <summary>
        /// Đồng bộ hóa faceBook like tới app
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int sysFaceBookLikeToApp(string id, String comment, String like)
        {
            try
            {
                int ids = Convert.ToInt32(id);
                int comments = Convert.ToInt32(comment);
                int likes = Convert.ToInt32(like);
                SqlParameter[] paramList = new SqlParameter[3];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = ids;
                paramList[1] = new SqlParameter("@faceBookLike", SqlDbType.Int, 32);
                paramList[1].Value = likes;
                paramList[2] = new SqlParameter("@faceBookComment", SqlDbType.Int, 32);
                paramList[2].Value = comments;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                ds.executeUpdate("synFacebookLike", paramList);
                return 1;
            }
            catch (Exception)
            {
                return 0;
                // throw;
            }
        }

        public String getLinkBynews(int id,int st)
        {
            //hoiban.com/<%#csweb.Util.toURLgach(Eval("loaitinName").ToString()) %>/<%# csweb.Util.toURLgach(Eval("TieuDe").ToString()) %>-Tin<%# Eval("Id") %>.html
            String domainNames = ConfigurationManager.AppSettings.Get("DomainName").ToString();
            News tin = new News();
            try
            {
                tin.GetNewsByUID(id, st);
                if (String.IsNullOrEmpty(tin.SubCategoryName))
                {
                    return null;
                }
                else
                {
                    return domainNames + @"/" + StringManager.toURLgach(tin.SubCategoryName) + "/" +
                           StringManager.toURLgach(tin.Tittle) + @"-" + tin.Id;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// trả về các tin theo loại tin nhập vào
        /// </summary>
        public DataTable getNewTable(string subCat, string publish, int minPrioty, string author,int currentPage, int pageSite,int st,out int count)
        {
            try
            {
                int beginRow = (currentPage - 1)*pageSite + currentPage;
                int endRow = (currentPage - 1)*pageSite + currentPage + pageSite;

                SqlParameter[] paramList = new SqlParameter[7];
                paramList[0] = new SqlParameter("@st", SqlDbType.Int, 32);
                paramList[0].Value = st;
                paramList[1] = new SqlParameter("@subcat", SqlDbType.VarChar, 10);
                paramList[1].Value = subCat;
                paramList[2] = new SqlParameter("@publish", SqlDbType.VarChar, 2);
                paramList[2].Value = publish;
                paramList[3] = new SqlParameter("@author", SqlDbType.VarChar, 10);
                paramList[3].Value = author;
                paramList[4] = new SqlParameter("@beginRow", SqlDbType.Int, 32);
                paramList[4].Value = beginRow;
                paramList[5] = new SqlParameter("@EndRow", SqlDbType.Int, 32);
                paramList[5].Value = endRow;
                paramList[6] = new SqlParameter("@Prioty", SqlDbType.Int, 32);
                paramList[6].Value = minPrioty;
                
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                Dal.DatabaseAccess2 d2s = new Dal.DatabaseAccess2();

                DataTable tables = ds.executeSelect("sp_getNewTable", paramList, out count);
                return tables;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable getNewTableFrontEnd(int subCat,int minPrioty, int currentPage, int pageSite, int st, out int count)
        {
            try
            {
                int beginRow = (currentPage - 1) * pageSite + currentPage;
                int endRow = (currentPage - 1) * pageSite + currentPage + pageSite;

                SqlParameter[] paramList = new SqlParameter[5];
                paramList[0] = new SqlParameter("@st", SqlDbType.Int, 32);
                paramList[0].Value = st;
                paramList[1] = new SqlParameter("@subcat", SqlDbType.VarChar, 10);
                paramList[1].Value = subCat;
                paramList[2] = new SqlParameter("@beginRow", SqlDbType.Int, 32);
                paramList[2].Value = beginRow;
                paramList[3] = new SqlParameter("@EndRow", SqlDbType.Int, 32);
                paramList[3].Value = endRow;
                paramList[4] = new SqlParameter("@Prioty", SqlDbType.Int, 32);
                paramList[4].Value = minPrioty;

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                Dal.DatabaseAccess2 d2s = new Dal.DatabaseAccess2();

                DataTable tables = ds.executeSelect("sp_getNewTable_2", paramList, out count);
                return tables;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataRow GetNewsByIdFrontEnd(int id,int st)
        {
            try
            {
                int ids = Convert.ToInt32(id);
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = ids;
                paramList[1] = new SqlParameter("@st", SqlDbType.Int, 32);
                paramList[1].Value = st;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("GetNewById_2", paramList);
                if (tables != null && tables.Rows.Count > 0)
                {
                  return tables.Rows[0];                
                }
                else { return null; }


            }
            catch (Exception)
            {
                return null;
                // throw;
            }
        }
        /// <summary>
        /// lấy 5 tin trong phần ảnh hot 
        /// </summary>
        /// <returns></returns>
        public DataTable getTopGalleryNews()
        {
            try
            {
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getTopGalleryNews");
                return tables;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Tra ve cac tin nhieu nguoi xem nhat
        /// </summary>
        public DataTable getNewsByViewTime()
        {
            try
            {
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("GetNewsByViewTime");
                return tables;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lấy về các tin vừa comment
        /// </summary>
        public DataTable getnewsestComment()
        {
            try
            {
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getTopNewComment");
                return tables;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataTable getNewByUId(String UID)
        {
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@UID", SqlDbType.NVarChar, 300);
            paramList[0].Value = UID;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable tables = ds.executeSelect("getAllNewsOfUser", paramList);
            return tables;
        }

        /// <summary>
        /// Lấy về các tin có nhiều người bình chọn nhất
        public DataTable getnewByrateTime()
        {
            try
            {
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getTopNewByrate");
                return tables;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public String getLastetTimePoster(String uid)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@UID", SqlDbType.NVarChar, 300);
                paramList[0].Value = uid;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getLasetTimePostOfUser", paramList);
                if (tables != null)
                {
                    if (tables.Rows.Count > 0)
                    {
                        return tables.Rows[0]["lastPost"].ToString();
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        /// <summary>
        /// Tra ve cac tin mới nhất được phân trang
        /// </summary>
        public DataTable getnewsestNewsPager(int currentPage, int pageSite, String category, String publish)
        {
            int beginRow = (currentPage - 1)*pageSite + currentPage;
            int endRow = (currentPage - 1)*pageSite + currentPage + pageSite;
            try
            {
                SqlParameter[] paramList = new SqlParameter[4];
                paramList[0] = new SqlParameter("@beginRow", SqlDbType.Int, 32);
                paramList[0].Value = beginRow;

                paramList[1] = new SqlParameter("@EndRow", SqlDbType.Int, 32);
                paramList[1].Value = endRow;

                paramList[2] = new SqlParameter("@category", SqlDbType.VarChar, 10);
                paramList[2].Value = category;

                paramList[3] = new SqlParameter("@publish", SqlDbType.VarChar, 10);
                paramList[3].Value = publish;


                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("spTB_getNews", paramList);
                return tables;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lay cac tin theo tag cua tin hien thoi
        /// </summary>
        /// <returns></returns>
        public DataTable getTopnewsByTag(String tag1, String tag2)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@tag1", SqlDbType.VarChar, 150);
                paramList[0].Value = tag1;
                paramList[1] = new SqlParameter("@tag2", SqlDbType.VarChar, 150);
                paramList[1].Value = tag2;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getTopNewByTag", paramList);
                return tables;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Khi page load trang chi tiết tin sẽ cộng 1 vào lượt xem của tin này
        /// </summary>
        public String updateViewTime(String id)
        {
            try
            {
                int j = Convert.ToInt32(id);
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@ID", SqlDbType.Int);
                paramList[0].Value = j;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                //  DatabaseAccess ds = new DatabaseAccess();
                ds.executeUpdate("UPDATEVIEW", paramList);
                return "cập nhật tin thành công";
            }
            catch (Exception e)
            {
                return "cập nhật tin Lỗi vui long liên hệ quản trị \n" + e.ToString();
            }
        }

        public int Addnews(String Subcategorys, String tittle, string _shortDesc, String newcontent, String imggesThumb,
            String publish, String prioty, String lang, String tag, String linkTag, int userID, int viewTime,int st)
        {
            checkValid checks = new checkValid();
            Tittle = checks.removeHtmlTangs(tittle);
            NewsDesc = StringManager.RemoveScript(newcontent);
            String newdesc1 = NewsDesc;

            if (newsDesc.Length > 50000)
            {
                newdesc1 = newsDesc.Substring(0, 49999);
            }
            try
            {
                int loaitins = Convert.ToInt32(Subcategorys);
                SqlParameter[] paramList = new SqlParameter[14];
                paramList[0] = new SqlParameter("@SubCatID", SqlDbType.Int);
                paramList[0].Value = Subcategorys;

                paramList[1] = new SqlParameter("@Tittle", SqlDbType.NVarChar, 300);
                paramList[1].Value = tittle;

                paramList[2] = new SqlParameter("@content", SqlDbType.NText);
                paramList[2].Value = newdesc1;


                paramList[3] = new SqlParameter("@anhDaiDien", SqlDbType.VarChar, 300);
                paramList[3].Value = imggesThumb;

                paramList[4] = new SqlParameter("@NgayDang", SqlDbType.VarChar, 300);
                paramList[4].Value = Dal.Times.GetyyyyMMddhhmmNow();

                paramList[5] = new SqlParameter("@tacGia", SqlDbType.NVarChar, 300);
                paramList[5].Value = userID;

                int congKhais = Convert.ToInt32(publish);
                paramList[6] = new SqlParameter("@congKhai", SqlDbType.Bit);
                paramList[6].Value = congKhais;

                int Uutiens = Convert.ToInt32(prioty);
                paramList[7] = new SqlParameter("@uuTien", SqlDbType.Int);
                paramList[7].Value = Uutiens;

                paramList[8] = new SqlParameter("@viewTime", SqlDbType.Int, 32);
                paramList[8].Value = viewTime;

                paramList[9] = new SqlParameter("@linktag", SqlDbType.VarChar, 150);
                paramList[9].Value = linkTag;

                paramList[10] = new SqlParameter("@lang", SqlDbType.NVarChar, 50);
                paramList[10].Value = lang;

                paramList[11] = new SqlParameter("@mieuTaNgan", SqlDbType.NVarChar, 300);
                paramList[11].Value = _shortDesc;

                paramList[12] = new SqlParameter("@tag", SqlDbType.NVarChar, 50);
                paramList[12].Value = tag;

                paramList[13] = new SqlParameter("@st", SqlDbType.Int);
                paramList[13].Value = st;


                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                int i = ds.executeUpdate("addNews", paramList);
                return i;
            }
            catch (Exception)
            {
                throw;
                //return 0;
            }
        }

        public int UpdateNews(int newId, String Subcategorys, String tittle, String shortDesc, String newcontent,
            String imggesThumb, String publish, String prioty, String lang, String tag, String linkTag, int userID,
            int viewTime,int st)
        {
            checkValid checks = new checkValid();
            Tittle = checks.removeHtmlTangs(tittle);
            NewsDesc = StringManager.RemoveScript(newcontent);
            String newdesc1 = NewsDesc;

            if (newsDesc.Length > 50000)
            {
                newdesc1 = newsDesc.Substring(0, 49999);
            }
            try
            {
                int loaitins = Convert.ToInt32(Subcategorys);
                SqlParameter[] paramList = new SqlParameter[15];
                paramList[0] = new SqlParameter("@SubCatID", SqlDbType.Int);
                paramList[0].Value = Subcategorys;

                paramList[1] = new SqlParameter("@Tittle", SqlDbType.NVarChar, 300);
                paramList[1].Value = tittle;

                paramList[2] = new SqlParameter("@content", SqlDbType.NText);
                paramList[2].Value = newdesc1;

                paramList[3] = new SqlParameter("@anhDaiDien", SqlDbType.VarChar, 300);
                paramList[3].Value = imggesThumb;


                paramList[4] = new SqlParameter("@NgayDang", SqlDbType.VarChar, 300);
                paramList[4].Value = Dal.Times.GetyyyyMMddhhmmNow();

                paramList[5] = new SqlParameter("@tacGia", SqlDbType.NVarChar, 300);
                paramList[5].Value = userID;

                int congKhais = Convert.ToInt32(publish);
                paramList[6] = new SqlParameter("@congKhai", SqlDbType.Bit);
                paramList[6].Value = congKhais;

                int Uutiens = Convert.ToInt32(prioty);
                paramList[7] = new SqlParameter("@uuTien", SqlDbType.Int);
                paramList[7].Value = Uutiens;

                paramList[8] = new SqlParameter("@viewTime", SqlDbType.Int, 32);
                paramList[8].Value = viewTime;

                paramList[9] = new SqlParameter("@lang", SqlDbType.NVarChar, 50);
                paramList[9].Value = lang;

                paramList[10] = new SqlParameter("@ShortDesc", SqlDbType.NVarChar, 300);
                paramList[10].Value = shortDesc;

                paramList[11] = new SqlParameter("@tag", SqlDbType.NVarChar, 50);
                paramList[11].Value = tag;

                paramList[12] = new SqlParameter("@linktag", SqlDbType.VarChar, 150);
                paramList[12].Value = linkTag;

                paramList[13] = new SqlParameter("@Id", SqlDbType.Int);
                paramList[13].Value = newId;
                paramList[14] = new SqlParameter("@st", SqlDbType.Int);
                paramList[14].Value = st;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                int i = ds.executeUpdate("UpdateNews", paramList);
                return i;
            }
            catch (Exception)
            {
                throw;
                //return 0;
            }
        }

        /// <summary>
        /// Laays tat ca cac anh trong tin lam slide
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable getAllImagesSlideByNewId(int id, String type, int currentPage, int pageSite)
        {
            int beginRow = currentPage;
            int endRow = currentPage + pageSite;
            try
            {
                SqlParameter[] paramList = new SqlParameter[4];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = id;
                paramList[1] = new SqlParameter("@type", SqlDbType.VarChar, 150);
                paramList[1].Value = type;

                paramList[2] = new SqlParameter("@beginRow", SqlDbType.Int, 32);
                paramList[2].Value = beginRow;
                paramList[3] = new SqlParameter("@EndRow", SqlDbType.Int, 32);
                paramList[3].Value = endRow;

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getAllImagesSlideByNews", paramList);
                return tables;
            }
            catch (Exception)
            {
                return null;
                // throw;
            }
        }

        // xóa mọi video và ảnh tạm khi người dùng thoát trình duyệt
        public int dellAllMediaSessionTimeOut(String sessionId, String user)
        {
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@sesionId", SqlDbType.VarChar, 150);
            paramList[0].Value = sessionId;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("dellLinkImg", paramList);
        }

        public int AddMesdiaToNew(String SessionId_, String Url, String Prioty, String title, String TypeOfLink,
            String uid)
        {
            try
            {
                int prioty = 0;
                SqlParameter[] paramList = new SqlParameter[6];
                paramList[0] = new SqlParameter("@session", SqlDbType.VarChar, 150);
                paramList[0].Value = SessionId_;

                Url = StringManager.removeHtmlTangs(Url);
                paramList[1] = new SqlParameter("@Urls", SqlDbType.VarChar, 300);
                paramList[1].Value = Url;

                try
                {
                    prioty = Convert.ToInt32(Prioty);
                }
                catch (Exception)
                {
                    prioty = 1;
                }
                paramList[2] = new SqlParameter("@Prioty", SqlDbType.Int, 32);
                paramList[2].Value = Prioty;

                title = StringManager.removeHtmlTangs(title);
                paramList[3] = new SqlParameter("@title", SqlDbType.NVarChar, 1500);
                paramList[3].Value = title;
                checkValid c = new checkValid();
                TypeOfLink = c.ToEndcodedingLeveForum(TypeOfLink);
                paramList[4] = new SqlParameter("@TypeOfLink", SqlDbType.VarChar, 50);
                paramList[4].Value = TypeOfLink;

                paramList[5] = new SqlParameter("@authen", SqlDbType.NVarChar, 300);
                paramList[5].Value = uid;


                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("addNewsCurentMesdia", paramList);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable getLinkOfNewByUserAndSession(String usrId, String sessionId, String type)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[3];
                paramList[0] = new SqlParameter("@authen", SqlDbType.NVarChar, 300);
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

        public int delNews(String id,int st)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
                paramList[0].Value = id;
                paramList[1] = new SqlParameter("@st", SqlDbType.Int);
                paramList[1].Value = st;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("delNewsById", paramList);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

    public class Category
    {
        int catagorByCat;

        public int CatagorByCat
        {
            get { return catagorByCat; }
            set { catagorByCat = value; }
        }

        public Category()
        {
            CatagorByCat = 0;
        }

        public int addCategory(String tittle,int st)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@title", SqlDbType.NVarChar, 150);
                paramList[0].Value = tittle;
                paramList[1] = new SqlParameter("@st", SqlDbType.Int);
                paramList[1].Value = st;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("addCategory", paramList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateCategory(String tittle, String id)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@title", SqlDbType.NVarChar, 150);
                paramList[0].Value = tittle;
                paramList[1] = new SqlParameter("@Id", SqlDbType.Int);
                paramList[1].Value = id;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("updateCategory", paramList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DelCategory(String id)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
                paramList[0].Value = id;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("DelCategory", paramList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable getCategoryList(int st)
        {
            Dal.DatabaseAccess ds = new DatabaseAccess();
            return ds.executeCommandSelect("select * from category where st=" + st.ToString());
        }
    }

    public class SubCategory
    {
        String subName;
        int subId;
        int parentCatId;

        public String SubName
        {
            get { return subName; }
            set { subName = value; }
        }

        public int ParentCatId
        {
            get { return parentCatId; }
            set { parentCatId = value; }
        }

        public int SubId
        {
            get { return subId; }
            set { subId = value; }
        }

        public int countNewsInCat(int subCatId)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = subCatId;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeScalary("countNewInSubCat", paramList);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable GetSubcategoruByCat(int id,int st)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@catId", SqlDbType.Int, 32);
                paramList[0].Value = id;
                paramList[1] = new SqlParameter("@st", SqlDbType.Int);
                paramList[1].Value = st;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("getSubByCateGory", paramList);
            }
            catch (Exception)
            {
                return null;
                // throw;
            }
        }
        public DataTable GetArticleCateGory(int st)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
          
                paramList[0] = new SqlParameter("@st", SqlDbType.Int);
                paramList[0].Value = st;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeSelect("GetArticeCategory", paramList);
            }
            catch (Exception)
            {
                return null;           
            }
        }

        public SubCategory()
        {
        }

        public SubCategory(int id)
        {
            DatabaseAccess ds = new DatabaseAccess();
            DataTable table = ds.executeCommandSelect(@"select * from subcategory where Id=" + id);
            if (table != null && table.Rows.Count > 0)
            {
                SubId = Convert.ToInt32(table.Rows[0]["Id"].ToString());
                SubName = table.Rows[0]["mieuta"].ToString();
                ParentCatId = Convert.ToInt32(table.Rows[0]["categoryId"].ToString());
            }
        }

        public int AddSubCategory(int catId, String subName,int st)
        {
            {
                try
                {
                    SqlParameter[] paramList = new SqlParameter[3];
                    paramList[0] = new SqlParameter("@catId", SqlDbType.Int, 32);
                    paramList[0].Value = catId;
                    paramList[1] = new SqlParameter("@subName", SqlDbType.NVarChar, 150);
                    paramList[1].Value = subName;
                    paramList[2] = new SqlParameter("@st", SqlDbType.Int);
                    paramList[2].Value = st;
                    Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                    return ds.executeUpdate("addSubCategory", paramList);
                    ;
                }
                catch (Exception ex)
                {
                    Dal.errorManager er = new Dal.errorManager();
                    er.insertError(ex.Message, "From AddSubCategory method");
                    return 0;
                }
            }
        }

        public int delSubCat(int catId)
        {
            {
                try
                {
                    SqlParameter[] paramList = new SqlParameter[1];
                    paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                    paramList[0].Value = catId;

                    Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                    return ds.executeUpdate("delSubCat", paramList);
                    ;
                }
                catch (Exception ex)
                {
                    Dal.errorManager er = new Dal.errorManager();
                    er.insertError(ex.Message, "From AddSubCategory method");
                    return 0;
                }
            }
        }

        public int UpdateSubCat(int catId, String desc)
        {
            {
                try
                {
                    SqlParameter[] paramList = new SqlParameter[2];
                    paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                    paramList[0].Value = catId;
                    paramList[1] = new SqlParameter("@Desc", SqlDbType.NVarChar, 150);
                    paramList[1].Value = desc;

                    Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                    return ds.executeUpdate("UpdateSubCat", paramList);
                    ;
                }
                catch (Exception ex)
                {
                    Dal.errorManager er = new Dal.errorManager();
                    er.insertError(ex.Message, "From AddSubCategory method");
                    return 0;
                }
            }
        }
    }
}