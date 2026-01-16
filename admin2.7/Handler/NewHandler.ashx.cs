using AModul.Article;
using AModul;
using System;

using System.Linq;
using System.Web;
using Web.Mvc.Controllers;
using Dal;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for NewHandler
    /// </summary>
    public class NewHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var keyRequest = context.Request["op"];
            int uid = AppSession.CurentProfile.UserId;
            string s1 = "";            
            bool isAuthen = false;
            ArticleItemControl News = new ArticleItemControl();
            WebContentControl contentControl = new WebContentControl();
            if (UserProfileControl.IsUserInRole(uid, AEnum.UserRole.CanUpdateAndAddNew))
            {
                isAuthen = true;
            }
            if (isAuthen)
            {


                switch (keyRequest)
                {
                    case "getPolicy":
                        {

                            String type = context.Request["Type"];
                            int typeId = Convert.ToInt32(type);
                            try
                            {
                                if (typeId != 0)
                                {

                                    var model = contentControl.GetWebContent(typeId);
                                     s1 = model.TextContent;
                                }
                                else
                                {
                                    s1 = "";
                                }

                            }
                            catch
                            {

                                s1 = "";
                            }
                            break;
                        }
                    case "UpdatePolicy":
                        {
                            string Content = context.Request["Content"];
                            string type = context.Request["Type"];
                            try
                            {
                                if (type != "0")
                                {
                                    s1 = contentControl.UpdateWebContent(Convert.ToInt32(type), Content).ToString();
                                }
                                else
                                {
                                    s1 = "0";
                                }
                            }
                            catch (Exception)
                            {

                                s1 = "0";
                            }
                            break;
                        }
                    case "search":
                        {
                            string scat =context.Request["scat"];
                            string block = context.Request["block"];
                            if (string.IsNullOrEmpty(scat))
                            {
                                scat = "0";

                            }
                            if (string.IsNullOrEmpty(block))
                            {
                                block = "%";

                            }
                            String page = context.Request["page"];
                            if (string.IsNullOrEmpty(page))
                            {
                                page = "1";

                            }
                            s1 += "<table class='table table-bordered'>";
                            s1 += "<thead><tr><th>Thumb</th><th>Miêu Tả ngắn</th><th>Ngày Đăng</th><th>Lượt xem</th><th>Xóa</th><th>Chỉnh sửa</th></tr></thead>";

                            int newCount = 0;
                            var itemList = News.GetNewTable(Convert.ToInt32(scat), block, 0, "%", Convert.ToInt32(page), 12, out newCount);

                            if (itemList.Count > 0)
                            {
                                foreach (var item in itemList)
                                {
                                    s1 += "<tr><td class='thumbCl'><img src='" + item.Thumb + "' itemprop='image' alt='" + item.Tittle + "' /></td>";
                                    s1 += "<td>" + Ultil.StringHelper.SubString(50, item.Tittle);
                                    s1 += "</td><td>" + Ultil.Times.GetTimeFromYYYYmmddhhmmss(item.DatePost) + "</td>";
                                    s1 += "<td>" + item.ViewTime + "</td>";
                                    s1 += "<td><a href = 'javascript:void(0);' onclick='news.delNews(" + item.Id + ");'>Xóa</a></td>";
                                    s1 += "<td><a href = '/news/update?id=" + item.Id + "'>Chỉnh sửa</a></td></tr>";
                                }
                            }
                            s1 += " </table>";
                            if (itemList != null && itemList.Count() > 0)
                            {
                                s1 += Ultil.StringHelper.SetupAjaxPage(Convert.ToInt32(page), 12, newCount, 10, "news.search");
                            }

                            break;
                        }
                    case "delNews":
                        {
                            if (isAuthen)
                            {
                                String id = context.Request["id"];
                                try
                                {   
                                    News.DeleteNew(Convert.ToInt32(id));
                                    Dal.SysNotify sn = new Dal.SysNotify();
                                    sn.AddSysNotify("Thành viên " + AppSession.CurentProfile.UserName + " đã xóa tin có Id \" " + id + "\" vào lúc " + String.Format("{0:g}", DateTime.Now), "");
                                    s1 = "1";
                                }
                                catch (Exception)
                                {
                                    s1 = "0";
                                }
                            }
                            break;
                        }
                    case "Addnews":
                        {

                            string tittle = context.Request["tittle"].ToString().Trim();
                            tittle = Ultil.StringHelper.RemoveHtmlTangs(tittle);
                            tittle = Ultil.StringHelper.SubString(250, tittle);
                            string thumb = context.Request["thumb"];
                            int view = Convert.ToInt32(context.Request["view"]);
                            int publish = Convert.ToInt32(context.Request["publish"]);
                            int prioty = Convert.ToInt32(context.Request["prioty"]);


                            string shotDes = context.Request["shotDes"];

                            string SubCatId = context.Request["SubCatId"].ToString().Trim();

                            string tag = context.Request["tag"].ToString().Trim();
                            string linkTag = Ultil.StringHelper.toURLgachTag(tag);

                            string newContent = context.Request["newContent"];
                            int i = News.Addnews(SubCatId, tittle, shotDes, newContent, thumb, publish.ToString(), prioty.ToString(), "vi", tag, linkTag, uid, view);
                            Dal.SysNotify sn = new Dal.SysNotify();
                            sn.AddSysNotify("Thành viên " + AppSession.CurentProfile.UserName + " đã thêm tin \" <span class=\"lb-nf\"> " + tittle + "\" </span>vào lúc " + String.Format("{0:g}", DateTime.Now), "");

                        }
                        break;
                    case "Updatenews":
                        {
                            string tittle = context.Request["tittle"].ToString().Trim();
                            tittle = Ultil.StringHelper.RemoveHtmlTangs(tittle);
                            tittle = Ultil.StringHelper.SubString(250, tittle);
                            int newId = Convert.ToInt32(context.Request["id"].ToString().Trim());

                            String thumb = context.Request["thumb"];
                            int view = Convert.ToInt32(context.Request["view"]);
                            int publish = Convert.ToInt32(context.Request["publish"]);
                            int prioty = Convert.ToInt32(context.Request["prioty"]);
                            String shortDesc = context.Request["shortDes"].ToString().Trim();


                            String SubCatId = context.Request["SubCatId"].ToString().Trim();

                            String tag = context.Request["tag"].ToString().Trim();
                            String linkTag = Ultil.StringHelper.toURLgachTag(tag);

                            String newContent = context.Request["newContent"];

                            String UID = AppSession.CurentProfile.UserId.ToString();

                            double currentTime = Convert.ToDouble(Ultil.Times.GetyyyyMMddhhmmNow().Substring(0, 12));
                            var model = new Models.Modul.Article.ArticleItem();
                            model.Id = newId;
                            model.CategoryId = Convert.ToInt32(SubCatId);
                            model.Tittle = tittle;
                            model.ShortDesc = shortDesc;
                            model.NewsDesc = newContent;
                            model.Thumb = thumb;
                            model.Publish = publish == 1 ? true : false;
                            model.Prioty = prioty;
                            model.Lang = "vi";
                            model.Tag = tag;
                            model.CreateBy = uid;
                            model.ViewTime = view;
                            int i = News.UpdateNews(model);
                            Dal.SysNotify sn = new Dal.SysNotify();
                            sn.AddSysNotify("Thành viên " + AppSession.CurentProfile.UserName+ " đã cập nhật tin \" " + tittle + "\" vào lúc " + String.Format("{0:g}", DateTime.Now), "");

                        }
                        break;
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(s1);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}