using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SearchByLucene.Service.Interfaces;
using SearchByLucene.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


#region Service Dependence Injection

builder.Services.AddScoped<ISearchEngineMyDataService, SearchEngineMyDataService>();
builder.Services.AddScoped<ISearchEngineMCSmallDataService, SearchEngineMCSmallDataService>();
builder.Services.AddScoped<ISearchEngine1MFakeService, SearchEngine1MFakeService>();

#endregion

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
