using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Configuration;

/// <summary>
/// Lớp này chuyên dùng để kiểm tran form và xxs
/// </summary>
/// 

namespace Dal
{
    //public class checkValid : System.Web.UI.Page
    //{
    //    //private String toValidImagesTag(String images) {
    //        //return Regex.Replace(images, @"^\[https?://(?:[a-z\-]+\.)+[a-z]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png)\]$", @"^\<https?://(?:[a-z\-]+\.)+[a-z]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png)\>$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    //    //}
    //    public Boolean ValidateGoogleCaptcha(string captcha)
    //    {
    //        string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + System.Configuration.ConfigurationManager.AppSettings["googleCapchaSecret"] + "&response=" + captcha + "&remoteip=" + HttpContext.Current.Request.UserHostAddress;
    //        var client = new System.Net.WebClient();
    //        var GoogleReply = client.DownloadString(url);
    //        var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaClass>(GoogleReply);
    //        return Convert.ToBoolean(captchaResponse.Success);
    //    }


    //    public Boolean ValidateCaptcha(string captcha)
    //    {
    //        String[] s = (String[])HttpContext.Current.Session["capcha"];

    //        if (s != null && captcha.Equals(s[1]))
    //        {
    //            return true;
    //        }
    //        else
    //        {

    //            return false;
    //        }

    //    }
    //    /// <summary>
    //    /// tra
    //    /// </summary>
    //    /// <param name="s"></param>
    //    /// <returns></returns>
    //    public String TourlEndCode(String s)
    //    {
    //        return Server.UrlEncode(s);
    //    }
    //    public String ToHtmlDeCode(String s)
    //    {
    //        return Server.HtmlDecode(s);
    //    }
    //    public String RemoveExtenSion(String FileName)
    //    {
    //        int i = FileName.IndexOf(".");
    //        String NewFileName = FileName.Substring(0, i);
    //        NewFileName = removeHtmlTangs(NewFileName);
    //        if (NewFileName.Length > 20)
    //        {
    //            NewFileName = NewFileName.Substring(0, 20);
    //        }
    //        return NewFileName;
    //    }
    //    public String removeATangs(String s)
    //    {
    //        return Regex.Replace(s, @"<a\b[^>]+>([^<]*(?:(?!</a)<[^<]*)*)</a>", "$1");

    //    }
    //    public String ToEndcodedingLeveForum(String s)
    //    {
    //        if (s != null)
    //        {
    //            s = s.Replace(@"'", @" &quot;");
    //            s = s.Replace(@"\u003c", "[");
    //            s = s.Replace(@"&quot;", @" &quot;");
    //            s = s.Replace("<br />", "[br /]");
    //            s = s.Replace("<table>", "[table]");

    //            s = s.Replace("<strong>", "[strong]");
    //            s = s.Replace("</strong>", "[/strong]");

    //            s = s.Replace("<tr>", "[tr]");

    //            s = s.Replace("<i>", "[i]");
    //            s = s.Replace("</i>", "[/i]");

    //            s = s.Replace("<ul>", "[ul]");
    //            s = s.Replace("</ul>", "[/ul]");

    //            s = s.Replace("<li>", "[li]");
    //            s = s.Replace("</li>", "[/li]");


    //            s = s.Replace("<em>", "[em]");
    //            s = s.Replace("</em>", "[/em]");


    //            s = s.Replace("<b>", "[b]");
    //            s = s.Replace("</b>", "[/b]");

    //            s = s.Replace("<code>", "[code]");
    //            s = s.Replace("</code>", "[/code]");

    //            s = s.Replace("<tr>", "[tr]");
    //            s = s.Replace("</tr>", "[/tr]");

    //            s = s.Replace("<td>", "[td]");
    //            s = s.Replace("</td>", "[/td]");

    //            s = s.Replace("</h3>", "[/h3]");
    //            s = s.Replace("<h3>", "[h3]");

    //            s = s.Replace("<h4>", "[h4]");
    //            s = s.Replace("<h4>", "[h4]");

    //            s = s.Replace("</h5>", "[/h5]");
    //            s = s.Replace("<h5>", "[h5]");

    //            s = removeImgTangs(s);
    //            s = Ultil.StringHelper.RemoveScript(s);
    //            s = removeHtmlTangs(s);

    //            /// remove atag fist

    //            s = s.Replace(">", "]");

    //            //dau nho hon 10,16,html, unicode
    //            s = s.Replace("<", "[");
    //            s = s.Replace(@"&#60", "[");
    //            s = s.Replace(@"&#x3C", "[");
    //            s = s.Replace(@"&lt;", "[");
    //            s = s.Replace(@"\u003c", "[");

    //            s = s.Replace("--", "_");


    //            //dau nhay don 10,16,html, unicode
    //            s = s.Replace("'", "");
    //            s = s.Replace(@"&#39", "");
    //            s = s.Replace(@"&#x27", "");
    //            s = s.Replace(@"&apos;", "");
    //            s = s.Replace(@"\u0027", "");
    //            s = Server.HtmlEncode(s);
    //            s = s.Replace(@"&amp;nbsp;", "&nbsp;");
    //            s = s.Replace(@"&amp;quot;", "&quot;");
    //            return s;

    //        }
    //        else
    //        {
    //            return "";
    //        }
    //    }
    //    public String removeImgTangs(String s)
    //    {


    //        return Regex.Replace(s, @"<img\b[^>]+>", string.Empty);

    //    }
    //    public String removeHtmlTangs(String s)
    //    {
    //        return Regex.Replace(s, @"<.*?>", string.Empty);

    //    }
    //    /// <summary>
    //    /// hàm này xóa triệt để các thành phần html chỉ giữ lại một số thẻ cơ bản và chuyển sang encode
    //    /// </summary>
    //    /// <param name="s"></param>
    //    /// <returns></returns>
    //    public String HtmlDecodeIngLeverForum(String s)
    //    {
    //        s = s.Replace("''", "'");
    //        s = s.Replace(@"&#60", "[");
    //        s = s.Replace(@"&#x3C", "[");

    //        s = s.Replace(@"\u003c", "[");
    //        s = s.Replace(@"&quot;", @" &quot;");
    //        s = s.Replace("[br /]", "<br />");
    //        s = s.Replace("[table]", "<table>");

    //        s = s.Replace("[strong]", "<strong>");
    //        s = s.Replace("[/strong]", "</strong>");

    //        s = s.Replace("[tr]", "<tr>");


    //        s = s.Replace("[/span]", "</span>");

    //        s = s.Replace("[b]", "<b>");
    //        s = s.Replace("[/b]", "</b>");
    //        s = s.Replace("[code]", "<code>");
    //        s = s.Replace("[/code]", "</code>");
    //        s = s.Replace("[/tr]", "</tr>");
    //        s = s.Replace("[td]", "<td>");
    //        s = s.Replace("[/td]", "</td>");


    //        s = s.Replace("[em]", "<em>");
    //        s = s.Replace("[/em]", "</em>");

    //        s = s.Replace("[/h3]", "</h3>");
    //        s = s.Replace("[h3]", "<h3>");

    //        s = s.Replace("[h4]", "<h4>");
    //        s = s.Replace("[br]", "<br>");

    //        s = s.Replace("[h4]", "<h4>");

    //        s = s.Replace("[/h5]", "</h5>");

    //        s = s.Replace("[h5]", "<h5>");

    //        s = s.Replace("[i]", "<i>");
    //        s = s.Replace("[/i]", "</i>");
    //        s = Server.HtmlDecode(s);
    //        s = Ultil.StringHelper.imgBBCodeToImgTag(s);
    //        s = RemoveScript(s);
    //        s = RemoveIframe(s);
    //        return s;
    //    }
    //    /// <summary>
    //    /// Hàm này chỉ xóa một số ít thành phần html( nguy hiểm) chỉ dùng trong trang admin có tính tin cậy cao, không chuển sang encode
    //    /// </summary>
    //    /// <param name="s">tham số cần lọc</param>
    //    /// <returns>trả về chuỗi chưa encode</returns>
    //    public String removeHtmlTagLevelAdmin(String s)
    //    {
    //        s = s.Replace("''", "'");
    //        s = s.Replace(@"&#60", "[");
    //        s = s.Replace(@"&#x3C", "[");
    //        s = s.Replace(@"\u003c", "[");
    //        s = s.Replace(@"&quot;", @" &quot;");
    //        s = s.Replace(@"\u003c", "[");
    //        s = s.Replace(@"&quot;", @" &quot;");
    //        s = s.Replace("<br />", "[br /]");
    //        s = s.Replace("<table>", "[table]");

    //        s = s.Replace("<strong>", "[strong]");
    //        s = s.Replace("</strong>", "[/strong]");



    //        s = s.Replace("''", "'");

    //        s = s.Replace(@"&#60", "[");

    //        s = s.Replace(@"&#x3C", "[");


    //        s = s.Replace(@"\u003c", "[");
    //        s = s.Replace(@"&quot;", @" &quot;");
    //        s = s.Replace("[br /]", "<br />");
    //        s = s.Replace("[table]", "<table>");

    //        s = s.Replace("[strong]", "<strong>");
    //        s = s.Replace("[/strong]", "</strong>");

    //        s = s.Replace("[tr]", "<tr>");
    //        s = s.Replace("[/tr]", "</tr>");

    //        s = s.Replace("[td]", "<td>");
    //        s = s.Replace("[/td]", "</td>");
    //        s = s.Replace("[br]", "<br>");


    //        s = s.Replace("[/span]", "</span>");

    //        s = s.Replace("[b]", "<b>");
    //        s = s.Replace("[/b]", "</b>");

    //        s = s.Replace("[code]", "<code>");
    //        s = s.Replace("[/code]", "</code>");

    //        s = s.Replace("[em]", "<em>");
    //        s = s.Replace("[/em]", "</em>");

    //        s = s.Replace("[/h3]", "</h3>");
    //        s = s.Replace("[h3]", "<h3>");

    //        s = s.Replace("[h4]", "<h4>");

    //        s = s.Replace("[h4]", "<h4>");

    //        s = s.Replace("[/h5]", "</h5>");

    //        s = s.Replace("[h5]", "<h5>");

    //        s = s.Replace("[i]", "<i>");
    //        s = s.Replace("[/i]", "</i>");

    //        s = s.Replace("[/p]", "</p>");
    //        s = s.Replace("[p]", "<p>");
    //        s = RemoveScript(s);
    //        s = RemoveIframe(s);
    //        return s;
    //    }


    //    public String RemoveScript(String s)
    //    {
    //        return Regex.Replace(s, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    //    }
    //    public String RemoveCss(String s)
    //    {
    //        return Regex.Replace(s, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    //    }
    //    public String RemoveIframe(String s)
    //    {
    //        return Regex.Replace(s, "<iframe.*?</iframe>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    //    }




    //}
    //public class ReCaptchaClass
    //{

    //    [JsonProperty("success")]
    //    public string Success
    //    {
    //        get { return m_Success; }
    //        set { m_Success = value; }
    //    }

    //    private string m_Success;
    //    [JsonProperty("error-codes")]
    //    public List<string> ErrorCodes
    //    {
    //        get { return m_ErrorCodes; }
    //        set { m_ErrorCodes = value; }
    //    }


    //    private List<string> m_ErrorCodes;
    //}
}