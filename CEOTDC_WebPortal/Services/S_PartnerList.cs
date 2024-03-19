using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;

namespace CEOTDC_WebPortal.Services
{
	public interface IS_PartnerList
	{
		Task<ResponseData<List<M_PartnerList>>> getListPartnerList();
	}
	public class S_PartnerList : IS_PartnerList
	{
		private readonly ICallBaseApi _callApi;
		public S_PartnerList(ICallBaseApi callApi)
		{
			_callApi = callApi;
		}

		public async Task<ResponseData<List<M_PartnerList>>> getListPartnerList()
		{
			return await _callApi.GetResponseDataAsync<List<M_PartnerList>>("PartnerList/getListPartnerList", default(Dictionary<string, dynamic>));
		}
	}
}
