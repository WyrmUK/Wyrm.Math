using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wyrm.Math.Matrix.Base;

/// <summary>
/// Base abstract class for a <see cref="GeneralMatrix{T}"/> JSON converter.
/// </summary>
public abstract class GeneralMatrixJsonConverter<T1, T2> : JsonConverter<T1>
{
    /// <inheritdoc/>
    public override T1 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert != typeof(T1)) throw new JsonException($"Unable to convert type {typeToConvert}");

        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException($"Malformed complex number JSON.");
        reader.Read();

        var values = new List<List<T2>>();
        if (reader.TokenType == JsonTokenType.EndArray) return MakeValue(values);

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            var rowValues = new List<T2>();

            if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException($"Malformed complex number JSON.");
            reader.Read();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                rowValues.Add(GetValue(ref reader));
                reader.Read();
            }
            reader.Read();

            values.Add(rowValues);
        }

        if (reader.TokenType != JsonTokenType.EndArray) throw new JsonException($"Malformed complex number JSON.");

        return MakeValue(values);
    }

    /// <summary>
    /// Gets a number value.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns>The value.</returns>
    protected abstract T2 GetValue(ref Utf8JsonReader reader);

    /// <summary>
    /// Makes the matrix.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The matrix.</returns>
    protected abstract T1 MakeValue(IEnumerable<IEnumerable<T2>> values);

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, T1 value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        for (var row = 0; row < Rows(value); ++row)
        {
            writer.WriteStartArray();
            for (var col = 0; col < Columns(value); ++col)
            {
                WriteValue(writer, value, col, row);
            }
            writer.WriteEndArray();
        }
        writer.WriteEndArray();
    }

    /// <summary>
    /// Gets the number of rows.
    /// </summary>
    /// <param name="value">The matrix.</param>
    /// <returns>The numebr of rows.</returns>
    protected abstract int Rows(T1 value);

    /// <summary>
    /// Gets the number of columns.
    /// </summary>
    /// <param name="value">The matrix.</param>
    /// <returns>The numebr of columns.</returns>
    protected abstract int Columns(T1 value);

    /// <summary>
    /// Writes the real and imaginary values.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="column">The column.</param>
    /// <param name="row">The row.</param>
    protected abstract void WriteValue(Utf8JsonWriter writer, T1 value, int column, int row);
}
