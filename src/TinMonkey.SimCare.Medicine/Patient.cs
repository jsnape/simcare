using TinMonkey.SimCare.Core;

namespace TinMonkey.SimCare.Medicine;

public class Patient(string FullName, DateOnly BirthDate) : SimulationComponent
{
    public string FullName { get; init; } = FullName;

    public DateOnly BirthDate { get; init; } = BirthDate;

    public BloodPressure BloodPressure { get; set; } = BloodPressure.Default;

    public override Task Initialize() =>
        base.Initialize();

    public override async Task UpdateAsync(SimulationTime simulationTime, CancellationToken cancellationToken)
    {
        await base.UpdateAsync(simulationTime, cancellationToken);

        Console.WriteLine($"Patient {this.FullName} {this.BirthDate} {this.BloodPressure}");
    }
}
