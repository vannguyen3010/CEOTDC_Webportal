using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
	public interface IS_Person
	{
		Task<ResponseData<M_Person>> getPersonById(int id);
	}
	public class S_Person : IS_Person
    {
		private readonly ICallBaseApi _callApi;
		public S_Person(ICallBaseApi callApi)
		{
			_callApi = callApi;
		}
		public async Task<ResponseData<M_Person>> getPersonById(int id)
		{
			Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
			{
				{"id", id},
			};
			return await _callApi.GetResponseDataAsync<M_Person>("Person/getPersonById", dictPars);
		}
		
	}
}
