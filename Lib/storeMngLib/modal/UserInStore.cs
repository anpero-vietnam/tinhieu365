using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMng.modal
{
    public class UserInStore
    {

        int id;
        string lastVisit;
        string name, sureName, allRole, allBranch;

        public string AllBranch
        {
            get
            {
                return allBranch;
            }
            set
            {
                allBranch = value;
            }
        }

        public string AllRole
        {
            get
            {
                return allRole;
            }

            set
            {
                allRole = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string LastVisit
        {
            get
            {
                return lastVisit;
            }

            set
            {
                lastVisit = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string SureName
        {
            get
            {
                return sureName;
            }

            set
            {
                sureName = value;
            }
        }
    }
}
