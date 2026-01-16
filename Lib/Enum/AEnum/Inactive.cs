using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEnum
{
    public class Inactive
    {
        public enum SendingType
        {
            EMail=1,
            SMS=2
        }
        public enum TokenType
        {
            APIToken = 1,
            LoginToken=2
        }
    }
}
