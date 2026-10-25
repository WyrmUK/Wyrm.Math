using System.Text.Json;
using System.Text.Json.Serialization;
using Wyrm.Math.ComplexNumbers.Base;

namespace Wyrm.Math.ComplexNumbers;

/// <summary>
/// Converts a <see cref="GeneralComplexNumberDecimal"/> to or from JSON.
/// </summary>
public class GeneralComplexNumberDecimalJsonConverter : GeneralComplexNumberJsonConverter<GeneralComplexNumberDecimal, decimal>
{
    /// <inheritdoc/>
    protected override decimal GetValue(ref Utf8JsonReader reader) =>
        reader.GetDecimal();

    /// <inheritdoc/>
    protected override GeneralComplexNumberDecimal MakeValue(decimal real, decimal imaginary) =>
        new(real, imaginary);

    /// <inheritdoc/>
    protected override void WriteValues(Utf8JsonWriter writer, GeneralComplexNumberDecimal value)
    {
        writer.WriteNumberValue(value.Real);
        writer.WriteNumberValue(value.Imaginary);
    }
}
