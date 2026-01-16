using Dal;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace StringMng
{
    public static class StringManager
    {
        public static void Recapcha()
        {
            makeRandom rd = new makeRandom();
            System.Web.HttpContext.Current.Session["capcha"] = rd.RanDomTextVi();
        }
      
       
        #region cắt một chuỗi với độ dài tối đa cho phép
        /// <summary>
        /// cắt chuỗi
        /// </summary>
        /// <param name="input">String</param>
        /// <param name="maxCharacter">Độ dài tối đa của chuỗi</param>
        /// <returns></returns>
        public static string cutString(string input, int maxCharacter)
        {
            string tieude = input.ToString().Trim();
            int maxstr = maxCharacter;
            string chuoi = "";
            if (tieude.Length > maxstr)
            {
                string x = tieude.Substring(0, maxstr);
                string[] s = x.Split(' ');
                for (int i = 0; i < s.Length - 1; i++)
                {
                    chuoi = chuoi + " " + s[i];
                }
                chuoi = chuoi + "...";
            }
            else
            {
                chuoi = tieude;
            }
            return chuoi;
        }
        #endregion
        public static string toHtmlDecode(String title)
        {
            return System.Web.HttpUtility.HtmlDecode(title);

        }

        public static String HtmlDecodeIngLeverForum(String s)
        {


            s = s.Replace("''", "'");

            s = s.Replace(@"&#60", "[");

            s = s.Replace(@"&#x3C", "[");


            s = s.Replace(@"\u003c", "[");
            s = s.Replace(@"&quot;", @" &quot;");
            s = s.Replace("[br /]", "<br />");
            s = s.Replace("[br]", "<br>");
            s = s.Replace("[table]", "<table>");

            s = s.Replace("[strong]", "<strong>");
            s = s.Replace("[/strong]", "</strong>");

            s = s.Replace("[tr]", "<tr>");
            s = s.Replace("[/span]", "</span>");
            s = s.Replace("[b]", "<b>");
            s = s.Replace("[/b]", "</b>");
            s = s.Replace("[code]", "<code>");
            s = s.Replace("[/code]", "</code>");
            s = s.Replace("[/tr]", "</tr>");
            s = s.Replace("[td]", "<td>");
            s = s.Replace("[/td]", "</td>");


            s = s.Replace("[em]", "<em>");
            s = s.Replace("[/em]", "</em>");

            s = s.Replace("[/h3]", "</h3>");
            s = s.Replace("[h3]", "<h3>");

            s = s.Replace("[h4]", "<h4>");

            s = s.Replace("[/h4]", "</h4>");

            s = s.Replace("[/h5]", "</h5>");

            s = s.Replace("[h5]", "<h5>");

            s = s.Replace("[i]", "<i>");
            s = s.Replace("[/i]", "</i>");
           // s = s = Regex.Replace(s, @"\[IMG\](\bhttps?:[^)'']+\.(?:jpg|jpeg|gif|png))\[\/IMG\]", @"<img src=""$1"" border=""0"" />", RegexOptions.IgnoreCase);         
            s = s = Regex.Replace(s, @"\[IMG\](https?://(?:[a-z\-\.]+\.)+[a-z]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png|jpeg))\[\/IMG\]", @"<img src=""$1"" border=""0"" />", RegexOptions.IgnoreCase);         
            
            s = System.Web.HttpUtility.HtmlDecode(s);
            return s;
        }
        public static String EncodeBCImg(String s){
            return s = Regex.Replace(s, @"\[IMG\]((http(s?):)([\/|.|\w|\s-])*\.(?:jpg|gif|png|jpeg)){0,300}[\/IMG\]", @"<img src=""$1"" border=""0"" />", RegexOptions.IgnoreCase);         
        }
        public static String SubString(int maxLeng, String inPut)
        {
            if (inPut.Length >= maxLeng)
            {
                inPut = inPut.Substring(0, maxLeng - 2)+" ..";
            }
            return inPut;


        }
       
        public static String setUpPagedV2(int curentPage, int pageSite, int newsCount, int MaxPage, String query)
        {
            String pagedString = "";
            if (newsCount >= 1000)
            {
                newsCount = 1000;
            }
            int totallPaed = newsCount / pageSite;
            if ((newsCount % pageSite) > 0)
            {
                totallPaed += 1;
            }
            int totalPageSpit = totallPaed / pageSite;
            totallPaed = totallPaed - totalPageSpit;
            string CurentUrl = HttpContext.Current.Request.RawUrl.ToString();
            //String rrurl = Req HttpContext.Current.Request.RawUrl;
            //string[] s = rrurl.Split('.');
            string[] ss = CurentUrl.Split('/');

            string pageaspx = "";
            if (CurentUrl.LastIndexOf(query) > 0)
            {
                if (ss.Length == 5)
                {
                    pageaspx = ss[0] + "/" + ss[1] + "/" + ss[2] + "/" + ss[3];
                }
                if (ss.Length == 4)
                {
                    pageaspx = ss[0] + "/" + ss[1] + "/" + ss[2];
                }

                if (ss.Length == 3)
                {
                    pageaspx = ss[0] + "/" + ss[1];
                }
                if (ss.Length == 2)
                {
                    pageaspx = ss[0] + "/" + ss[1];
                }
                if (ss.Length == 1)
                {
                    pageaspx = ss[0];
                }
            }
            else
            {
                if (ss.Length == 5)
                {
                    pageaspx = ss[0] + "/" + ss[1] + "/" + ss[2] + "/" + ss[3] + "/" + ss[4];
                }
                if (ss.Length == 4)
                {
                    pageaspx = ss[0] + "/" + ss[1] + "/" + ss[2] + "/" + ss[3];
                }

                if (ss.Length == 3)
                {
                    pageaspx = ss[0] + "/" + ss[1] + "/" + ss[2];
                }
                if (ss.Length == 2)
                {
                    pageaspx = ss[0] + "/" + ss[1];
                }
                if (ss.Length == 1)
                {
                    pageaspx = ss[0];
                }
            }
            int queryIndex = pageaspx.LastIndexOf(query);
            if (queryIndex > 0)
            {
                pageaspx = pageaspx.Substring(0, queryIndex);
            }


            // string path = HttpContext.Current.Request.Url.AbsolutePath;
            // // /TESTERS/Default6.aspx
            if (newsCount > pageSite)
            {
                if (curentPage == 1)
                {
                    pagedString += @"<div class='new_pages_c'><span><img src='/images/btn_prev.png' title='Bạn đang ở trang đầu' alter='trang sau'></span><span>Trang :</span>";

                }
                else
                {
                    pagedString += @"<div class='new_pages_c'><span><a href='" + pageaspx + query + (curentPage - 1) + "'><img src='/images/btn_prev.png' title='trang sau' alter='trang sau'></a></span><span>Trang :</span>";

                }


                if (curentPage <= totallPaed)
                {
                    //gioi han hien tai cua phan trang
                    int j = curentPage + MaxPage;
                    int k = MaxPage;
                    if (k <= totallPaed)
                    {
                        k = totallPaed;
                    }
                    if (j >= k)
                    {
                        j = totallPaed;
                    }
                    int currenttag;
                    if (curentPage == 1 || curentPage == 2 || curentPage == 3)
                    {
                        currenttag = 1;
                    }
                    else
                    {
                        currenttag = curentPage - 2;
                    }

                    for (int i = currenttag; i < j; i++)
                    {



                        if (i == curentPage)
                        {
                            pagedString += @"<b>" + (i) + "</b>";

                        }
                        else
                        {
                            pagedString += @"<a href='" + pageaspx + query + i + "'>" + i + "</a>";
                        }

                    }



                }

                if (curentPage == totallPaed)
                {
                    pagedString += @"<span><img src='/images/btn_next.png' title='Bạn đang ở trang cuối' alter='trang tiếp'></span></div>";

                }
                else
                {
                    pagedString += @"<a href='" + pageaspx + query + totallPaed + @"'> . " + totallPaed + @"</a>";
                    pagedString += @"<span><a href='" + pageaspx + query + (curentPage + 1) + "'><img src='/images/btn_next.png' title='trang Tiếp' alter='trang tiếp'></a></span></div>";

                }

            }



            return pagedString;
        }
        public static String setUpPagedV3(int curentPage, int pageSite, int newsCount, int MaxPage, String query)
        {
            String pagedString = "";
            if (newsCount >= 1000)
            {
                newsCount = 1000;
            }
            int totallPaed = newsCount / pageSite;
            if ((newsCount % pageSite) > 0)
            {
                totallPaed += 1;
            }
            int totalPageSpit = totallPaed / pageSite;
            totallPaed = totallPaed - totalPageSpit;
            string CurentUrl = HttpContext.Current.Request.RawUrl.ToString();         
            string pageaspx = "";
        
            int queryIndex = pageaspx.LastIndexOf(query);
            if (queryIndex > 0)
            {
                String pageAspx2 = pageaspx.Substring(queryIndex, CurentUrl.Length);
            }


            // string path = HttpContext.Current.Request.Url.AbsolutePath;
            // // /TESTERS/Default6.aspx
            if (newsCount > pageSite)
            {
                if (curentPage == 1)
                {
                    pagedString += @"<div class='new_pages_c'><span><img src='/images/btn_prev.png' title='Bạn đang ở trang đầu' alter='trang sau'></span><span>Trang :</span>";

                }
                else
                {
                    pagedString += @"<div class='new_pages_c'><span><a href='" + pageaspx + query + (curentPage - 1) + "'><img src='/images/btn_prev.png' title='trang sau' alter='trang sau'></a></span><span>Trang :</span>";

                }


                if (curentPage <= totallPaed)
                {
                    //gioi han hien tai cua phan trang
                    int j = curentPage + MaxPage;
                    int k = MaxPage;
                    if (k <= totallPaed)
                    {
                        k = totallPaed;
                    }
                    if (j >= k)
                    {
                        j = totallPaed;
                    }
                    int currenttag;
                    if (curentPage == 1 || curentPage == 2 || curentPage == 3)
                    {
                        currenttag = 1;
                    }
                    else
                    {
                        currenttag = curentPage - 2;
                    }

                    for (int i = currenttag; i < j; i++)
                    {



                        if (i == curentPage)
                        {
                            pagedString += @"<b>" + (i) + "</b>";

                        }
                        else
                        {
                            pagedString += @"<a href='" + pageaspx + query + i + "'>" + i + "</a>";
                        }

                    }



                }

                if (curentPage == totallPaed)
                {
                    pagedString += @"<span><img src='/images/btn_next.png' title='Bạn đang ở trang cuối' alter='trang tiếp'></span></div>";

                }
                else
                {
                    pagedString += @"<a href='" + pageaspx + query + totallPaed + @"'> . " + totallPaed + @"</a>";
                    pagedString += @"<span><a href='" + pageaspx + query + (curentPage + 1) + "'><img src='/images/btn_next.png' title='trang Tiếp' alter='trang tiếp'></a></span></div>";

                }

            }



            return pagedString;
        }
     
    }
}