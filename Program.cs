using BlazorDryIoC;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

//create DryIoC container
var container = new Container(rules => rules.WithTrackingDisposableTransients());

builder.ConfigureContainer(new DryIocServiceProviderFactory(container), adaptedContainer =>
{
    adaptedContainer.Register<IBaseInterface, FirstService>(serviceKey: "first", reuse: Reuse.Singleton);
    adaptedContainer.Register<IBaseInterface, SecondService>(serviceKey: "second", reuse: Reuse.Singleton);
    adaptedContainer.Register<IBaseInterface, ThirdService>(serviceKey: "third", reuse: Reuse.Singleton);
    DependencyContainer.Container = adaptedContainer; 
});



builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IMyService, MyService>();
builder.Services.AddSingleton<IMyDependentSerivce, MyDependentService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();


public static class DependencyContainer
{
    public static IContainer Container { get; set; }
}