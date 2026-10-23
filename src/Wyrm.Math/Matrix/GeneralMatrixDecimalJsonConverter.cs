using System.Text.Json;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix;

/// <summary>
/// Converts a <see cref="GeneralMatrixDecimal"/> to or from JSON.
/// </summary>
public class GeneralMatrixDecimalJsonConverter : GeneralMatrixJsonConverter<GeneralMatrixDecimal, decimal>
{
    /// <inheritdoc/>
    protected override decimal GetValue(ref Utf8JsonReader reader) =>
        reader.GetDecimal();

    /// <inheritdoc/>
    protected override GeneralMatrixDecimal MakeValue(IEnumerable<IEnumerable<decimal>> values) =>
        new GeneralMatrixDecimal(values);

    /// <inheritdoc/>
    protected override int Rows(GeneralMatrixDecimal value) =>
        value.Rows;

    /// <inheritdoc/>
    protected override int Columns(GeneralMatrixDecimal value) =>
        value.Columns;

    /// <inheritdoc/>
    protected override void WriteValue(Utf8JsonWriter writer, GeneralMatrixDecimal value, int column, int row) =>
        writer.WriteNumberValue(value[column, row]);
}
