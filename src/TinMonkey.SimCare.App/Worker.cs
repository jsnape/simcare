using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TinMonkey.SimCare.Core;
using TinMonkey.SimCare.Medicine;

namespace TinMonkey.SimCare.App;

internal class Worker(ILogger<Worker> logger, IHost host) : BackgroundService
{
    private readonly ILogger<Worker> logger = logger;
    private readonly IHost host = host;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Starting simulation");

        var patients = new Patient[]
        {
            new Patient("Alice", DateOnly.Parse("1971-10-13")),
        };

        var simulation = new Simulation(patients);
        await simulation
            .RunAsync(stoppingToken)
            .ConfigureAwait(false);

        Console.WriteLine("Ending simulation");

        await this.host.StopAsync(stoppingToken)
            .ConfigureAwait(false);
    }
}
