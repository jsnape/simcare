using System.Diagnostics;

namespace TinMonkey.SimCare.Core;

public class Simulation
{
    private readonly List<ISimulationComponent> components = [];

    public Simulation(IEnumerable<ISimulationComponent> components)
    {
        ArgumentNullException.ThrowIfNull(components);
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

            Console.WriteLine($"Tick {simulationTime.TotalTime} {simulationTime.ElapsedTime}");

            foreach (var component in this.components)
            {
                if (component is IUpdateable updateable)
                {
                    await updateable
                        .UpdateAsync(simulationTime, cancellationToken)
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
