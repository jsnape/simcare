using Microsoft.Extensions.DependencyInjection;

namespace TinMonkey.Domain.Core;

public static class DomainEvents
{
    [ThreadStatic] //so that each thread has its own callbacks
    private static List<Delegate>? actions;

    public static IServiceProvider? ServiceProvider { get; set; }

    //Registers a callback for the given domain event
    public static void Register<T>(Action<T> callback) where T : IDomainEvent
    {
        actions ??= [];
        actions.Add(callback);
    }

    //Clears callbacks passed to Register on the current thread
    public static void ClearCallbacks()
    {
        actions = null;
    }

    //Raises the given domain event
    public static void Raise<T>(T args) where T : IDomainEvent
    {
        if (ServiceProvider != null)
        {
            foreach (var handler in ServiceProvider.GetServices<IHandles<T>>())
            {
                handler.Handle(args);
            }
        }

        if (actions != null)
        {
            foreach (var action in actions)
            {
                if (action is Action<T> eventAction)
                {
                    eventAction(args);
                }
            }
        }
    }
}
