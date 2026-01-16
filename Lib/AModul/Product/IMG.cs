using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Models.Modul.Product;
using Models;
using Ultil.Cache;
/// <summary>
/// Summary description for NewImg
/// </summary>
/// 
namespace AModul.Product
{

    public class Img : AModul.Dapper.ConnectionProxy<Ads>
    {
        public Img(){} 
        public int AddNewsImg(string referId, string url, string author, String clickUrl, Decimal size,string desc)
        {
            try
            {
                string cacheKey = "ads_st_" +  referId;
                CacheHelper.Remove(cacheKey);
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@REFERID", referId);
                paramlist.Add("@SVNAME","");
                paramlist.Add("@URL", url);
                paramlist.Add("@ClickUrl", clickUrl);
                paramlist.Add("@AUTHOR", author);
                paramlist.Add("@SIZE", size);
                paramlist.Add("@desc", desc);
                return  base.ExecuteProc("sp_addImg_proc", paramlist);
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                ms.SendMessagesToRole("addmin", false, "Lỗi từ trang media fileHandler addNewsImg", ex.Message, "0");
                return 0;
                
            }
        
        
            //  Dal.DatabaseAccess acess = new Dal.DatabaseAccess();
            //return 0;
        }
        public List<Ads> GetImgOfReferal(string ReferalId)
        {
            List<Ads> rs = new List<Ads>();
            
            string cacheKey = "ads_st_" + ReferalId;
            try
            {
                if(!CacheHelper.TryGet(cacheKey,out rs))
                {
                    Dictionary<string, object> paramlist = new Dictionary<string, object>();
                    paramlist.Add("@referedId", ReferalId);                    
                    rs = base.Select("sp_getImg_proc",  paramlist);
                    CacheHelper.Set(cacheKey, rs, 60*24);
                }
            }
            catch (Exception ex)
            {
                Dal.MessengerControl massage = new Dal.MessengerControl();
                massage.SendMsgToAdmin("Error from function GetImgOfReferal: " + ex);
            }
            return rs;

        }
        public List<Ads> GetRandomImgOfReferal(string ReferalId, int numberRecord)
        {
            try
            {
                var rs= GetImgOfReferal(ReferalId).OrderBy(x => Guid.NewGuid()).ToList();
                if (rs.Count > numberRecord)
                {
                    rs = rs.Take(numberRecord).ToList();
                }
                return rs;
            }
            catch (Exception)
            {
                return null;
                
            }

        }
        public int DelPrThumb(string ReferalId,string imgId, string author)
        {
            try
            {
                
                string cacheKey = "ads_st_" + ReferalId;
                CacheHelper.Remove(cacheKey);
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@imgId", imgId);
                paramlist.Add("@author", author);
                return base.ExecuteProc("delPrThumb_proc", paramlist);

            }
            catch (Exception)
            {
                return 0;
               
            }

        }
        public int ResetSlide(string referalId, string author)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@referId", referalId);
                paramlist.Add("@author", author);
                return base.ExecuteProc("sp_ResetSlide", paramlist);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}