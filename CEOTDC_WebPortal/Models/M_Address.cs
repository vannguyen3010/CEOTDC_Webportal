using System;
using System.ComponentModel.DataAnnotations;

namespace CEOTDC_WebPortal.Models
{
	public class M_Address : M_BaseModel.AddressCustom
	{
	}
	public class M_Country : M_BaseModel.CountryCustom
	{
	}
	public class M_Province : M_BaseModel.ProvinceCustom
	{
	}
	public class M_District : M_BaseModel.DistrictCustom
	{
	}
	public class M_Ward : M_BaseModel.WardCustom
	{
	}
	public class EM_Address : M_BaseModel.BaseCustom
	{
		public int? id { get; set; }
		public string title { get; set; }
		[StringLength(100, ErrorMessage = "Số nhà có độ dài tối đa 100 ký tự")]
		public string addressNumber { get; set; }
		[StringLength(100, ErrorMessage = "Địa chỉ có độ dài tối đa 100 ký tự")]
		public string addressText { get; set; }
		public int? countryId { get; set; }
		public int? provinceId { get; set; }
		public int? districtId { get; set; }
		public int? wardId { get; set; }
		public double? latitude { get; set; }
		public double? longitude { get; set; }
	}
}