using System.Diagnostics;
using TinMonkey.SimCare.Core;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{FullName} DOB:{BirthDate} BP:{BloodPressure}")]
public class Patient(string FullName, DateOnly BirthDate) : SimulationComponent
{
    private TimeSpan nextBloodPressureUpdate = TimeSpan.Zero;

    public string FullName { get; init; } = FullName;

    public DateOnly BirthDate { get; init; } = BirthDate;

    public BloodPressure BloodPressure { get; set; } = BloodPressure.Default;

    public override async Task UpdateAsync(SimulationTime simulationTime, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(simulationTime);
        ArgumentNullException.ThrowIfNull(cancellationToken);

        if (this.nextBloodPressureUpdate < simulationTime.TotalTime)
        {
            var nextBP = this.BloodPressure.Next();

            if (nextBP != this.BloodPressure)
            {
                this.BloodPressure = nextBP;
                this.Updated = true;
            }

            this.nextBloodPressureUpdate = simulationTime.TotalTime + TimeSpan.FromSeconds(10);
        }

        await Task.CompletedTask;
    }

    public override async Task DrawAsync(DrawContext drawContext, SimulationTime simulationTime, CancellationToken cancellationToken)
    {
        if (!this.Updated)
        {
            return;
        }

        ArgumentNullException.ThrowIfNull(drawContext);
        ArgumentNullException.ThrowIfNull(simulationTime);
        ArgumentNullException.ThrowIfNull(cancellationToken);

        await drawContext.Log.WriteLineAsync($"Patient {this.FullName} {this.BirthDate} {this.BloodPressure}");

        this.Updated = false;
    }
}
