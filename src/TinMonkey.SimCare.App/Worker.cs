using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TinMonkey.SimCare.Core;
using TinMonkey.SimCare.Medicine;
using TinMonkey.SimCare.Medicine.Fhir;

namespace TinMonkey.SimCare.App;

internal class Worker(ILogger<Worker> logger, IHost host) : BackgroundService
{
    private readonly ILogger<Worker> logger = logger;
    private readonly IHost host = host;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var encounter = new Encounter(EncounterStatus.Planned)
        {
            EncounterClass = EncounterClass.InPatient,
            Subject = new Reference(Guid.NewGuid().ToBase36(), null, null, null),
        };


        Console.WriteLine("Starting simulation");
        var patientFactory = new BogusPatientFactory();

        var patients = Enumerable
            .Range(1, 10)
            .Select(_ => patientFactory.CreateBogusPatient())
            .Select(p => new PatientSimulator(p))
            .Cast<ISimulationComponent>();

        var departments = new Department[]
        {
            new("Acute Medicine"),
            new("Cardiology"),
            new("Gastroenterology"),
            new("General Surgery"),
            new("Neurology"),
        };

        var departmentSimulators = departments
            .Select(d => new LocationSimulator(d));

        var locationSimulators = departments
            .Select(d =>
            {
                var wardName = string.Concat(d.Name.AsSpan(0, 1), "1");
                var ward = d.CreateWard(wardName);

                var beds = Enumerable
                    .Range(1, 4)
                    .Select(i => d.CreateBed(wardName + "/" + i, ward));

                return new LocationSimulator(ward);
            });

        var drawContext = new DrawContext { Log = Console.Out };

        var components = departmentSimulators
            .Concat(locationSimulators)
            .Concat(patients)
            .ToList();

        var simulation = new Simulation(drawContext, components);
        await simulation
            .RunAsync(stoppingToken)
            .ConfigureAwait(false);

        Console.WriteLine("Ending simulation");

        await this.host.StopAsync(stoppingToken)
            .ConfigureAwait(false);
    }
}
