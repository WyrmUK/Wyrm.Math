using System.Text.Json;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix;

/// <summary>
/// Converts a <see cref="ComplexMatrixDecimal"/> to or from JSON.
/// </summary>
public class ComplexMatrixDecimalJsonConverter : GeneralMatrixJsonConverter<ComplexMatrixDecimal, GeneralComplexNumberDecimal>
{
    /// <inheritdoc/>
    protected override GeneralComplexNumberDecimal GetValue(ref Utf8JsonReader reader, JsonSerializerOptions options) =>
        new GeneralComplexNumberDecimalJsonConverter().Read(ref reader, typeof(GeneralComplexNumberDecimal), options);

    /// <inheritdoc/>
    protected override ComplexMatrixDecimal MakeValue(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> values) =>
        new(values);

    /// <inheritdoc/>
    protected override int Rows(ComplexMatrixDecimal value) =>
        value.Rows;

    /// <inheritdoc/>
    protected override int Columns(ComplexMatrixDecimal value) =>
        value.Columns;

    /// <inheritdoc/>
    protected override void WriteValue(Utf8JsonWriter writer, ComplexMatrixDecimal value, int column, int row, JsonSerializerOptions options) =>
        new GeneralComplexNumberDecimalJsonConverter().Write(writer, value[column, row], options);
}
