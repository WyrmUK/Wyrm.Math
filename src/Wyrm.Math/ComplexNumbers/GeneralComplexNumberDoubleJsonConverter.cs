using System.Text.Json;
using Wyrm.Math.ComplexNumbers.Base;

namespace Wyrm.Math.ComplexNumbers;

/// <summary>
/// Converts a <see cref="GeneralComplexNumberDouble"/> to or from JSON.
/// </summary>
public class GeneralComplexNumberDoubleJsonConverter : GeneralComplexNumberJsonConverter<GeneralComplexNumberDouble, double>
{
    /// <inheritdoc/>
    protected override double GetValue(ref Utf8JsonReader reader) =>
        reader.GetDouble();

    /// <inheritdoc/>
    protected override GeneralComplexNumberDouble MakeValue(double real, double imaginary) =>
        new GeneralComplexNumberDouble(real, imaginary);

    /// <inheritdoc/>
    protected override void WriteValues(Utf8JsonWriter writer, GeneralComplexNumberDouble value)
    {
        writer.WriteNumberValue(value.Real);
        writer.WriteNumberValue(value.Imaginary);
    }
}
