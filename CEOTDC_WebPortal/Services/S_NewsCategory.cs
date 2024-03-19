using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
    public interface IS_NewsCategory
    {
        Task<ResponseData<T>> getListNewsCategory<T>();
        Task<ResponseData<M_NewsCategory>> getNewsCategoryById(int id);
    }
    public class S_NewsCategory : IS_NewsCategory
    {
        private readonly ICallBaseApi _callApi;
        public S_NewsCategory(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<T>> getListNewsCategory<T>()
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {

            };
            return await _callApi.GetResponseDataAsync<T>("NewsCategory/getListNewsCategory", dictPars);
        }
        public async Task<ResponseData<M_NewsCategory>> getNewsCategoryById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_NewsCategory>("NewsCategory/getNewsCategoryById", dictPars);
        }
    }
}
