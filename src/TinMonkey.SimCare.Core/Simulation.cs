using System.Diagnostics;

namespace TinMonkey.SimCare.Core;

public class Simulation
{
    private readonly DrawContext drawContext;
    private readonly List<ISimulationComponent> components = [];

    public Simulation(DrawContext drawContext, IEnumerable<ISimulationComponent> components)
    {
        ArgumentNullException.ThrowIfNull(drawContext);
        ArgumentNullException.ThrowIfNull(components);

        this.drawContext = drawContext;
        this.components.AddRange(components);
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken)
            .ConfigureAwait(false);

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var lastTime = stopwatch.Elapsed;
        while (!cancellationToken.IsCancellationRequested)
        {
            var updateTime = stopwatch.Elapsed;
            var elapsedTime = updateTime - lastTime;
            lastTime = updateTime;

            var simulationTime = new SimulationTime(updateTime, elapsedTime);

            await this.UpdateAsync(simulationTime, cancellationToken)
                .ConfigureAwait(false);
        }

        stopwatch.Stop();
    }

    public async Task UpdateAsync(SimulationTime simulationTime, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(simulationTime);

            foreach (var component in this.components)
            {
                if (component is IUpdateable updateable)
                {
                    await updateable
                        .UpdateAsync(simulationTime, cancellationToken)
                        .ConfigureAwait(false);
                }
            }

            foreach (var component in this.components)
            {
                if (component is IDrawable drawable && drawable.Visible)
                {
                    await drawable
                        .DrawAsync(this.drawContext, simulationTime, cancellationToken)
                        .ConfigureAwait(false);
                }
            }

            await Task.Delay(0, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (TaskCanceledException)
        {
        }
    }
}
