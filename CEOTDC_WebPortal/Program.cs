using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Middlewares;
using CEOTDC_WebPortal.Models;
using CEOTDC_WebPortal.Services;
/*using CEOTDC_WebPortal.Mapper;*/
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

void GetDefaultHttpClient(IServiceProvider serviceProvider, HttpClient httpClient, string hostUri)
{
    if (!string.IsNullOrEmpty(hostUri))
        httpClient.BaseAddress = new Uri(hostUri);
    //client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
    httpClient.Timeout = TimeSpan.FromMinutes(1);
    httpClient.DefaultRequestHeaders.Clear();
    httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}

HttpClientHandler GetDefaultHttpClientHandler()
{
    return new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        UseCookies = false,
        AllowAutoRedirect = false,
        UseDefaultCredentials = true,
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true,
    };
}

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie = new CookieBuilder
    {
        //Domain = "cms.labadalat.com", //Releases in active
        Name = "AuthCMS",
        HttpOnly = true,
        Path = "/",
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.Always
    };
    options.LoginPath = new PathString("/Account/SignIn");
    options.LogoutPath = new PathString("/Account/SignOut");
    options.AccessDeniedPath = new PathString("/Error/403");
    options.SlidingExpiration = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSession(options =>
{
    //options.Cookie.Domain = ".koolselling.com"; //Releases in active
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

//builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly); //AutoMapperProfile
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient("base")
    .ConfigureHttpClient((serviceProvider, httpClient) => GetDefaultHttpClient(serviceProvider, httpClient, builder.Configuration.GetSection("ApiSettings:UrlApi").Value))
    .SetHandlerLifetime(TimeSpan.FromMinutes(5)) //Default is 2 min
    .ConfigurePrimaryHttpMessageHandler(x => GetDefaultHttpClientHandler());

builder.Services.AddHttpClient("custom")
    .ConfigureHttpClient((serviceProvider, httpClient) => GetDefaultHttpClient(serviceProvider, httpClient, string.Empty))
    .SetHandlerLifetime(TimeSpan.FromMinutes(5)) //Default is 2 min
    .ConfigurePrimaryHttpMessageHandler(x => GetDefaultHttpClientHandler());

builder.Services.AddSingleton<IBase_CallApi, Base_CallApi>();
builder.Services.AddSingleton<ICallBaseApi, CallBaseApi>();
builder.Services.AddSingleton<ICallApi, CallApi>();

builder.Services.AddSingleton<IS_Address, S_Address>();
builder.Services.AddSingleton<IS_Contact, S_Contact>();
builder.Services.AddSingleton<IS_Document, S_Document>();
builder.Services.AddSingleton<IS_DocumentFile, S_DocumentFile>();
builder.Services.AddSingleton<IS_DocumentSubject, S_DocumentSubject>();
builder.Services.AddSingleton<IS_Supplier, S_Supplier>();
builder.Services.AddSingleton<IS_Major, S_Major>();
builder.Services.AddSingleton<IS_News, S_News>();
builder.Services.AddSingleton<IS_NewsCategory, S_NewsCategory>();
builder.Services.AddSingleton<IS_OrganizationalComposition, S_OrganizationalComposition>();
builder.Services.AddSingleton<IS_PersonOrgComposition, S_PersonOrgComposition>();
builder.Services.AddSingleton<IS_PartnerList, S_PartnerList>();
builder.Services.AddSingleton<IS_Person, S_Person>();
builder.Services.AddSingleton<IS_SupplierMajor, S_SupplierMajor>();

builder.Services.Configure<Config_ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<Config_MetaSEO>(builder.Configuration.GetSection("MetaSEO"));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseHsts();
}

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 7 * 60 * 60 * 24; //7 days
        ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
    }
});

app.UseMiddleware<SecurityHeadersMiddleware>(); //App config security header

app.UseCookiePolicy(); ;

app.UseSession();

app.UseRouting();

/*app.UseAuthorization();*/

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Introduce IsHot",
        pattern: "gioi-thieu",
        defaults: new { controller = "Introduce", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Introduce",
        pattern: "so-do-to-chuc",
        defaults: new { controller = "Introduce", action = "Organizational" });

    endpoints.MapControllerRoute(
        name: "Introduce Detail",
        pattern: "gioi-thieu/{titleSlug}-{id}",
        defaults: new { controller = "Introduce", action = "Detail" });

    endpoints.MapControllerRoute(
        name: "Introduce",
        pattern: "gioi-thieu",
        defaults: new { controller = "Introduce", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Library",
        pattern: "thu-vien",
        defaults: new { controller = "Library", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Library Detail",
        pattern: "thu-vien/{titleSlug}-{id}",
        defaults: new { controller = "Library", action = "Detail" });

    endpoints.MapControllerRoute(
        name: "Contact",
        pattern: "lien-he",
        defaults: new { controller = "Contact", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Activity",
        pattern: "hoat-dong",
        defaults: new { controller = "Activity", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Activity",
        pattern: "hoat-dong/{titleSlug}-{id}",
        defaults: new { controller = "Activity", action = "Index" });

    endpoints.MapControllerRoute(
        name: "News",
        pattern: "tin-tuc",
        defaults: new { controller = "News", action = "Index" });

    endpoints.MapControllerRoute(
        name: "News Detail",
        pattern: "tin-tuc/{titleSlug}-{id}",
        defaults: new { controller = "News", action = "Detail" });

    endpoints.MapControllerRoute(
        name: "Member",
        pattern: "hoi-vien",
        defaults: new { controller = "Member", action = "Index" });

/*
      endpoints.MapControllerRoute(
        name: "Member",
        pattern: "hoi-vien/{c=-2}",
        defaults: new { controller = "Member", action = "Index" });
*/
    endpoints.MapControllerRoute(
        name: "Member Detail",
        pattern: "hoi-vien/{titleSlug}-{id}",
        defaults: new { controller = "Member", action = "Detail" });

    endpoints.MapControllerRoute(
        name: "Service",
        pattern: "dich-vu",
        defaults: new { controller = "Service", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Service Detail",
        pattern: "dich-vu/{titleSlug}-{id}",
        defaults: new { controller = "Service", action = "Detail" });

    endpoints.MapControllerRoute(
       name: "Error page",
       pattern: "error/{code}",
       defaults: new { controller = "Error", action = "Index" });

    endpoints.MapControllerRoute(
       name: "No Data Page",
       pattern: "no-data",
       defaults: new { controller = "NoData", action = "Index" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
