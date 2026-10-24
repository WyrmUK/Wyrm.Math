namespace Wyrm.Math.Matrix;

/// <summary>
/// A <see cref="decimal"/> and <see cref="GeneralMatrixDecimal"/> pair.
/// </summary>
/// <param name="Value">A <see cref="decimal"/> value.</param>
/// <param name="Matrix">A <see cref="GeneralMatrixDecimal"/> matrix.</param>
public record ValueMatrixDecimalPair(decimal Value, GeneralMatrixDecimal Matrix);
