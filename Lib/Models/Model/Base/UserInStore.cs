using Models.Modul.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserProfile
    {
        private string _sureName { get; set; }
        private string _Avata { get; set; }
        public string WebSite { get; set; }
        public string AllRole { get; set; }
        public int UserId { get; set; }
        public DateTime? LastVisit { get; set; }
        public string UserName { get; set; }
        public string IntroduceYourself { get; set; }
        public string IntroduceBusiness { get; set; }
        public string BusinessName { get; set; }
        public string Email { get; set; }
        public string SureName
        {
            get
            {
                if (string.IsNullOrEmpty(_sureName))
                {
                    return UserName;
                }
                else
                {
                    return _sureName;
                }
            }
            set { _sureName = value; }
        }
        public string Avata
        {
            get
            {
                if (string.IsNullOrEmpty(_Avata))
                {
                    return "https://media.whereviet.com/images/1/112020/12020112717042683.png";
                }
                else
                {
                    return _Avata;
                }
            }
            set { _Avata = value; }
        }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string IdentityCardNumber { get; set; }
        public int? ProvinceId { get; set; }
        public int? LocationId { get; set; }
        public bool IsAuthenlication { get; set; }
        public string locationJson { get; set; }
        public double Credit { get; set; }
        public Models.Modul.Common.Location Location
        {
            get
            {
                if (!string.IsNullOrEmpty(locationJson))
                {
                    List<Location> LocationList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Location>>(locationJson);
                    Models.Modul.Common.Location location = LocationList.Where(x => x.ParentId == 0).FirstOrDefault();
                    if (location != null && location.Id > 0)
                    {
                        //location.ChildLocation = new List<Modul.Common.Location>();
                        location.ChildLocation=LocationList.Where(x2 => x2.ParentId > 0).ToList();
                    }
                    return location;
                }
                else { return null; }
            }
        }

        public UserProfile()
        {
            IsAuthenlication = false;
        }
    }
}
