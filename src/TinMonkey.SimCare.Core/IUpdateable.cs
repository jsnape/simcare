namespace TinMonkey.SimCare.Core;

public interface IUpdateable
{
    Task UpdateAsync(SimulationTime simulationTime, CancellationToken cancellationToken);
}
