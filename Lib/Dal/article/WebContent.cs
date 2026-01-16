using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultil;

namespace Dal
{
    public class WebContentControl
    {
        public int UpdateWebContent(int st,int type,string content)
        {
            if (content.Length > 100000)
            {
                content = content.Substring(0, 100000);
            }

            SqlParameter[] paramList = new SqlParameter[3];
            paramList[0] = new SqlParameter("@st", SqlDbType.Int, 32);
            paramList[0].Value = st;
            paramList[1] = new SqlParameter("@type", SqlDbType.TinyInt);
            paramList[1].Value = type;
            paramList[2] = new SqlParameter("@content", SqlDbType.NText);
            paramList[2].Value =content;
     
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("UpdateWebContent", paramList);
        }
        public WebContent GetWebContent(int st, int type)
        {
            WebContent wc = new WebContent();

            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@st", SqlDbType.Int, 32);
            paramList[0].Value = st;
            paramList[1] = new SqlParameter("@type", SqlDbType.TinyInt);
            paramList[1].Value = type;            

            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable table= ds.executeSelect("getWebContent", paramList);
            if (table!=null && table.Rows.Count > 0)
            {
                wc.Content = table.Rows[0]["TextContent"].ToString();
                wc.St = st;
                wc.Type = type;
                
            }

            return wc;

        }


    }
   public class WebContent
    {
        public WebContent()
        {
            St = 0;
            Type = 0;
            Content = "";
        }
        int st, type;

        public int St
        {
            get { return st; }
            set { st = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

    }
}
