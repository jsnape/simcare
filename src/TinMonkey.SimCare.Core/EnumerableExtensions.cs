namespace TinMonkey.SimCare.Core;

public static class EnumerableExtensions
{
    /// <summary>
    /// Generates a single value sequence.
    /// </summary>
    /// <typeparam name="T">Type of sequence to generate.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A sequence with a single item in it.</returns>
    public static IEnumerable<T> ToEnumerable<T>(this T value)
    {
        yield return value;
    }
}
