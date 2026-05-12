namespace Core.Shared.Extensions;

public static class FunctionalExtensions
{
    public static T Tap<T>(this T instance, Action action)
    {
        action();
        return instance;
    }

    public static T Tap<T>(this T instance, Action<T> action)
    {
        action(instance);
        return instance;
    }

    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (T element in collection)
            action(element);
    }

    public static void TryCatchFinally(Action action, Action<Exception> catchAction, Action finallyAction)
    {
        try { action(); }
        catch (Exception exception) { catchAction(exception); }
        finally { finallyAction(); }
    }

    public static void TryCatch(Action action, Action<Exception> catchAction)
    {
        try { action(); }
        catch (Exception exception) { catchAction(exception); }
    }
}
