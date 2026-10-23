using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wyrm.Math.ComplexNumbers.Base;

/// <summary>
/// Base abstract class for a <see cref="GeneralComplexNumber{T}"/> JSON converter.
/// </summary>
public abstract class GeneralComplexNumberJsonConverter<T1,T2> : JsonConverter<T1>
{
    /// <inheritdoc/>
    public override T1 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert != typeof(T1)) throw new JsonException($"Unable to convert type {typeToConvert}");

        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException($"Malformed complex number JSON.");
        reader.Read();

        var real = GetValue(ref reader);
        reader.Read();

        var imaginary = GetValue(ref reader);
        reader.Read();

        if (reader.TokenType != JsonTokenType.EndArray) throw new JsonException($"Malformed complex number JSON.");

        return MakeValue(real, imaginary);
    }

    /// <summary>
    /// Gets a number value.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns>The value.</returns>
    protected abstract T2 GetValue(ref Utf8JsonReader reader);

    /// <summary>
    /// Makes the complex number.
    /// </summary>
    /// <param name="real">The real part.</param>
    /// <param name="imaginary">The imaginary part.</param>
    /// <returns>The complex number.</returns>
    protected abstract T1 MakeValue(T2 real, T2 imaginary);

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, T1 value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        WriteValues(writer, value);
        writer.WriteEndArray();
    }

    /// <summary>
    /// Writes the real and imaginary values.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The value to write.</param>
    protected abstract void WriteValues(Utf8JsonWriter writer, T1 value);
}
