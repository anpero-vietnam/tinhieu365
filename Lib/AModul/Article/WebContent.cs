using Models.Modul.Article;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace AModul.Article
{
    public class WebContentControl:AModul.Dapper.ConnectionProxy<WebContentModel>
    {
        public int UpdateWebContent(int type,string content)
        {
            if (content.Length > 100000)
            {
                content = content.Substring(0, 100000);
            }
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            
            paramList.Add("@type", type);
            paramList.Add("@content", content);
            return base.ExecuteProc("UpdateWebContent",  paramList);            
        }
        public WebContentModel GetWebContent(int type)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();            
            paramList.Add("@type", type);
            return base.SelectSingle("getWebContent",paramList);          
        }
    }
   
}
