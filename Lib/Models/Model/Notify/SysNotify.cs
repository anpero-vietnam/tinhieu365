using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Notify
{
   public class SysNotify
    {
        
        public int Id { get;set; }
        public int St { get; set; }
        public DateTime NotifyTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserLock { get; set; }
        public string UserLockName { get; set; }
        public string NotifyTimeText { get; set; }
        public bool IsLocked { get; set; }
        public SysNotify()
        {
            Id = 0;
            Content = string.Empty;
            NotifyTime = DateTime.Now;
            St = 0;
            Title = string.Empty;
            UserLockName = string.Empty;

        }
    }

}


