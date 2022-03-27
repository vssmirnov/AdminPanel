using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TestTask.Data;
using TestTask.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// dotnet user-secret set "ApiKey" ""
var apiKey = builder.Configuration["ApiKey"];
var baseUrl = builder.Configuration["Baseurl"];

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddOptions();
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<BloggerService>(x => new BloggerService(apiKey, baseUrl));
builder.Services.AddTransient<PostService>(x => new PostService(apiKey, baseUrl));
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

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
