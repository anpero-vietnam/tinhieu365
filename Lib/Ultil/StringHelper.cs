using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
namespace Ultil
{

    public static class StringHelper
    {
        public static string GetOrientationName(int? orientationId)
        {
            switch (orientationId)
            {
                case 1:
                    return "East";
                case 2:
                    return "West";
                case 3:
                    return "South";
                case 4:
                    return "North";
                case 5:
                    return "Northeast";
                case 6:
                    return "Northwest";
                case 7:
                    return "Southwest";
                case 8:
                    return "Southeast";
                default:
                    return string.Empty;
                
            }
        }
        public static string GetYoutubeIdFromImagesLink(string youtubeImages)
        {
            string ytbIds = string.Empty;
            if (youtubeImages.Contains("img.youtube.com"))
            {            
                ytbIds = youtubeImages.Split('/')[youtubeImages.Split('/').Length - 2];
            }
            return ytbIds;
        }
        public static string ToKMB(this decimal num)
        {

            if (num > 999999999 || num < -999999999)
            {
                return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
            }
            else if (num > 999999 || num < -999999)
            {
                return num.ToString("0,,.##M", CultureInfo.InvariantCulture);
            }
            else if (num > 999 || num < -999)
            {
                return num.ToString("0,.#K", CultureInfo.InvariantCulture);
            }
            else
            {
                return num.ToString(CultureInfo.InvariantCulture);
            }
        }
        public static string GetRandomString(int size, bool lowerCase)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static int GetDiscountPersen(decimal oldPrice, decimal newPrive)
        {
            try
            {
                return Convert.ToInt32(((oldPrice - newPrive) / oldPrice) * 100);
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public static string RemoveHexadecimalSymbols(string txt, bool htmlDecode = true)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                if (htmlDecode)
                {
                    txt = HttpUtility.HtmlDecode(txt);
                }
                string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
                return Regex.Replace(txt, r, "", RegexOptions.Compiled).Replace(@"\u001", string.Empty).Replace("0x1D", string.Empty);
            }
            else
            {
                return "";
            }
        }

        public static string GetSafeHtml(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Microsoft.Security.Application.Sanitizer.GetSafeHtmlFragment(input);
            }
            else
            {
                return string.Empty;
            }
        }
        public static string HtmlEncodeAntiXss(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(input, false);
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// DataTable to html table
        /// </summary>
        /// <param name="DataTables"></param>
        /// <param name="mapPingColunmName"></param>
        /// <param name="ignoreColunm"></param>
        /// <returns></returns>
        public static string AutoCreateHtmlTable(System.Data.DataTable DataTables, string mapPingColunmName, List<string> ignoreColunm)
        {
            string outPut = "<table class=\"table table-bordered\">";
            List<string> columnNames = new List<string>();
            Boolean haveData = false;
            if (DataTables != null && DataTables.Rows.Count > 0)
            {
                haveData = true;
            }
            if (haveData)
            {
                outPut += "<tr>";
                for (int i = 0; i < DataTables.Columns.Count; i++)
                {
                    if (ignoreColunm != null)
                    {
                        if (!ignoreColunm.Contains(DataTables.Columns[i].ColumnName))
                        {
                            outPut += "<th data-col=\"c-" + i + "\">" + Ultil.StringHelper.MultiTextReplace(DataTables.Columns[i].ColumnName, mapPingColunmName) + "</th>";
                        }

                    }
                    else
                    {
                        outPut += "<th  class=\"c-" + i + "\">" + Ultil.StringHelper.MultiTextReplace(DataTables.Columns[i].ColumnName, mapPingColunmName) + "</th>";
                    }
                }
                outPut += "</tr>";
                if (DataTables != null && DataTables.Rows.Count > 0)
                {
                    for (int i = 0; i < DataTables.Rows.Count; i++)
                    {
                        outPut += "<tr>";
                        for (int j = 0; j < DataTables.Columns.Count; j++)
                        {
                            if (ignoreColunm != null)
                            {
                                if (!ignoreColunm.Contains(DataTables.Columns[j].ColumnName))
                                {
                                    outPut += "<td data-col=\"c-" + j + "\">" + (DBNull.Value == DataTables.Rows[i][j] ? "" : DataTables.Rows[i][j].ToString()) + "</td>";

                                }

                            }
                            else
                            {
                                outPut += "<td data-col=\"c-" + j + "\">" + (DBNull.Value == DataTables.Rows[i][j] ? "" : DataTables.Rows[i][j].ToString()) + "</td>";
                            }

                        }
                        outPut += "</tr>";
                    }
                }
                outPut += "</tr>";
            }


            return outPut += "</table>";
        }
        public static String RemoveExtenSion(String FileName)
        {
            int i = FileName.IndexOf(".");
            String NewFileName = FileName.Substring(0, i);
            NewFileName = RemoveHtmlTangs(NewFileName);
            if (NewFileName.Length > 20)
            {
                NewFileName = NewFileName.Substring(0, 20);
            }
            return NewFileName;
        }
        public static String RemoveATangs(String s)
        {
            return Regex.Replace(s, @"<a\b[^>]+>([^<]*(?:(?!</a)<[^<]*)*)</a>", "$1");

        }

        public static String ToEndcodedingLeveForum(String s)
        {
            if (s != null)
            {
                s = s.Replace(@"'", @" &quot;");
                s = s.Replace(@"\u003c", "[");
                s = s.Replace(@"&quot;", @" &quot;");
                s = s.Replace("<br />", "[br /]");
                s = s.Replace("<table>", "[table]");

                s = s.Replace("<strong>", "[strong]");
                s = s.Replace("</strong>", "[/strong]");

                s = s.Replace("<tr>", "[tr]");

                s = s.Replace("<i>", "[i]");
                s = s.Replace("</i>", "[/i]");

                s = s.Replace("<ul>", "[ul]");
                s = s.Replace("</ul>", "[/ul]");

                s = s.Replace("<li>", "[li]");
                s = s.Replace("</li>", "[/li]");


                s = s.Replace("<em>", "[em]");
                s = s.Replace("</em>", "[/em]");


                s = s.Replace("<b>", "[b]");
                s = s.Replace("</b>", "[/b]");

                s = s.Replace("<code>", "[code]");
                s = s.Replace("</code>", "[/code]");

                s = s.Replace("<tr>", "[tr]");
                s = s.Replace("</tr>", "[/tr]");

                s = s.Replace("<td>", "[td]");
                s = s.Replace("</td>", "[/td]");

                s = s.Replace("</h3>", "[/h3]");
                s = s.Replace("<h3>", "[h3]");

                s = s.Replace("<h4>", "[h4]");
                s = s.Replace("<h4>", "[h4]");

                s = s.Replace("</h5>", "[/h5]");
                s = s.Replace("<h5>", "[h5]");

                s = RemoveImgTangs(s);
                s = Ultil.StringHelper.RemoveScript(s);
                s = RemoveHtmlTangs(s);

                /// remove atag fist

                s = s.Replace(">", "]");

                //dau nho hon 10,16,html, unicode
                s = s.Replace("<", "[");
                s = s.Replace(@"&#60", "[");
                s = s.Replace(@"&#x3C", "[");
                s = s.Replace(@"&lt;", "[");
                s = s.Replace(@"\u003c", "[");

                s = s.Replace("--", "_");


                //dau nhay don 10,16,html, unicode
                s = s.Replace("'", "");
                s = s.Replace(@"&#39", "");
                s = s.Replace(@"&#x27", "");
                s = s.Replace(@"&apos;", "");
                s = s.Replace(@"\u0027", "");
                s = HttpUtility.HtmlEncode(s);
                s = s.Replace(@"&amp;nbsp;", "&nbsp;");
                s = s.Replace(@"&amp;quot;", "&quot;");
                return s;

            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// trả về chuỗi html phân trang
        /// </summary>
        /// <param name="curentPage">Trang hiện tại</param>
        /// <param name="pageSite">Số tin trên trang</param>
        /// <param name="newsCount">Tổng số tin</param>
        /// <param name="MaxPage">Số phân trang tối đa, số trang tối đa trong menu phân trang</param>
        ///<param name="query">query có dạng /page nếu để param không cần bao gồm dấu ? hoặc &</param>
        /// <returns>String</returns> 
        public static string SetUpPagedV2(int curentPage, int pageSite, int itemCount, int MaxPage, string paramName)
        {
            String pagedString = "";
            paramName = paramName.Replace(@"?", "").Replace(@"&", "");
            int totallPaed = itemCount / pageSite;
            if (itemCount % pageSite > 0)
            {
                totallPaed += 1;
            }
            #region get link
            string CurentUrl = HttpContext.Current.Request.RawUrl.ToString();
            String pageaspx = CurentUrl;
            int legth = CurentUrl.LastIndexOf("?" + paramName);
            if (legth > 0)
            {
                pageaspx = CurentUrl.Substring(0, legth);
            }
            else
            {
                legth = CurentUrl.LastIndexOf("&" + paramName);
                if (legth > 0)
                {
                    pageaspx = CurentUrl.Substring(0, legth);
                }
            }
            if (pageaspx.Contains(@"?"))
            {
                paramName = "&" + paramName;
            }
            else
            {
                paramName = "?" + paramName;
            }
            #endregion
            // string path = HttpContext.Current.Request.Url.AbsolutePath;
            // // /TESTERS/Default6.aspx
            //nếu số tin lới hơn số tin trên trang mới hiển thị phân trang
            if (itemCount > pageSite)
            {
                pagedString = "<ul class='pagination flex flex-jc-center'>";
                if (curentPage == 1)
                {
                    pagedString += @"<li class='page-item'><a class='page-link' href='javascript:void(0);' tittle='Bạn đang ở trang đầu'>&laquo;</a></li>";
                }
                else
                {
                    pagedString += @"<li  class='page-item'><a class='page-link' href='" + pageaspx + paramName + (curentPage - 1) + "'>&laquo;</a></li>";
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
                            pagedString += @"<li class='page-item active'><a class='page-link' href='javascript:void(0);' tittle='đang ở trang này'>" + (i) + "</a><li>";

                        }
                        else
                        {
                            pagedString += @"<li><a class='page-link' href='" + pageaspx + paramName + i + "'>" + i + "</a></li>";
                        }

                    }



                }

                if (curentPage == totallPaed)
                {
                    pagedString += @"<li class='page-item active'><a class='page-link' href='javascript:void(0);' tittle='bạn đang ở trang cuôi'>&raquo;</a><li>";

                }
                else
                {
                    pagedString += @"<li class='page-item'><a class='page-link' href='" + pageaspx + paramName + totallPaed + @"'> . " + totallPaed + @"</a></li>";
                    pagedString += @"<li class='page-item'><a class='page-link' href='" + pageaspx + paramName + (curentPage + 1) + "'>&raquo;</a></li>";

                }
                pagedString += @"</ul>";
            }



            return pagedString;
        }
        public static String RemoveImgTangs(String s)
        {


            return Regex.Replace(s, @"<img\b[^>]+>", string.Empty);

        }
        /// <summary>
        /// hàm này xóa triệt để các thành phần html chỉ giữ lại một số thẻ cơ bản và chuyển sang encode
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlDecodeIngLeverForum(String s)
        {
            s = s.Replace("''", "'");
            s = s.Replace(@"&#60", "[");
            s = s.Replace(@"&#x3C", "[");

            s = s.Replace(@"\u003c", "[");
            s = s.Replace(@"&quot;", @" &quot;");
            s = s.Replace("[br /]", "<br />");
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
            s = s.Replace("[br]", "<br>");

            s = s.Replace("[h4]", "<h4>");

            s = s.Replace("[/h5]", "</h5>");

            s = s.Replace("[h5]", "<h5>");

            s = s.Replace("[i]", "<i>");
            s = s.Replace("[/i]", "</i>");
            s = HttpUtility.HtmlDecode(s);
            s = Ultil.StringHelper.imgBBCodeToImgTag(s);
            s = RemoveScript(s);
            s = RemoveIframe(s);
            return s;
        }
        public static string RemoveHtmlTagLevelAdmin(String s)
        {
            s = s.Replace("''", "'");
            s = s.Replace(@"&#60", "[");
            s = s.Replace(@"&#x3C", "[");
            s = s.Replace(@"\u003c", "[");
            s = s.Replace(@"&quot;", @" &quot;");
            s = s.Replace(@"\u003c", "[");
            s = s.Replace(@"&quot;", @" &quot;");
            s = s.Replace("<br />", "[br /]");
            s = s.Replace("<table>", "[table]");

            s = s.Replace("<strong>", "[strong]");
            s = s.Replace("</strong>", "[/strong]");



            s = s.Replace("''", "'");

            s = s.Replace(@"&#60", "[");

            s = s.Replace(@"&#x3C", "[");


            s = s.Replace(@"\u003c", "[");
            s = s.Replace(@"&quot;", @" &quot;");
            s = s.Replace("[br /]", "<br />");
            s = s.Replace("[table]", "<table>");

            s = s.Replace("[strong]", "<strong>");
            s = s.Replace("[/strong]", "</strong>");

            s = s.Replace("[tr]", "<tr>");
            s = s.Replace("[/tr]", "</tr>");

            s = s.Replace("[td]", "<td>");
            s = s.Replace("[/td]", "</td>");
            s = s.Replace("[br]", "<br>");


            s = s.Replace("[/span]", "</span>");

            s = s.Replace("[b]", "<b>");
            s = s.Replace("[/b]", "</b>");

            s = s.Replace("[code]", "<code>");
            s = s.Replace("[/code]", "</code>");

            s = s.Replace("[em]", "<em>");
            s = s.Replace("[/em]", "</em>");

            s = s.Replace("[/h3]", "</h3>");
            s = s.Replace("[h3]", "<h3>");

            s = s.Replace("[h4]", "<h4>");

            s = s.Replace("[h4]", "<h4>");

            s = s.Replace("[/h5]", "</h5>");

            s = s.Replace("[h5]", "<h5>");

            s = s.Replace("[i]", "<i>");
            s = s.Replace("[/i]", "</i>");

            s = s.Replace("[/p]", "</p>");
            s = s.Replace("[p]", "<p>");
            s = RemoveScript(s);
            s = RemoveIframe(s);
            return s;
        }
        public static string RemoveIframe(String s)
        {
            return Regex.Replace(s, "<iframe.*?</iframe>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
        public static string RemoveCss(String s)
        {
            return Regex.Replace(s, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
        public static string MultiTextReplace(string input, string mapping)
        {
            if (!string.IsNullOrEmpty(mapping))
            {
                string[] mapingList = mapping.Split(',');
                if (mapingList.Length > 0)
                {
                    for (int i = 0; i < mapingList.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(mapingList[i]))
                        {
                            string[] rule = mapingList[i].Split('>');
                            input = Regex.Replace(input, rule[0].Trim(), rule[1].Trim(), RegexOptions.IgnoreCase);
                            if (input.Equals(rule[1].Trim()))
                                return input;
                        }
                    }
                }
            }
            return input;
        }
        public static bool isInternetUrl(String url)
        {
            string pattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

            if (Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase))
            {
                return true;

            }
            else
            {


                return false;
            }

        }
        public static string URLBBCodeToATag(String s)
        {
            s = Regex.Replace(s, @"\[URL\](http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)\[\/URL\]", @"<a href=""$1"" target='_blank' rel='nofollow'/>$1</a>", RegexOptions.IgnoreCase);
            return s;
        }
        public static bool isEmail(String Email)
        {

            // string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            try
            {
                if (Email != null)
                {
                    return Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }


        }
        public static Boolean isYouToBeLink(String link)
        {


            try
            {
                string pattern = @"^http://youtu.be/([A-Z0-9]*_?\-?[A-Z0-9]*)*$";
                if (Regex.IsMatch(link, pattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string GetYouTubeAgument(String link)
        {
            try
            {

                int lengths = link.Length;
                int i = link.LastIndexOf("/");
                return link.Substring(i, lengths - i);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string imgBBCodeToImgTag(String s)
        {
            s = Regex.Replace(s, @"\[IMG\](https?://(?:[a-z0-9\-\.]+\.)+[a-z0-9]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png|jpeg))\[\/IMG\]", @"<img src=""$1""/>", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"\[IMG alt=""([^/><*&?]+)""\](https?://(?:[a-z0-9\-\.]+\.)+[a-z0-9]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png|jpeg))\[\/IMG\]", @"<img src=""$2"" alt=""$1""/>", RegexOptions.IgnoreCase);
            return s;
        }
        public static string ImgTagtoimgBBCode(String s)
        {
            s = Regex.Replace(s, @"<img src=""(https?://(?:[a-z0-9\-\.]+\.)+[a-z0-9]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png|jpeg))"">", @"[IMG]$1[/IMG]", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"<img src=""(https?://(?:[a-z0-9\-\.]+\.)+[a-z0-9]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png|jpeg))"" \/>", @"[IMG]$1[/IMG]", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"<img src=""(https?://(?:[a-z0-9\-\.]+\.)+[a-z0-9]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png|jpeg))"" alt=""([^/><*&?]+)""\/>", @"[IMG alt=""$2""]$1[/IMG]", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"<img src=""(https?://(?:[a-z0-9\-\.]+\.)+[a-z0-9]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png|jpeg))"" alt=""([^/><*&?]+)"">", @"[IMG alt=""$2""]$1[/IMG]", RegexOptions.IgnoreCase);
            return s;
        }
        /// <summary>
        /// trả về chuỗi html phân trang
        /// </summary>
        /// <param name="curentPage">Trang hiện tại</param>
        /// <param name="pageSite">Số tin trên trang</param>
        /// <param name="newsCount">Tổng số tin</param>
        /// <param name="MaxPage">Số phân trang tối đa, số trang tối đa trong menu phân trang</param>
        ///<param name="query">query có dạng /page hoặc ?page</param>
        /// <returns>String</returns> 
        public static string SetupAjaxPage(int curentPage, int pageSite, int newsCount, int MaxPage)
        {
            String pagedString = "";
            if (newsCount >= 1000)
            {
                newsCount = 1000;
            }
            int totallPaed = newsCount / pageSite;
            // string path = HttpContext.Current.Request.Url.AbsolutePath;
            //TESTERS/Default6.aspx
            //nếu số tin lới hơn số tin trên trang mới hiển thị phân trang
            if (newsCount > pageSite)
            {
                pagedString = "<ul class='pagination'>";
                if (curentPage == 1)
                {
                    pagedString += @"<li><a href='#' tittle='Bạn đang ở trang đầu'>&laquo;</a></li>";
                }
                else
                {
                    pagedString += @"<li><a href='javascript:s(" + (curentPage - 1) + ");'>&laquo;</a></li>";
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
                            pagedString += @"<li><a href='javascript:void(0);' tittle='đang ở trang này'>" + (i) + "</a><li>";

                        }
                        else
                        {
                            pagedString += @"<li><a href='javascript:s(" + i + ");'>" + i + "</a></li>";
                        }

                    }



                }

                if (curentPage == totallPaed)
                {
                    pagedString += @"<li><a href='#' tittle='bạn đang ở trang cuôi'>&raquo;</a><li>";

                }
                else
                {
                    pagedString += @"<li><a href='javascript:s(" + totallPaed + @");'> . " + totallPaed + @"</a></li>";
                    pagedString += @"<li><a href='javascript:s(" + (curentPage + 1) + ");'>&raquo;</a></li>";

                }
                pagedString += @"</ul>";
            }
            return pagedString;
        }
        public static string SetupAjaxPage(int curentPage, int pageSite, int newsCount, int MaxPage, string funcName)
        {
            String pagedString = "";

            int totallPaed = newsCount / pageSite;
            if (newsCount % pageSite > 0)
            {
                totallPaed += 1;
            }
            // string path = HttpContext.Current.Request.Url.AbsolutePath;
            //TESTERS/Default6.aspx
            //nếu số tin lới hơn số tin trên trang mới hiển thị phân trang
            if (newsCount > pageSite)
            {
                pagedString = "<ul class=\"pagination flex flex-jc-center\">";
                if (curentPage == 1)
                {
                    pagedString += "<li class=\"page-item active\"><a class=\"page-link\" href=\"#\" tittle=\"Bạn đang ở trang đầu\">&laquo;</a></li>";
                }
                else
                {
                    pagedString += "<li class=\"page-item\"><a class=\"page-link\" href=\"javascript:" + funcName + "(" + (curentPage - 1) + ");\">&laquo;</a></li>";
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

                    for (int i = currenttag; i <= j; i++)
                    {
                        if (i == curentPage)
                        {
                            pagedString += "<li  class=\"page-item active\"><a class=\"page-link\" href=\"javascript:void(0);\" title=\"đang ở trang này\" >" + (i) + "</a><li>";

                        }
                        else
                        {
                            pagedString += "<li class=\"page-item\"><a class=\"page-link\" href=\"javascript:" + funcName + "(" + i + ");\">" + i + "</a></li>";
                        }
                    }
                }

                if (curentPage == totallPaed)
                {
                    pagedString += "<li  class=\"page-item active\"><a class=\"page-link\" href=\"#\" title=\"bạn đang ở trang cuôi\">&raquo;</a><li>";

                }
                else
                {
                    pagedString += "<li class=\"page-item\"><a class=\"page-link\" href=\"javascript:" + funcName + "(" + totallPaed + ");\"> . " + totallPaed + "</a></li>";
                    pagedString += "<li class=\"page-item\"><a class=\"page-link\" href=\"javascript:" + funcName + "(" + (curentPage + 1) + ");\">&raquo;</a></li>";
                }
                pagedString += @"</ul>";
            }
            return pagedString;
        }
        public static string RemoveScript(String s)
        {
            ///check
            ///
            if (string.IsNullOrEmpty(s))
            {

                return s;
            }
            else
            {
                s = s.Replace("--", "_");
                s = s.Replace("'", "");

            }

            return Regex.Replace(s, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        }
        public static string checkHackSql(String s)
        {
            s = s.ToLower();
            s = RemoveScript(s);
            s = s.Replace("--", "_");
            s = s.Replace("'", "");
            s = Regex.Replace("drop", "/kg", "drops", RegexOptions.IgnoreCase);
            s = Regex.Replace("@", "/kg", "(at)", RegexOptions.IgnoreCase);
            // s = Regex.Replace("update", "/kg", "updates", RegexOptions.IgnoreCase);
            //s = Regex.Replace("insert", "/kg", "inserts", RegexOptions.IgnoreCase);


            return s;
        }
        public static String RemoveHtmlTangs(String s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return Regex.Replace(s, @"<.*?>", string.Empty);

            }
            else { return s; }

        }
        #region const
        public const string uniChars =
          "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

        public const string KoDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
        #endregion
        public static bool isVnPhone(String input)
        {
            bool valid = true;

            if (input != null && input.Length > 9)
            {
                input = input.Replace(" ", String.Empty).Trim();
                input = input.Replace("+", "0").Replace("-", "").Replace(".", "");


                if (input.Length < 10 || input.Length > 16)
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }

            try
            {
                Convert.ToDouble(input);
                valid = true;
            }
            catch (Exception)
            {

                valid = false;
            }


            return valid;
        }
        public static string UnicodeToKoDau(string s)
        {
            string retVal = String.Empty;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }
        public static string ToURLgach(String title)
        {
            title = UnicodeToKoDau(title).ToLower();
            Regex regex = new Regex("[^a-zA-Z0-9 -]");
            title = regex.Replace(title, String.Empty);


            if (title.Length > 35)
            {
                title = title.Substring(0, 35);

            }
            title = title.Trim().ToLower();
            title = title.Replace(" ", "-").Replace(@"--", "-");
            return title.Replace(@"--", "-").Replace(@"–", "");
        }
        public static string ConVertToMoneyString(String s)
        {

            try
            {

                int i = s.Length;
                if (i <= 3)
                {
                    return s;
                }
                else
                {
                    int j = i % 3;
                    int k = i / 3;
                    String s1 = s.Substring(0, j);
                    s.Insert(3, ",");
                    String sss = " ";
                    for (int ii = 0; ii < k; ii++)
                    {
                        if (ii == k - 1)
                        {
                            sss += s.Substring(j, 3);

                        }
                        else
                        {
                            sss += s.Substring(j, 3) + ",";
                        }
                        j += 3;
                    }

                    if (s1.Equals("") || s1.Equals(" ") || s1 == null)
                    {
                        return sss;
                    }
                    else
                    {
                        return s1 + "," + sss;
                    }



                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string toURLgachTag(String title)
        {
            try
            {
                title = UnicodeToKoDau(title);
                Regex regex = new Regex("[^a-zA-Z0-9 -,]");
                title = regex.Replace(title, String.Empty);
                if (title.Length > 150)
                {
                    title = title.Substring(0, 150);

                }
                title = title.Trim().ToLower();
                title = title.Replace(" ", "-").Replace(@"--", "-");
                return title.Replace(@"--", "-").Replace(@"–", "");
            }
            catch (Exception)
            {
                return "";

            }

        }
        public static string SubString(int maxLeng, string inPut)
        {
            if (!String.IsNullOrEmpty(inPut))
            {
                if (inPut.Length >= maxLeng)
                {
                    inPut = inPut.Substring(0, maxLeng - 2) + " ..";
                }
            }

            return inPut??string.Empty;


        }
        public static string ConVertToMoneyFormatInt(string s)
        {
            int dotIndex = s.LastIndexOf(".");
            if (s != null && !DBNull.Value.Equals(s))
            {
                if (s == "0" || s == "0.00")
                {
                    return "0";
                }
                if (s.Length > (dotIndex + 3) && dotIndex != -1)
                {
                    s = s.Substring(0, dotIndex + 2);

                }
                return string.Format("{0:##,###}", Convert.ToDecimal(s));

            }
            else
            {
                return "0";
            }
        }
        public static string ConVertToMoneyFormat(decimal value)
        {
            return ConVertToMoneyFormat(value.ToString());
        }
        public static string ConVertToMoneyFormat(string s)
        {
            int dotIndex = s.LastIndexOf(".");
            if (s != null && !DBNull.Value.Equals(s))
            {
                if (s == "0" || s == "0.00")
                {
                    return "0";
                }
                if (s.Length > (dotIndex + 3) && dotIndex != -1)
                {
                    s = s.Substring(0, dotIndex + 2);

                }
                return string.Format("{0:##,###}", Convert.ToDecimal(s));

            }
            else
            {
                return null;
            }
        }
        public static string toURLEncode(String title)
        {

            return System.Web.HttpUtility.UrlEncode(title);
        }
        public static string ToSplitURLgach(String title)
        {
            title = RemoveHtmlTangs(title);

            title = UnicodeToKoDau(title);
            Regex regex = new Regex("[^a-zA-Z0-9 -]");
            title = regex.Replace(title, String.Empty);
            if (title.Length > 50)
            {
                title = title.Substring(0, 50);

            }
            title = title.Trim();
            title = title.Replace("'", String.Empty).Replace("|", String.Empty).Replace(" ", "-").Replace("!", "").Replace(" ", "-").Replace(@"\", String.Empty).Replace(@"/", String.Empty).Replace(@"?", "").Replace(@"<", "").Replace(@">", "").Replace(@"(", "").Replace(@")", "").Replace(@"--", "-").Replace(@":", String.Empty);
            return System.Web.HttpUtility.UrlEncode(title).Replace(@"%0", "").Replace(@"%1", "").Replace(@"%2c", ",").Replace(@"%3a", String.Empty);
        }

        /// <summary>
        /// Chuyển dạng số dang dạng chứ đọc tiếng vie
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToStringVN(Decimal number)
        {
            string s = number.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            bool booAm = false;
            double decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDouble(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "Âm " + str;
            return UppercaseFirst(str) + "đồng chẵn.";
        }
        public static string ToStringVN(int number)
        {
            string s = number.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            double decS = 0;
            //Tung addnew
            try
            {
                decS = Convert.ToDouble(s.ToString());
            }
            catch
            {
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            return UppercaseFirst(str);
        }

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
    public static class Times
    {
        public static DateTime ConvertTodateTime(string inputString, bool getEndOfDay = false)
        {
            DateTime datetime = new DateTime();
            try
            {
                datetime = DateTime.ParseExact(inputString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None);
                if (getEndOfDay)
                {
                    datetime.AddHours(23);
                }
            }
            catch (Exception)
            {
                try
                {
                    try
                    {
                        string year = inputString.Substring(0, 4);
                        string month = inputString.Substring(4, 2);
                        string day = inputString.Substring(6, 2);
                        datetime = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                    }
                    catch (Exception)
                    {
                    }

                }
                catch (Exception)
                {

                }
                //                datetime = DateTime.ParseExact(inputString, "dd/MM/yyyy h:mm tt", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            return datetime;
        }
        public static DateTime? ConvertToDateTime(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return null;
            }
            else
            {
                try
                {
                    return DateTime.ParseExact(inputString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                catch (Exception)
                {
                    return null;

                }
            }


        }
        public static string DateTimeTostring(Nullable<DateTime> time)
        {
            if (time.HasValue)
            {
                DateTime tempTime = time.Value;
                return (tempTime.Day < 10 ? "0" + tempTime.Day.ToString() : tempTime.Day.ToString()) + "/" + (tempTime.Month < 10 ? "0" + tempTime.Month.ToString() : tempTime.Month.ToString()) + "/" + tempTime.Year;
            }
            return string.Empty;
        }
        public static String GetCurentYearYYYYMMDDdHhmm(Boolean isBeginYear)
        {
            if (isBeginYear)
            {
                DateTime dt = DateTime.Now;


                String s6 = dt.Year.ToString();

                return s6 + "00000000";
            }
            else
            {
                DateTime dt = DateTime.Now;
                String s6 = dt.Year.ToString();
                return s6 + "99999999";
            }


        }
        public static string ConvertStringToTimeForMatVN(string YYYYMMddhhmmss)
        {
            try
            {
                if (YYYYMMddhhmmss.Length >= 8)
                {
                    String year = YYYYMMddhhmmss.Substring(0, 4);
                    String month = YYYYMMddhhmmss.Substring(4, 2);
                    String day = YYYYMMddhhmmss.Substring(6, 2);
                    return day + " tháng " + month + " năm " + year;
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
        public static string ConvertStringToTimeForMatVN(DateTime dateTime)
        {
            try
            {


                return dateTime.Date + " tháng " + dateTime.Month + " năm " + dateTime.Year;
            }
            catch (Exception)
            {

                return "0";
            }


        }
        public static String GetCurentMonthYYYYMMDDdHhmm(Boolean isBeginMonth)
        {
            if (isBeginMonth)
            {
                DateTime dt = DateTime.Now;

                String s5 = dt.Month.ToString();
                String s6 = dt.Year.ToString();
                if (s5.Length == 1)
                {
                    s5 = "0" + s5;
                }
                return s6 + s5 + "000000";
            }
            else
            {
                DateTime dt = DateTime.Now;

                String s5 = dt.Month.ToString();
                String s6 = dt.Year.ToString();
                if (s5.Length == 1)
                {
                    s5 = "0" + s5;
                }
                return s6 + s5 + "999999";
            }


        }
        public static String GetyyyyMMddhhmm(string dateTimeFormat)
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTimeFormat))
                {
                    String[] z = dateTimeFormat.Split('/');
                    if (z.Length > 2)
                    {
                        if (z[1].Length == 1)
                        {
                            z[1] = "0" + z[1];
                        }
                        if (z[0].Length == 1)
                        {
                            z[0] = "0" + z[0];
                        }
                        return z[2] + z[1] + z[0] + "2460";
                    }
                    else
                    {
                        return dateTimeFormat;
                    }

                }
            }
            catch (Exception)
            {

                return "0";
            }
            return "0";
        }
        public static String GetyyyyMMddhhmm(string dateTimeFormat, Boolean isMinimize)
        {
            try
            {
                if (!string.IsNullOrEmpty(dateTimeFormat))
                {
                    String[] z = dateTimeFormat.Split('/');
                    if (z.Length > 2)
                    {
                        if (z[1].Length == 1)
                        {
                            z[1] = "0" + z[1];
                        }
                        if (z[0].Length == 1)
                        {
                            z[0] = "0" + z[0];
                        }
                        if (isMinimize)
                        {
                            return z[2] + z[1] + z[0] + "0000";
                        }
                        else
                        {
                            return z[2] + z[1] + z[0] + "2460";
                        }
                    }
                    else
                    {
                        return dateTimeFormat;
                    }

                }
            }
            catch (Exception)
            {

                return "0";
            }
            return "0";
        }
    

        public static String GetFistDayOfMonthNow_yyyyMMddhhmm(bool isExactly)
        {
            DateTime dt = DateTime.Now;


            String s2 = "";
            String s3 = "";
            String s5 = dt.Month.ToString();
            int thisDay = dt.Day;
            if (thisDay < 15 && !isExactly)
            {
                s5 = (dt.Month - 1).ToString();
            }
            else
            {
                s5 = dt.Month.ToString();
            }
            String s4 = "01";


            String s6 = dt.Year.ToString();
            if (s5.Length == 1)
            {
                s5 = "0" + s5;
            }
            if (s4.Length == 1)
            {
                s4 = "0" + s4;
            }
            s2 = "00";
            s3 = "00";




            return s6 + s5 + s4 + s3 + s2;

        }
   
        public static String GetyyyyMMddhhmmNow(Boolean isMinTime)
        {
            DateTime dt = DateTime.Now;


            String s2 = "";
            String s3 = "";
            String s4 = dt.Day.ToString();
            String s5 = dt.Month.ToString();
            String s6 = dt.Year.ToString();
            if (s5.Length == 1)
            {
                s5 = "0" + s5;
            }
            if (s4.Length == 1)
            {
                s4 = "0" + s4;
            }
            if (isMinTime)
            {

                s2 = "00";
                s3 = "00";


            }
            else
            {
                s2 = "60";
                s3 = "24";
            }

            return s6 + s5 + s4 + s3 + s2;

        }
        public static String GetyyyyMMddhhmmNow()
        {
            DateTime dt = DateTime.Now;


            String s2 = dt.Minute.ToString();
            String s3 = dt.Hour.ToString();
            String s4 = dt.Day.ToString();
            String s5 = dt.Month.ToString();
            String s6 = dt.Year.ToString();
            if (s5.Length == 1)
            {
                s5 = "0" + s5;
            }
            if (s4.Length == 1)
            {
                s4 = "0" + s4;
            }
            if (s3.Length == 1)
            {
                s3 = "0" + s3;
            }
            if (s2.Length == 1)
            {
                s2 = "0" + s2;
            }

            return s6 + s5 + s4 + s3 + s2;

        }
        /// <summary>
        /// trả về một chuỗi thời gian hiện tại yyyymmddhhssms
        /// </summary>
        /// <returns>trả về một chuỗi thời gian hiện tại yyyymmddssmm 16 char</returns>
        public static String GetYYYYMMDDHHmmssmsNow()
        {
            DateTime dt = DateTime.Now;
            String s = dt.Millisecond.ToString();
            String s1 = dt.Second.ToString();
            String s2 = dt.Minute.ToString();
            String s3 = dt.Hour.ToString();
            String s4 = dt.Day.ToString();
            String s5 = dt.Month.ToString();
            String s6 = dt.Year.ToString();

            if (s5.Length == 1)
            {
                s5 = "0" + s5;
            }
            if (s4.Length == 1)
            {
                s4 = "0" + s4;
            }
            if (s3.Length == 1)
            {
                s3 = "0" + s3;
            }
            if (s2.Length == 1)
            {
                s2 = "0" + s2;
            }
            if (s1.Length == 1)
            {
                s1 = "0" + s1;
            }
            if (s.Length == 1)
            {
                s = "0" + s;
            }
            if (s.Length > 2)
            {
                s = s.Substring(0, 2);
            }
            return s6 + s5 + s4 + s3 + s2 + s1 + s;

        }
        public static String getSortStringByTimeNow()
        {
            DateTime dt = DateTime.Now;
            String s5 = dt.Month.ToString();
            String s6 = dt.Year.ToString();
            return s5 + s6;

        }


      
    }
}
