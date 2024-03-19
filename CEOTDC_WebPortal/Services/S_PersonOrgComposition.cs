using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
    public interface IS_PersonOrgComposition
    {
        Task<ResponseData<List<M_PersonOrgComposition>>> getListPersonOrgComposition();
    }
    public class S_PersonOrgComposition : IS_PersonOrgComposition
    {
        private readonly ICallBaseApi _callApi;
        public S_PersonOrgComposition(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_PersonOrgComposition>>> getListPersonOrgComposition()
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                
            };
            return await _callApi.GetResponseDataAsync<List<M_PersonOrgComposition>>("PersonOrgComposition/getListPersonOrgComposition", dictPars);
        }
    }
}
