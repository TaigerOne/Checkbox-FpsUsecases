using Checkbox.Fdm.Sdk;
using FpsUsecases.PosExample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = CreateHostBuilder(args).Build();
Console.WriteLine("Fps Finances Pos Example");
try
{
    //Inject the Checkbox Service into our application
    var myFpsExample = host.Services.GetRequiredService<PosExample>();

    //Initialize the Checkboxes, this will configure their destination address settings automatically if possible
    await myFpsExample.InitPos();
    await myFpsExample.RunPos();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

Console.WriteLine("Powered by www.checkbox.be");
return;


static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            //TODO Add your Checkbox Api key here
            services.AddCheckbox("YOUR CHECKBOX API KEY");
            services.AddSingleton<PosExample>();
        });

