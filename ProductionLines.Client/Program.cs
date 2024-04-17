using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductionLines.Client;
using ProductionLines.Client.Data;
using ProductionLines.Client.Interfaces;
using ProductionLines.Client.Layout;
using ProductionLines.Client.Services;
using Radzen;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddScoped(sp =>
//{
//    var client = new HttpClient();
//    client.BaseAddress = new Uri(HttpConstants.BaseAddress);
//    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic","YWRtaW46UEBzc3cwcmQ=");
//    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//    return client;
//});

builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredModal();
builder.Services.AddSingleton<AppState>();
builder.Services.AddScoped<IBCData,BCDataService>();

await builder.Build().RunAsync();
