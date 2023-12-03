using CloudLearn.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
	
builder.Services.AddSyncfusionBlazor();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjk1NTQxOUAzMjMzMmUzMDJlMzBuMHRCamxwZE9Ra1RxTVEwMHZkVXY5SkVIQzd2RkdPbnRxWDk2MGhway9rPQ==");

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

await builder.Build().RunAsync();
