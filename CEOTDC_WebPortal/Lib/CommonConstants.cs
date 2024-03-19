namespace CEOTDC_WebPortal.Lib
{
    public static class CommonConstants
    {
        public const string DIRECTORY_IMAGE_SRC = "https://image.koolselling.com";
        public const int TIMEOUT_CHECK_AUTHENTICATION = 5; //Minute
        public const int MAX_FILE_SIZE_IMAGE_UPLOAD = 10; //MB
        public const int BANNER_RECORDS = 10; //Banner mid, bot
        public const int DISCOUNT_PRODUCT_RECORDS = 20; //Sp đang khuyến mãi
        public const int SELLING_PRODUCT_RECORDS = 20; //Sp bán chạy (nổi bật)
        public const int POPULAR_PRODUCT_RECORDS = 20; //Sp phổ biến (xem nhiều)
        public const int NEWS_LATEST_RECORDS = 5; //Tin tức mới
        public const int NEWS_RELATED_RECORDS = 5; //Tin tức liên quan
        public const int OWNER_SUPPLIER_ID = 1;
        public const string CACHE_KEY_SUPPLIER = "supplier";
        public const string CACHE_KEY_NEWS_CATEGORY = "newscategory";
        public const string CACHE_KEY_MEMBER_CATERGORY = "memberCategory";
        public const string CACHE_KEY_LIBRARY_CATEGORY = "libraryCategory";
        public const string SEQUENCESTATUS = "1";
    }
}
