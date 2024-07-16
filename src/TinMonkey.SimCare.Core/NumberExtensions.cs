using System.Globalization;

namespace TinMonkey.SimCare.Core;

public static class NumberExtensions
{
    /// <summary>
    /// The alpha codes
    /// </summary>
    private const string AlphaCodes = "0123456789abcdefghijklmnopqrstuvwxyz";

    /// <summary>
    /// The separator used for Guid conversion
    /// </summary>
    private const string Separator = ".";

    /// <summary>
    /// Converts the supplied integer value to a string of base 36 alphanumeric characters.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <returns>String of converted alpha numeric characters.</returns>
    public static string ToBase36(this int value)
    {
        var result = string.Empty;

        if (value == 0)
        {
            return "0";
        }

        var sign = value < 0 ? "-" : string.Empty;

        value = Math.Abs(value);

        while (value > 0)
        {
            result = AlphaCodes[value % 36] + result;
            value /= 36;
        }

        return sign + result;
    }

    /// <summary>
    /// Converts the supplied integer value to a string of base 36 alphanumeric characters.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <returns>String of converted alpha numeric characters.</returns>
    public static string ToBase36(this uint value)
    {
        var result = string.Empty;

        if (value == 0)
        {
            return "0";
        }

        while (value > 0)
        {
            var index = value % 36;
            result = AlphaCodes[(int)index] + result;
            value /= 36;
        }

        return result;
    }

    /// <summary>
    /// Converts a unique identifier to a base 36 string.
    /// </summary>
    /// <param name="id">The unique identifier.</param>
    /// <returns>A base 35 alpha string</returns>
    public static string ToBase36(this Guid id)
    {
        var bytes = new byte[16];

        if (!id.TryWriteBytes(bytes))
        {
            throw new InvalidOperationException("Failed to write bytes from Guid.");
        }

        var part1 = BitConverter.ToUInt32(bytes, 0);
        var part2 = BitConverter.ToUInt32(bytes, 4);
        var part3 = BitConverter.ToUInt32(bytes, 8);
        var part4 = BitConverter.ToUInt32(bytes, 12);

        return string.Concat(
            part1.ToBase36(),
            Separator,
            part2.ToBase36(),
            Separator,
            part3.ToBase36(),
            Separator,
            part4.ToBase36());
    }
}
