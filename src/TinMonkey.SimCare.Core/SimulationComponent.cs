namespace TinMonkey.SimCare.Core;

public abstract class SimulationComponent : ISimulationComponent, IUpdateable, IDrawable
{
    public bool Visible { get; } = true;

    public bool Updated { get; protected set; }

    public virtual Task Initialize() => Task.CompletedTask;

    public abstract Task UpdateAsync(SimulationTime simulationTime, CancellationToken cancellationToken);

    public abstract Task DrawAsync(DrawContext drawContext, SimulationTime simulationTime, CancellationToken cancellationToken);
}
