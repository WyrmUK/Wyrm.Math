namespace Wyrm.Math.Matrix;

/// <summary>
/// A <see cref="double"/> and <see cref="GeneralMatrixDouble"/> pair.
/// </summary>
/// <param name="Value">A <see cref="double"/> value.</param>
/// <param name="Matrix">A <see cref="GeneralMatrixDouble"/> matrix.</param>
public record ValueMatrixDoublePair(double Value, GeneralMatrixDouble Matrix);
