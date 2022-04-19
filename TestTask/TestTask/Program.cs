using TestTask.Interfaces;
using TestTask.Providers;
using TestTask.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// dotnet user-secret set "ApiKey" ""
var apiKey = builder.Configuration["ApiKey"];
var baseUrl = builder.Configuration["Baseurl"];

var httpClientHandler = new HttpClientHandler()
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
};

// Add services to the container.
builder.Services.AddHttpClient("ignoreSSL", c => {}).ConfigurePrimaryHttpMessageHandler(h => httpClientHandler);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddOptions();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IBloggerWebCacheService, BloggerWebCacheService>();
builder.Services.AddTransient<IPostWebCacheService, PostWebCacheService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

var httpClientFactory = builder.Services.BuildServiceProvider().GetService<IHttpClientFactory>();

builder.Services.AddTransient<IBloggerService>(x => new BloggerService(apiKey, baseUrl, httpClientFactory));
builder.Services.AddTransient<IPostService>(x => new PostService(apiKey, baseUrl, httpClientFactory));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
