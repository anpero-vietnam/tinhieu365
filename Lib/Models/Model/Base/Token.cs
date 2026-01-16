using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TokenModel
    {
        public TokenModel()
        {
            TokenType = 1;
            UserId = 0;
            AppId= string.Empty;
            Token = string.Empty;
            AppSecrecKey = string.Empty;
            ExpiresInMinute = 0;
        }
        
        public int  TokenType { get; set; }
        public int UserId { get; set; }
        public string AppId { get; set; }
        public string Token { get; set; }
        public string AppSecrecKey { get; set; }
        public int ExpiresInMinute{ get; set; }
    }
}
