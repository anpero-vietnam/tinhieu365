using AModul;
using AModul.Common;
using AModul.Dapper;
using AModul.Product;
using AModul.ProductProperties;
using Models;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ultil;

namespace StoreMng
{
    public class WebConfigControl:ConnectionProxy<Webconfig>
    {
        int shortCacheTime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["shortCacheTime"]);
        public int UpdateWebConfig(Webconfig configModel)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@hotline", configModel.HotLine);
                paramlist.Add("@Skype", configModel.Skype);                
                paramlist.Add("@facebookLink", configModel.FaceBookLink);
                paramlist.Add("@email", configModel.Email);
                paramlist.Add("@address", configModel.Address);
                paramlist.Add("@otherPhone", configModel.OtherPhone);
                paramlist.Add("@Logo", configModel.Logo);
                paramlist.Add("@Favicon", configModel.Favicon);
                paramlist.Add("@Title", configModel.Title);
                paramlist.Add("@ShortDesc", configModel.ShortDesc);
                return base.ExecuteProc("sp_AddWebConfig", paramlist);
            }
            catch (Exception)
            {
                return 0;
                throw;
            }


        }
        public int UpdateJavaScript(string javascript)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@javascript", javascript);             
                return base.ExecuteProc("sp_UpdateJavascript", paramlist);
            }
            catch (Exception)
            {
                return 0;
                throw;
            }


        }
        
      
        public Webconfig GetOriginWebConfig()
        {
            Webconfig webConfig = new Webconfig();
            List<SqlParameter> paramList = new List<SqlParameter>();
            DatabaseAccess ds = new DatabaseAccess();
            DataTable table = ds.executeSelect("GET_WEBCONFIG", paramList.ToArray());
            if (table != null && table.Rows.Count > 0)
            {
                webConfig.FaceBookLink = table.Rows[0]["FacebookLink"].ToString();
                webConfig.Footer = table.Rows[0]["footerText"].ToString();
                webConfig.HotLine = table.Rows[0]["HotLine"].ToString();
                webConfig.Logo = table.Rows[0]["Logo"].ToString();
                webConfig.Skype = table.Rows[0]["Skype"].ToString();
                webConfig.Favicon = table.Rows[0]["Favicon"] == DBNull.Value ? "" : table.Rows[0]["Favicon"].ToString();                
                webConfig.PageScript = table.Rows[0]["pageScript"].ToString();
                webConfig.ShortDesc = table.Rows[0]["Shortdesc"].ToString();
                webConfig.Email = table.Rows[0]["EMAIL"].ToString();
                webConfig.Address = table.Rows[0]["address"].ToString();
                webConfig.Title = table.Rows[0]["Title"].ToString(); 
                webConfig.OtherPhone = table.Rows[0]["OtherPhone"].ToString();
         

            }
            return webConfig;
        }
        public Webconfig AdminGetWebConfig()
        {
            return base.SelectSingle("GET_WEBCONFIG_2")??new Webconfig();
        }

        public Webconfig GetWebconfig()
        {
            Webconfig webconfig = new Webconfig();
            IProperties properties = new PropertiesControl();
            MenuControl mc = new MenuControl();
            webconfig = GetOriginWebConfig();
            #region getMenu
            webconfig.MenuList = mc.GetMenu(0, true);
            webconfig.FooterMenuList = mc.GetMenu(0, true, AEnum.MenuPosition.FooterMenu);
            #endregion getMenu

            
          
            return webconfig;
        }
        public string GetAnperoPlugin()
        {
            string rs = "";
            rs += "<script type='text/javascript'>";
            string googleSiteKey = System.Configuration.ConfigurationManager.AppSettings["googleCapchaSitekey"];
            rs += "var googleCapchaSitekey=\"" + googleSiteKey + "\"";
            rs += "</script>";
            return rs;
        }

        public Webconfig GetCommonConfig()
        {
            Webconfig wc = new Webconfig();
            string cacheKey = AEnum.Cache.GetCommonInfoKey();
            if (!Ultil.Cache.CacheHelper.TryGet<Webconfig>(cacheKey, out wc))
            {
                wc = GetWebconfig();
                wc.AnperoPlugin = GetAnperoPlugin();
                Ultil.Cache.CacheHelper.Set<Webconfig>(cacheKey, wc, shortCacheTime);
            }
            return wc;
        }
        public void ClearCache()
        {
            Ultil.Cache.CacheHelper.Remove(AEnum.Cache.GetCommonInfoKey());

        }
    }
}
