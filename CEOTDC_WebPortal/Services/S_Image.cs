using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static System.String;

namespace LABA_WebAdmin_Corporate.Services
{
    public interface IS_Image
    {
        Task<ResponseData<T>> UploadImageBase64<T>(IFormFile imgFile, string farmId, string hostUri, string createdBy);
        Task<ResponseData<T>> Upload<T>(IFormFile imgFile, string farmId, string hostUri, string createdBy);
        Task<ResponseData<T>> UploadListImageIFormFile<T>(List<IFormFile> imgFiles, string farmId, string hostUri, string description, string createdBy);
        Task<ResponseData<T>> UploadResize<T>(IFormFile imgFile, string refId, string hostUri, string createdBy);
        Task<ResponseData<T>> UploadListImageResize<T>(List<IFormFile> imgFiles, string refId, string hostUri, string createdBy);

        //File upload
        Task<ResponseData<T>> UploadFile<T>(IFormFile imgFile, string refId, string hostUri, string createdBy);
        Task<ResponseData<T>> UploadListIFormFile<T>(List<IFormFile> imgFiles, string refId, string hostUri, string createdBy);
    }
    public class S_Image : IS_Image
    {
        private readonly ICallApi _callApi;
        private readonly IOptions<Config_ApiSettings> _apiSettings;
        public S_Image(ICallApi callApi, IOptions<Config_ApiSettings> apiSettings)
        {
            _callApi = callApi;
            _apiSettings = apiSettings;
        }

        public async Task<ResponseData<T>> UploadImageBase64<T>(IFormFile imgFile, string farmId, string hostUri, string createdBy)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(IsNullOrEmpty(farmId) ? "0" : farmId), "farmId");
            formData.Add(new StringContent(ImageHelper.EncodeToBase64String(imgFile)), "imgBase64");
            formData.Add(new StringContent(IsNullOrEmpty(IsNullOrEmpty(hostUri) ? _apiSettings.Value.UrlImageHost : hostUri) ? _apiSettings.Value.UrlImageHost : hostUri), "hostUri");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "createdBy");
            return await _callApi.PostResponseDataAsync<T>(_apiSettings.Value.UrlApiImage + "Image/UploadImage", formData);
        }
        public async Task<ResponseData<T>> Upload<T>(IFormFile imgFile, string farmId, string hostUri, string createdBy)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(IsNullOrEmpty(farmId) ? "0" : farmId), "farmId");
            formData.Add(ImageHelper.EncodeToStreamContent(imgFile), "imgFile", imgFile.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(hostUri) ? _apiSettings.Value.UrlImageHost : hostUri), "hostUri");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "createdBy");
            return await _callApi.PostResponseDataAsync<T>(_apiSettings.Value.UrlApiImage + "Image/Upload", formData);
        }
        public async Task<ResponseData<T>> UploadListImageIFormFile<T>(List<IFormFile> imgFiles, string farmId, string hostUri, string description, string createdBy)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            foreach (var item in imgFiles)
                formData.Add(ImageHelper.EncodeToStreamContent(item), "imgFiles", item.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(farmId) ? "0" : farmId), "farmId");
            formData.Add(new StringContent(IsNullOrEmpty(hostUri) ? _apiSettings.Value.UrlImageHost : hostUri), "hostUri");
            formData.Add(new StringContent(IsNullOrEmpty(description) ? "" : description), "description");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "createdBy");
            return await _callApi.PostResponseDataAsync<T>(_apiSettings.Value.UrlApiImage + "Image/UploadListImageIFormFile", formData);
        }
        public async Task<ResponseData<T>> UploadResize<T>(IFormFile imgFile, string refId, string hostUri, string createdBy)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(ImageHelper.EncodeToStreamContent(imgFile), "imgFile", imgFile.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "refId");
            formData.Add(new StringContent(IsNullOrEmpty(hostUri) ? _apiSettings.Value.UrlImageHost : hostUri), "hostUri");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "createdBy");
            return await _callApi.PostResponseDataAsync<T>(_apiSettings.Value.UrlApiImage + "Image/UploadResize", formData);
        }
        public async Task<ResponseData<T>> UploadListImageResize<T>(List<IFormFile> imgFiles, string refId, string hostUri, string createdBy)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            foreach (var item in imgFiles)
                formData.Add(ImageHelper.EncodeToStreamContent(item), "imgFiles", item.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "refId");
            formData.Add(new StringContent(IsNullOrEmpty(hostUri) ? _apiSettings.Value.UrlImageHost : hostUri), "hostUri");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "createdBy");
            return await _callApi.PostResponseDataAsync<T>(_apiSettings.Value.UrlApiImage + "Image/UploadListImageResize", formData);
        }

        //File upload
        public async Task<ResponseData<T>> UploadFile<T>(IFormFile imgFile, string refId, string hostUri, string createdBy)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(ImageHelper.EncodeToStreamContent(imgFile), "imgFile", imgFile.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "refId");
            formData.Add(new StringContent(IsNullOrEmpty(hostUri) ? _apiSettings.Value.UrlImageHost : hostUri), "hostUri");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "createdBy");
            return await _callApi.PostResponseDataAsync<T>(_apiSettings.Value.UrlApiImage + "File/UploadFile", formData);
        }
        public async Task<ResponseData<T>> UploadListIFormFile<T>(List<IFormFile> imgFiles, string refId, string hostUri, string createdBy)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            foreach (var item in imgFiles)
                formData.Add(ImageHelper.EncodeToStreamContent(item), "imgFiles", item.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "refId");
            formData.Add(new StringContent(IsNullOrEmpty(hostUri) ? _apiSettings.Value.UrlImageHost : hostUri), "hostUri");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "createdBy");
            return await _callApi.PostResponseDataAsync<T>(_apiSettings.Value.UrlApiImage + "File/UploadListIFormFile", formData);
        }
    }   
}