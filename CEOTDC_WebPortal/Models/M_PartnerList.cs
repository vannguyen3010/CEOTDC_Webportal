using System;
using System.Collections.Generic;


namespace CEOTDC_WebPortal.Models
{
    public partial class M_PartnerList
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public string name { get; set; }
        public string Title { get; set; }
        public string DescShort { get; set; }
        public string Url { get; set; }
        public string smallUrl { get; set; }
        public int? ImageId { get; set; }
        public int? ClickNumber { get; set; }
        public int? Status { get; set; }
        public string PageName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime Timer { get; set; }
        public M_PartnerList imageObj { get; set; }
    }

}
