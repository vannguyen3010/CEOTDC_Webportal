using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
    public interface IS_OrganizationalComposition
    {
        Task<ResponseData<List<M_OrganizationalComposition>>> getListOrganizationalComposition();
    }
    public class S_OrganizationalComposition : IS_OrganizationalComposition
    {
        private readonly ICallBaseApi _callApi;
        public S_OrganizationalComposition(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_OrganizationalComposition>>> getListOrganizationalComposition()
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                
            };
            return await _callApi.GetResponseDataAsync<List<M_OrganizationalComposition>>("OrganizationalComposition/getListOrganizationalComposition", dictPars);
        }
        
        
    }
}
