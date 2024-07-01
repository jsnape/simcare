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
        var patientFactory = new BogusPatientFactory();

        var patients = new Patient[]
        {
            patientFactory.CreateBogusPatient(),
            patientFactory.CreateBogusPatient(),
        };

        var departments = new Department[]
        {
            new("Acute Medicine"),
            new("Cardiology"),
            new("Gastroenterology"),
            new("General Surgery"),
            new("Neurology"),
        };

        foreach (var department in departments)
        {
            var wardName = string.Concat(department.Name.AsSpan(0, 1), "1");
            var ward = department.CreateWard(wardName);

            for (var i = 1; i <= 4; i++)
            {
                ward.CreateBed(wardName + "/" + i);
            }
        }

        var drawContext = new DrawContext { Log = Console.Out };

        var components = departments
            .Cast<ISimulationComponent>()
            .Concat(patients);

        var simulation = new Simulation(drawContext, components);
        await simulation
            .RunAsync(stoppingToken)
            .ConfigureAwait(false);

        Console.WriteLine("Ending simulation");

        await this.host.StopAsync(stoppingToken)
            .ConfigureAwait(false);
    }
}
