using System.Diagnostics.CodeAnalysis;

namespace Wyrm.Math.ComplexNumbers.Base;

internal readonly struct GeneralComplexNumber<T> where T : struct
{
    public T Real { get; } = default;
    public T Imaginary { get; } = default;

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

    public override string ToString()
    {
        var realValue = Real.ToString() ?? "0";
        var complexValue = Imaginary.ToString() ?? "0";

        return complexValue == "0" || complexValue == "0.0"
        ? realValue
        : realValue == "0" || realValue == "0.0"
            ? $"{complexValue}i"
            : complexValue.StartsWith('-')
                ? $"({realValue}-{complexValue[1..]}i)"
                : $"({realValue}+{complexValue}i)";
    }
}
