using TinMonkey.SimCare.Core;
using TinMonkey.SimCare.Patient;

var cancelationToken = new CancellationTokenSource();
Console.CancelKeyPress += (sender, args) =>
{
    cancelationToken.Cancel();
    args.Cancel = true;
};

Console.WriteLine("Starting simulation");

var patients = new Patient[]
{
    new Patient("Alice", DateOnly.Parse("1971-10-13")),
};

var simulation = new Simulation(patients);
await simulation
    .RunAsync(cancelationToken.Token)
    .ConfigureAwait(false);

Console.WriteLine("Ending simulation");
