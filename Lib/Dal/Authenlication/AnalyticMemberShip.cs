using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Authenlication
{
    public class AnalyticMemberShip
    {
        public int AllUserCount { get; set; }
        public int AllBlockUserUsercount { get; set; }
        public int AllOauthUserCount { get; set; }
        public void SetUpAnalyticMemberShip()
        {
            try
            {
                DatabaseAccess dal = new DatabaseAccess();
                DataTable table = dal.executeSelect("analyticUser");
                AllUserCount = Convert.ToInt32(table.Rows[0]["allUser"]);
                AllBlockUserUsercount = Convert.ToInt32(table.Rows[0]["allBlockUser"]);
                AllOauthUserCount = Convert.ToInt32(table.Rows[0]["OauthCount"]);
            }
            catch (Exception ex)
            {
                
            }

        }
    }
}
