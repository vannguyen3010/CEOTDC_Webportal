using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Services
{
    public interface IS_News
    {
        Task<ResponseData<M_NewsHome>> getListNewsBySupplierId(int supplierId, int recordNumberIntroduce = 5, int recordNumberNews = 10, int recordNumberActivity = 10);
        Task<ResponseData<List<M_News>>> getListNewsTypeIdPagination(int typeId, int? newsCategoryId, int page, int recordNumber);
        Task<ResponseData<T>> getListNewsByTypeId<T>(int typeId, int newsCategoryId, int page, int recordNumber);
        Task<ResponseData<M_News>> getNewsByIdWithListRelated(int id, int typeId, int recordNumber);
        Task<ResponseData<List<M_News>>> getListLatestNews(int typeId, int recordNumber);
        Task<ResponseData<T>> getNewsById<T>(int id);
        Task<ResponseData<T>> getIntroduceByIdIsHotWithListRelated<T>(int? id, int isHot, int recordNumberRef);
        Task<ResponseData<M_News>> getActivityByIdIsHotWithListRelated(int? id, int isHot, int recordNumberRef);
    }
    public class S_News : IS_News
    {
        private readonly ICallBaseApi _callApi;
        public S_News(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<M_NewsHome>> getListNewsBySupplierId(int supplierId, int recordNumberIntroduce = 5, int recordNumberNews = 10, int recordNumberActivity = 10)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", supplierId},
                {"recordNumberIntroduce", recordNumberIntroduce},
                {"recordNumberNews", recordNumberNews},
                {"recordNumberActivity", recordNumberActivity},
            };
            return await _callApi.GetResponseDataAsync<M_NewsHome>("News/getListNewsBySupplierId", dictPars);
        }

        public async Task<ResponseData<List<M_News>>> getListNewsTypeIdPagination(int typeId, int? newsCategoryId, int page, int recordNumber)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"typeId", typeId},
                {"newsCategoryId", newsCategoryId},
                {"page", page},
                {"recordNumber", recordNumber},
            };
            return await _callApi.GetResponseDataAsync<List<M_News>>("News/getListNewsTypeIdPagination", dictPars);
        }

        public async Task<ResponseData<T>> getListNewsByTypeId<T>(int typeId, int newsCategoryId, int page, int recordNumber)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"typeId", typeId},
                {"newsCategoryId", newsCategoryId},
                {"page", page},
                {"recordNumber", recordNumber},
            };
            return await _callApi.GetResponseDataAsync<T>("News/getListNewsTypeIdPagination", dictPars);
        }

        public async Task<ResponseData<M_News>> getNewsByIdWithListRelated(int id, int typeId, int recordNumber)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"typeId", typeId},
                {"recordNumber", recordNumber},
            };
            return await _callApi.GetResponseDataAsync<M_News>("News/getNewsByIdWithListRelated", dictPars);
        }

        public async Task<ResponseData<T>> getNewsById<T>(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<T>("News/getNewsById", dictPars);
        }

        public async Task<ResponseData<List<M_News>>> getListLatestNews(int typeId, int recordNumber)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"typeId", typeId},
                {"recordNumber", recordNumber},
            };
            return await _callApi.GetResponseDataAsync<List<M_News>>("News/getListLatestNews", dictPars);
        }

        public async Task<ResponseData<T>> getIntroduceByIdIsHotWithListRelated<T>(int? id, int isHot, int recordNumberRef)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"isHot", isHot},
                {"recordNumberRef", recordNumberRef},
            };
            return await _callApi.GetResponseDataAsync<T>("News/getIntroduceByIdIsHotWithListRelated", dictPars);
        }
        public async Task<ResponseData<M_News>> getActivityByIdIsHotWithListRelated(int? id, int isHot, int recordNumberRef)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"isHot", isHot},
                {"recordNumberRef", recordNumberRef},
            };
            return await _callApi.GetResponseDataAsync<M_News>("News/getActivityByIdIsHotWithListRelated", dictPars);
        }
    }
}
