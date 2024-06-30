using TinMonkey.SimCare.Core;
using TinMonkey.SimCare.Medicine;

namespace TinMonkey.SimCare.Web;

public class SimulationWorker : BackgroundService
{
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
    }
}
