using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
    public interface IS_DocumentFile
    {
        Task<ResponseData<List<M_DocumentFile>>> getListDocumentFile();
    }
    public class S_DocumentFile : IS_DocumentFile
    {
        private readonly ICallBaseApi _callApi;
        public S_DocumentFile(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_DocumentFile>>> getListDocumentFile()
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {

            };
            return await _callApi.GetResponseDataAsync<List<M_DocumentFile>>("DocumentFile/getListDocumentFile", dictPars);
        }
    }
}
