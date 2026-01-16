using Models;
using System;
using System.Collections.Generic;

using AModul.Dapper;
using Ultil.Cache;
using Models.Modul.Common;
/// <summary>
/// when all store web change to new Modul version will change db to theme DB and move this class to Theme Modul
/// </summary>
namespace AModul.Common
{
    public class MenuControl : ConnectionProxy<Menu>
    {  
        public List<Menu> GetMenu(int parentId, bool getChidlValue, string position = "master")

        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                
                paramlist.Add("@parentId", parentId);
                paramlist.Add("@position", position);
                var menuList = base.Select("sp_getMenu",  paramlist);

                if (getChidlValue && menuList != null && menuList.Count > 0)
                {
                    foreach (var item in menuList)
                    {
                        item.ChidMenu = GetMenu( item.Id, false, position);
                    }
                }
                return menuList == null ? new List<Menu>() : menuList;
            }
            catch (Exception ex)
            {
                var x = ex.StackTrace;
                return new List<Menu>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="link"></param>
        /// <param name="possition">default('master')</param>
        /// <param name="prioty"></param>
        /// <returns></returns>
        public int SetMenu(string title, string link, string possition, string prioty, int parent)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@title", title);
                paramlist.Add("@link", link);
                paramlist.Add("@possition", possition);
                paramlist.Add("@prioti", prioty);
                paramlist.Add("@parent", parent);
                
                int i = base.ExecuteProc("addMenu", paramlist);
                
                string cacheKey = AEnum.Cache.GetCommonInfoKey();
                Ultil.Cache.CacheHelper.Remove(cacheKey);
                return i;
            }
            catch (Exception)
            {
                throw;
                //return 0;
            }


        }
        //updated
        public int UpdateImgReferer(string linkClick, string desc, int id, string prioty,  string imagesLink, string referID)
        {
            try
            {
                string cacheKey = "ads_st_" +  referID;

                CacheHelper.Remove(cacheKey);
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@id", id);
                paramlist.Add("@linkClick", linkClick);
                paramlist.Add("@desc", desc);                
                paramlist.Add("@prioty", prioty);
                paramlist.Add("@imageslink", imagesLink);
                return base.ExecuteProc("UpdateImg", paramlist);
            }
            catch
            {
                return 0;
            }
        }

        public int UpdateMenu(String title, String link, string prioty, int id, int parent)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                int i = 0;
                paramlist.Add("@title", title);
                paramlist.Add("@link", link);
                
                paramlist.Add("@prioti", prioty);
                paramlist.Add("@Id", id);
                paramlist.Add("@parent", parent);
                i = base.ExecuteProc("UpdateMenu", paramlist);
                string cacheKey = AEnum.Cache.GetCommonInfoKey();
                Ultil.Cache.CacheHelper.Remove(cacheKey);
                return i;
            }
            catch (Exception)
            {
                throw;
                //return 0;
            }


        }

        public int DelMenu(string id)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@Id", id);
                  
                string cacheKey = AEnum.Cache.GetCommonInfoKey();
                Ultil.Cache.CacheHelper.Remove(cacheKey);
                return base.ExecuteProc("delMenu",  paramlist); 

            }
            catch (Exception ex)
            {

                return 0;
            }


        }

    }
}

