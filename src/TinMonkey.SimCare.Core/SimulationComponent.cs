
namespace TinMonkey.SimCare.Core;

public class SimulationComponent : ISimulationComponent, IUpdateable
{
    public virtual Task Initialize() => Task.CompletedTask;

    public virtual async Task UpdateAsync(SimulationTime simulationTime, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(simulationTime);
        ArgumentNullException.ThrowIfNull(cancellationToken);

        Console.WriteLine($"Tick {simulationTime.TotalTime} {simulationTime.ElapsedTime}");

        await Task.Delay(0, cancellationToken)
            .ConfigureAwait(false);
    }
}
