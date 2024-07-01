using System.Diagnostics;
using TinMonkey.SimCare.Core;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{Name}")]
public class Department(string Name) : SimulationComponent
{
    private readonly List<Ward> wards = [];

    public string Name { get; } = Name;

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

        await drawContext.Log.WriteLineAsync($"Department {Name}");

        this.Updated = false;
    }

    public Ward CreateWard(string? name)
    {
        // Blank ward name should default to the department name.
        var ward = new Ward(name ?? this.Name, this);
        this.wards.Add(ward);

        return ward;
    }
}
