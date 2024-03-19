using CEOTDC_WebPortal.Lib;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
	public interface IS_Supplier
	{
		Task<ResponseData<T>> getSupplierById<T>(int id);
	}
	public class S_Supplier : IS_Supplier
	{
		private readonly ICallBaseApi _callApi;
		public S_Supplier(ICallBaseApi callApi)
		{
			_callApi = callApi;
		}

		public async Task<ResponseData<T>> getSupplierById<T>(int id)
		{
			Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
			{
				{"id", id},
			};
			return await _callApi.GetResponseDataAsync<T>("Supplier/getSupplierById", dictPars);
		}
	}
}
