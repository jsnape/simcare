using System.Diagnostics;
using TinMonkey.SimCare.Core;
using TinMonkey.SimCare.Medicine;

namespace TinMonkey.SimCare.App;

[DebuggerDisplay("{Patient.FullName} DOB:{Patient.BirthDate}")]
public class PatientSimulator(Patient Patient) : SimulationComponent
{
    private TimeSpan nextBloodPressureUpdate = TimeSpan.Zero;

    public Patient Patient { get; } = Patient;

    public override async Task UpdateAsync(SimulationTime simulationTime, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(simulationTime);
        ArgumentNullException.ThrowIfNull(cancellationToken);

        if (this.nextBloodPressureUpdate < simulationTime.TotalTime)
        {
            var nextBP = this.Patient.BloodPressure.Next();

            if (nextBP != this.Patient.BloodPressure)
            {
                this.Patient.BloodPressure = nextBP;
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

        await drawContext.Log.WriteLineAsync($"Patient {this.Patient.FullName} {this.Patient.BirthDate} {this.Patient.BloodPressure}");

        this.Updated = false;
    }
}
