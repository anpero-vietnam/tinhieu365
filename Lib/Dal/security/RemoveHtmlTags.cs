using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for RemoveATags
/// </summary>
public class RemoveHtmlTags
{
    public RemoveHtmlTags()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public String removeATangs(String s)
    {
        return Regex.Replace(s, @"<a\b[^>]+>([^<]*(?:(?!</a)<[^<]*)*)</a>", "$1");

    }
    public String removeHtmlTangs(String s)
    {
        return Regex.Replace(s, @"<.*?>", string.Empty);

    }
    public String RemoveScript(String s)
    {
        return Regex.Replace(s, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    }
}