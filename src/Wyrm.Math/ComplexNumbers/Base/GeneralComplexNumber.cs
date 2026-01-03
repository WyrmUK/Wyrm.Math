using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Wyrm.Math.ComplexNumbers.Base;

internal readonly struct GeneralComplexNumber<T> where T : struct
{
    public T Real { get; } = default;
    public T Imaginary { get; } = default;

    internal const char ImaginaryIdentifier = 'i';
    internal const char ComplexStartChar = '(';
    internal const char ComplexEndChar = ')';

    public GeneralComplexNumber(T real, T imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public GeneralComplexNumber(GeneralComplexNumber<T> complexNumber) : this(complexNumber.Real, complexNumber.Imaginary)
    {
    }

    public override int GetHashCode() =>
        Real.GetHashCode() * 16777619 + Imaginary.GetHashCode();

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        ((obj is GeneralComplexNumber<T> compare) &&
            Real.Equals(compare.Real) &&
            Imaginary.Equals(compare.Imaginary)) ||
        ((obj is T compareValue) &&
            Real.Equals(compareValue) &&
            Imaginary.Equals((T)(object)(Imaginary is double ? 0.0 : 0.0M)));

    public bool Equals(GeneralComplexNumber<T> complexNumber) =>
        Real.Equals(complexNumber.Real) &&
        Imaginary.Equals(complexNumber.Imaginary);

    public override string ToString() =>
        ToString(Real.ToString() ?? "0", Imaginary.ToString() ?? "0");

    internal static string ToString(string realValue, string imaginaryValue)
    {
        return imaginaryValue.All(c => !char.IsDigit(c) || c == '0')
        ? realValue
        : realValue.All(c => !char.IsDigit(c) || c == '0')
            ? $"{imaginaryValue}{ImaginaryIdentifier}"
            : imaginaryValue.StartsWith('-')
                ? $"{ComplexStartChar}{realValue}{imaginaryValue}{ImaginaryIdentifier}{ComplexEndChar}"
                : $"{ComplexStartChar}{realValue}+{imaginaryValue}{ImaginaryIdentifier}{ComplexEndChar}";
    }

    internal delegate bool TryParseFunc(ReadOnlySpan<char> source, NumberStyles numberStyles, out T value);

    internal static bool TryParse(ReadOnlySpan<char> source, TryParseFunc tryParseFunc, out GeneralComplexNumber<T> complexNumber)
    {
        const char paddingSpace = ' ';
        const char plusChar = '+';
        const char minusChar = '-';
        const char exponentChar = 'E';
        const NumberStyles numberStyles = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;

        complexNumber = new GeneralComplexNumber<T>();
        if (source.Length == 0) return false;

        T real = default;
        if (source[0] == ComplexStartChar && source[^1] == ComplexEndChar)
        {
            source = source[1..^1].Trim().TrimStart(plusChar);
            if (source.Length == 0) return false;

            var end = source[0] == minusChar
                ? source[1..].IndexOfAny(paddingSpace, plusChar, minusChar) + 1
                : source.IndexOfAny(paddingSpace, plusChar, minusChar);

            if (end > 0 && (source[end] == minusChar || source[end] == plusChar) && char.ToUpper(source[end - 1]) == exponentChar)
            {
                end = source[(end + 1)..].IndexOfAny(paddingSpace, plusChar, minusChar) + end + 1;
            }
            if (end > 0)
            {
                if (!tryParseFunc(source[..end], numberStyles, out real)) return false;
                var start = source[end..].IndexOfAnyExcept(paddingSpace, plusChar) + end;
                if (source[start - 1] != plusChar && source[start] != minusChar) return false;
                source = start < end ? source[end..] : source[start..];
            }
        }
        T imaginary = default;
        if (source[^1] == ImaginaryIdentifier)
        {
            if (source[^2] == paddingSpace || !tryParseFunc(source[..^1], numberStyles, out imaginary)) return false;
        }
        else
        {
            if (!tryParseFunc(source, numberStyles, out real)) return false;
        }
        complexNumber = new GeneralComplexNumber<T>(real, imaginary);
        return true;
    }
}
