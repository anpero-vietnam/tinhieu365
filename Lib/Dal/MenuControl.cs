using Dal.Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class MenuControl : ConnectionProxy<Models.Menu>
    {

       
        public List<Menu> GetMenu(AnperoClient client, int parentId, bool getChidlValue, string position = "master")

        {
            try
            {
                base.ServerName = client.ServerName;                
                base.ParamList.Add("@st", client.AgenId);
                base.ParamList.Add("@parentId", parentId);
                base.ParamList.Add("@position", position);
                var menuList = base.Select("sp_getMenu");

                if (getChidlValue && menuList != null && menuList.Count > 0)
                {
                    foreach (var item in menuList)
                    {
                        item.ChidMenu = GetMenu(client, item.Id, false, position);
                    }
                }
                return menuList == null ? new List<Menu>() : menuList;
            }
            catch (Exception)
            {
                return new List<Menu>();
            }
        }

    }
}
