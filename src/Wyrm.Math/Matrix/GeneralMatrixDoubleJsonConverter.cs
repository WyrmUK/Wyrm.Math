using System.Text.Json;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix;

/// <summary>
/// Converts a <see cref="GeneralMatrixDouble"/> to or from JSON.
/// </summary>
public class GeneralMatrixDoubleJsonConverter : GeneralMatrixJsonConverter<GeneralMatrixDouble, double>
{
    /// <inheritdoc/>
    protected override double GetValue(ref Utf8JsonReader reader, JsonSerializerOptions options) =>
        reader.GetDouble();

    /// <inheritdoc/>
    protected override GeneralMatrixDouble MakeValue(IEnumerable<IEnumerable<double>> values) =>
        new(values);

    /// <inheritdoc/>
    protected override int Rows(GeneralMatrixDouble value) =>
        value.Rows;

    /// <inheritdoc/>
    protected override int Columns(GeneralMatrixDouble value) =>
        value.Columns;

    /// <inheritdoc/>
    protected override void WriteValue(Utf8JsonWriter writer, GeneralMatrixDouble value, int column, int row, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value[column, row]);
}
