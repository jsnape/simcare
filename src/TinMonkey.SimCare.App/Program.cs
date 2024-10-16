using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TinMonkey.SimCare.App;

var cancelationToken = new CancellationTokenSource();
Console.CancelKeyPress += (sender, args) =>
{
    cancelationToken.Cancel();
    args.Cancel = true;
};

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<Worker>();
        })
    .Build();

await host
    .RunAsync(cancelationToken.Token)
    .ConfigureAwait(false);
