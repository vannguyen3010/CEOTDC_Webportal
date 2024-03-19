using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
    public interface IS_Major
    {
        Task<ResponseData<List<M_Major>>> getListMajorDropDown(string sequenceStatus);

    }

    public class S_Major : IS_Major
    {
        private readonly ICallBaseApi _callApi;
        public S_Major(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_Major>>> getListMajorDropDown(string sequenceStatus)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
            };
            return await _callApi.GetResponseDataAsync<List<M_Major>>("Major/getListMajorDropDown", dictPars);
        }
    }
}
