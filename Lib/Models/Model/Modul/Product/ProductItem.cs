using Models.Modul.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public partial  class ProductItem
    {
        public ProductItem()
        {
            ImagesSlide = new List<Ads>();
            Atribute = new List<AtributeModel>();
            ImagesSlide2 = new List<Ads>();
            Property = new List<AtributeValue>();
            ViewTime = 0;
            CreateBy = 0;
            Rank = 0;
        }
        public DateTime? EndAdsDay { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public int CreateBy { get; set; }
        public List<AtributeValue> Property { get; set; }
        public List<AtributeModel> Atribute { get; set; }
        public string Title { get; set; }
        public int? ProjectId { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
        public int? ParentCatId { get; set; }
        public decimal? Price { get; set; }
        public string Unit { get; set; }
        public string UpdateTime { get; set; }
        public string ProjectName { get; set; }
        public string Images { get; set; }
        public string Detail { get; set; }
        public string ParentCatName { get; set; }        
        public int Id { get; set; }
        public bool IsPublish { get; set; }
        public int Rank { get; set; }
        public string ShortDesc { get; set; }        
        public string Script { get; set; }
        public string KeyWord { get; set; }        
        public List<Ads> ImagesSlide2 { get; set; }
        public List<Ads> ImagesSlide { get; set; }
        public string Tag { get; set; }
        public string TagLink { get; set; }
        public string Specifications { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? ClosingDate   { get; set; }
        public decimal? LaneWidth { get; set; }
        public decimal? FacadeLength { get; set; }
        public int? Orientation { get; set; }
        public int? BalconyOrientation { get; set; }
        public int? Floors { get; set; }
        public int? BedRoom { get; set; }
        public int? BathRoom { get; set; }
        public string Address { get; set; }
        public string Keywords { get; set; }        
        public decimal? Area { get; set; }
        public int? DistrictId { get; set; }
        public int? ProvinceId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int ViewTime { get; set; }
    }
}
