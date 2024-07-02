using TinMonkey.SimCare.Core;
using TinMonkey.SimCare.App;
using TinMonkey.SimCare.Medicine;

namespace TinMonkey.SimCare.Web;

public class SimulationWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Starting simulation");

        var patientFactory = new BogusPatientFactory();

        var patients = Enumerable
            .Range(1, 10)
            .Select(_ => patientFactory.CreateBogusPatient())
            .Select(p => new PatientSimulator(p))
            .Cast<ISimulationComponent>();

        var drawContext = new DrawContext { Log = Console.Out };

        var simulation = new Simulation(drawContext, patients);
        await simulation
            .RunAsync(stoppingToken)
            .ConfigureAwait(false);

        Console.WriteLine("Ending simulation");
    }
}
