using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Dal
{
    class SysFaceBook
    {

    }

    //public class FacebookDetail
    //{
    //    public String getLinkBynews(String id)
    //    {
    //        //hoiban.com/<%#csweb.Util.toURLgach(Eval("loaitinName").ToString()) %>/<%# csweb.Util.toURLgach(Eval("TieuDe").ToString()) %>-Tin<%# Eval("Id") %>.html
    //        String domainNames = ConfigurationManager.AppSettings.Get("DomainName").ToString();
    //        News tin = new News();
    //        try
    //        {

    //            tin.GetNewsByUID(id,"%");
    //            if (String.IsNullOrEmpty(tin.SubCategoryName))
    //            {
    //                return null;
    //            }
    //            else
    //            {
    //                // 
    //                return domainNames + @"/" + StringManager.toURLgach(tin.SubCategoryName) + "/" + StringManager.toURLgach(tin.Tittle) + @"-" + tin.Id;
    //            }
    //        }
    //        catch (Exception)
    //        {

    //            return null;
    //        }

    //    }

    //    public int faceBookSyn(String id)
    //    {


    //        try
    //        {
    //            News tin = new News();
    //            String link = tin.getLinkBynews(id);
    //            //String link = "http://xuanbac.azgame.vn/gag/170";

    //            if (!String.IsNullOrEmpty(link))
    //            {
    //                FacebookDetail fDetail = new FacebookDetail();
    //                fDetail = SybFaceBook(link);
    //                tin.sysFaceBookLikeToApp(id, fDetail.CommentCount, fDetail.TotalCount);
    //                return 1;

    //            }
    //            else {
    //                return 0;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            return 0;
    //        }

    //    }
    //    public FacebookDetail SybFaceBook(String urls)
    //    {


    //        FacebookDetail _objwebsitedetail = new FacebookDetail();
    //        WebClient web = new WebClient();

    //        string url = string.Format("https://api.facebook.com/method/fql.query?query=SELECT url, share_count, like_count, comment_count, total_count, click_count FROM link_stat where url='" + urls + "'");
    //        string response = web.DownloadString(url);
    //        DataSet _objxmlds = new DataSet();
    //        using (StringReader stringReader = new StringReader(response))
    //        {
    //            _objxmlds = new DataSet();
    //            _objxmlds.ReadXml(stringReader);
    //        }
    //        DataTable dt = _objxmlds.Tables["link_stat"];
    //        foreach (DataRow dtrow in dt.Rows)
    //        {
    //            _objwebsitedetail.Url = dtrow["url"].ToString();
    //            _objwebsitedetail.LikeCount = dtrow["like_count"].ToString();
    //            _objwebsitedetail.SharedCount = dtrow["share_count"].ToString();
    //            _objwebsitedetail.CommentCount = dtrow["comment_count"].ToString();
    //            _objwebsitedetail.ClickCount = dtrow["click_count"].ToString();
    //            _objwebsitedetail.TotalCount = dtrow["total_count"].ToString();
    //        }
    //        return _objwebsitedetail;
    //    }

 

    //    public string Url { get; set; }
    //    public string SharedCount { get; set; }
    //    public string LikeCount { get; set; }
    //    public string CommentCount { get; set; }
    //    public string ClickCount { get; set; }
    //    public string TotalCount { get; set; }
    //}
    }



