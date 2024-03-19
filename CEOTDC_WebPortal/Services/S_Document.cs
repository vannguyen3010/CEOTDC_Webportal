using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
    public interface IS_Document
    {
        Task<ResponseData<List<M_Document>>> getListDocumentByDocumentSubjectId(int? documentSubjectId, int recordNumber, int page);
        Task<ResponseData<T>> getDocumentById<T>(int id);
    }
    public class S_Document : IS_Document
    {
        private readonly ICallBaseApi _callApi;
        public S_Document(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_Document>>> getListDocumentByDocumentSubjectId(int? documentSubjectId, int recordNumber, int page)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                   {"documentSubjectId", documentSubjectId},
                   {"recordNumber", recordNumber},
                   {"page", page},
            };
            return await _callApi.GetResponseDataAsync<List<M_Document>>("Document/getListDocumentByDocumentSubjectId", dictPars);
        }
        public async Task<ResponseData<T>> getDocumentById<T>(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                   {"id", id},
            };
            return await _callApi.GetResponseDataAsync<T>("Document/getDocumentById", dictPars);
        }
    }
}
