namespace TinMonkey.SimCare.Core;

public interface IDrawable
{
    bool Visible { get; }

    Task DrawAsync(DrawContext drawContext, SimulationTime simulationTime, CancellationToken cancellationToken);
}
