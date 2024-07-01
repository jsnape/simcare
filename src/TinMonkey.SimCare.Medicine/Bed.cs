using System.Diagnostics;
using TinMonkey.SimCare.Core;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{Name} Ward {Ward}")]
public class Bed(string Name, Ward Ward) : SimulationComponent
{
    public string Name { get; } = Name;
    public Ward Ward { get; } = Ward;

    public override async Task UpdateAsync(SimulationTime simulationTime, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(simulationTime);
        ArgumentNullException.ThrowIfNull(cancellationToken);

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

        await drawContext.Log.WriteLineAsync($"Bed {this.Ward.Name}/{this.Name}");

        this.Updated = false;
    }
}
