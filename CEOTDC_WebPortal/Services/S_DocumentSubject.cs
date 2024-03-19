using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
    public interface IS_DocumentSubject
    {
        Task<ResponseData<T>> getListDocumentSubject<T>();
        Task<ResponseData<M_DocumentSubject>> getDocumentSubjectById(int id);
    }
    public class S_DocumentSubject : IS_DocumentSubject
    {
        private readonly ICallBaseApi _callApi;
        public S_DocumentSubject(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<T>> getListDocumentSubject<T>()
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {

            };
            return await _callApi.GetResponseDataAsync<T>("DocumentSubject/getListDocumentSubject", dictPars);
        }

        public async Task<ResponseData<M_DocumentSubject>> getDocumentSubjectById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                   {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_DocumentSubject>("DocumentSubject/getDocumentSubjectById", dictPars);
        }

    }
}
