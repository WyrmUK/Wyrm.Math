using System.Diagnostics.CodeAnalysis;

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

    internal static (string Real, string Imaginary) SplitForParse(string s)
    {
        var minimisedValue = s.Replace(" ", string.Empty).Trim(ComplexStartChar, ComplexEndChar);
        var pos = minimisedValue.IndexOfAny(['+', '-'], 1);
        string[] parts = pos < 1
            ? [minimisedValue]
            : [minimisedValue[..pos], minimisedValue[pos..].TrimStart('+')];
        return parts.Length == 1
            ? (parts[0].EndsWith($"{ImaginaryIdentifier}", StringComparison.OrdinalIgnoreCase)
                ? ("0", parts[0][..^1])
                : (parts[0], "0"))
            : (parts[0].EndsWith($"{ImaginaryIdentifier}", StringComparison.OrdinalIgnoreCase)
                ? (parts[1], parts[0][..^1])
                : (parts[0], parts[1][..^1]));
    }
}
