using System.Text.Json;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix;

/// <summary>
/// Converts a <see cref="ComplexMatrixDouble"/> to or from JSON.
/// </summary>
public class ComplexMatrixDoubleJsonConverter : GeneralMatrixJsonConverter<ComplexMatrixDouble, GeneralComplexNumberDouble>
{
    /// <inheritdoc/>
    protected override GeneralComplexNumberDouble GetValue(ref Utf8JsonReader reader, JsonSerializerOptions options) =>
        new GeneralComplexNumberDoubleJsonConverter().Read(ref reader, typeof(GeneralComplexNumberDouble), options);

    /// <inheritdoc/>
    protected override ComplexMatrixDouble MakeValue(IEnumerable<IEnumerable<GeneralComplexNumberDouble>> values) =>
        new(values);

    /// <inheritdoc/>
    protected override int Rows(ComplexMatrixDouble value) =>
        value.Rows;

    /// <inheritdoc/>
    protected override int Columns(ComplexMatrixDouble value) =>
        value.Columns;

    /// <inheritdoc/>
    protected override void WriteValue(Utf8JsonWriter writer, ComplexMatrixDouble value, int column, int row, JsonSerializerOptions options) =>
        new GeneralComplexNumberDoubleJsonConverter().Write(writer, value[column, row], options);
}
