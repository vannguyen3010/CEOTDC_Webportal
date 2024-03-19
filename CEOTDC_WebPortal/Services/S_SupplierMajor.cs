using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
    public interface IS_SupplierMajor
    {
        Task<ResponseData<M_SupplierMajorDetail>> getSupplierMajorById(int id);
        Task<ResponseData<List<M_SupplierMajor>>> getListSupplierMajorBySequenceStatus(int? majorId, string sequenceStatus, int page, int recordNumber);
    }
    public class S_SupplierMajor : IS_SupplierMajor
    {
        private readonly ICallBaseApi _callApi;
        public S_SupplierMajor(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<M_SupplierMajorDetail>> getSupplierMajorById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id}
            };
            return await _callApi.GetResponseDataAsync<M_SupplierMajorDetail>("SupplierMajor/getSupplierMajorById", dictPars);
        }

        public async Task<ResponseData<List<M_SupplierMajor>>> getListSupplierMajorBySequenceStatus(int? majorId, string sequenceStatus, int page, int recordNumber)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
               {"sequenceStatus", sequenceStatus},
               {"majorId", majorId},
               {"page", page},
               {"recordNumber", recordNumber}
            };
            return await _callApi.GetResponseDataAsync<List<M_SupplierMajor>>("SupplierMajor/getListSupplierMajorBySequenceStatus", dictPars);
        }
    }
}
