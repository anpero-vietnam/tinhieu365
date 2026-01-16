using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Notify
{
    public class UserMessenge
    {
        
        public DateTime? ReadDate { get; set; }
        public DateTime? SenderDate { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string ReveiceName { get; set; }
        public string SenderName { get; set; }
        public int To { get; set; }
        public Guid Id { get; set; }
        public int From { get; set; }
        public bool IsReader { get; set; }

        public UserMessenge()
        {
            Id = new Guid();
            From = 0;
            To = 0;
            Title = string.Empty;
            Content = string.Empty;
            ReveiceName = string.Empty;
            SenderName = string.Empty;
            IsReader = false;
        }
    }
}
