using System.Diagnostics;
using TinMonkey.SimCare.Core;

namespace TinMonkey.SimCare.Medicine;

[DebuggerDisplay("{Name} Dept {Department}")]
public class Ward(string Name, Department Department) : SimulationComponent
{
    private readonly List<Bed> beds = [];

    public string Name { get; } = Name;
    public Department Department { get; } = Department;

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

        await drawContext.Log.WriteLineAsync($"Ward {this.Name}");

        this.Updated = false;
    }

    public Bed CreateBed(string name)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(name);

        var bed = new Bed(name, this);
        this.beds.Add(bed);

        return bed;
    }
}
