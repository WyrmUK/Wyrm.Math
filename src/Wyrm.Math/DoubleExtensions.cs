namespace Wyrm.Math;

/// <summary>
/// Extension methods for <see cref="double"/>s.
/// </summary>
public static class DoubleExtensions
{
    /// <inheritdoc cref="System.Math.Abs(double)"/>
    public static double Abs(this double value) => System.Math.Abs(value);

    /// <inheritdoc cref="System.Math.Acos(double)"/>
    public static double Acos(this double d) => System.Math.Acos(d);
    /*
    /// <inheritdoc cref="System.Math.Acosh(double)"/>
    public static double Acosh(this double d) => System.Math.Acosh(d);
    */
    /// <inheritdoc cref="System.Math.Asin(double)"/>
    public static double Asin(this double d) => System.Math.Asin(d);
    /*
    /// <inheritdoc cref="System.Math.Asinh(double)"/>
    public static double Asinh(this double d) => System.Math.Asinh(d);
    */
    /// <inheritdoc cref="System.Math.Atan(double)"/>
    public static double Atan(this double d) => System.Math.Atan(d);
    /*
    /// <inheritdoc cref="System.Math.Atanh(double)"/>
    public static double Atanh(this double d) => System.Math.Atanh(d);
    */
    /// <inheritdoc cref="System.Math.Atan2(double, double)"/>
    public static double Atan2(this double y, double x) => System.Math.Atan2(y, x);

    /// <inheritdoc cref="System.Math.BitDecrement(double)"/>
    public static double BitDecrement(this double x) => System.Math.BitDecrement(x);

    /// <inheritdoc cref="System.Math.BitIncrement(double)"/>
    public static double BitIncrement(this double x) => System.Math.BitIncrement(x);
    /*
    /// <inheritdoc cref="System.Math.Cbrt(double)"/>
    public static double Cbrt(this double d) => System.Math.Cbrt(d);
    */
    /// <inheritdoc cref="System.Math.Ceiling(double)"/>
    public static double Ceiling(this double a) => System.Math.Ceiling(a);
    /*
    /// <inheritdoc cref="System.Math.CopySign(double, double)"/>
    public static double CopySign(this double y, double x) => System.Math.CopySign(y, x);
    */
    /// <inheritdoc cref="System.Math.Clamp(double, double, double)"/>
    public static double Clamp(this double value, double min, double max) => System.Math.Clamp(value, min, max);

    /// <inheritdoc cref="System.Math.Cos(double)"/>
    public static double Cos(this double a) => System.Math.Cos(a);
    /*
    /// <inheritdoc cref="System.Math.Cosh(double)"/>
    public static double Cosh(this double value) => System.Math.Cosh(value);
    */
    /// <inheritdoc cref="System.Math.Exp(double)"/>
    public static double Exp(this double d) => System.Math.Exp(d);

    /// <inheritdoc cref="System.Math.Floor(double)"/>
    public static double Floor(this double d) => System.Math.Floor(d);
    /*
    /// <inheritdoc cref="System.Math.FusedMultiplyAdd(double, double, double)"/>
    public static double FusedMultiplyAdd(this double x, double y, double z) => System.Math.FusedMultiplyAdd(x, y, z);

    /// <inheritdoc cref="System.Math.IEEERemainder(double, double)"/>
    public static double IEEERemainder(this double y, double x) => System.Math.IEEERemainder(y, x);

    /// <inheritdoc cref="System.Math.ILogB(double)"/>
    public static int ILogB(this double d) => System.Math.ILogB(d);
    */
    /// <inheritdoc cref="System.Math.Log(double)"/>
    public static double Log(this double d) => System.Math.Log(d);
    /*
    /// <inheritdoc cref="System.Math.Log(double, double)"/>
    public static double Log(this double d, double newBase) => System.Math.Log(d, newBase);

    /// <inheritdoc cref="System.Math.Log2(double)"/>
    public static double Log2(this double x) => System.Math.Log2(x);

    /// <inheritdoc cref="System.Math.Log10(double)"/>
    public static double Log10(this double d) => System.Math.Log10(d);
    */
    /// <inheritdoc cref="System.Math.Max(double, double)"/>
    public static double Max(this double val1, double val2) => System.Math.Max(val1, val2);
    /*
    /// <inheritdoc cref="System.Math.MaxMagnitude(double, double)"/>
    public static double MaxMagnitude(this double val1, double val2) => System.Math.MaxMagnitude(val1, val2);
    */
    /// <inheritdoc cref="System.Math.Min(double, double)"/>
    public static double Min(this double val1, double val2) => System.Math.Min(val1, val2);
    /*
    /// <inheritdoc cref="System.Math.MinMagnitude(double, double)"/>
    public static double MinMagnitude(this double val1, double val2) => System.Math.MinMagnitude(val1, val2);
    */
    /// <inheritdoc cref="System.Math.Pow(double, double)"/>
    public static double Pow(this double x, double y) => System.Math.Pow(x, y);
    /*
    /// <inheritdoc cref="System.Math.ReciprocalEstimate(double)"/>
    public static double ReciprocalEstimate(this double d) => System.Math.ReciprocalEstimate(d);

    /// <inheritdoc cref="System.Math.ReciprocalSqrtEstimate(double)"/>
    public static double ReciprocalSqrtEstimate(this double d) => System.Math.ReciprocalSqrtEstimate(d);
    */
    /// <inheritdoc cref="System.Math.Round(double)"/>
    public static double Round(this double a) => System.Math.Round(a);

    /// <inheritdoc cref="System.Math.Round(double, int)"/>
    public static double Round(this double a, int digits) => System.Math.Round(a, digits);

    /// <inheritdoc cref="System.Math.Round(double, MidpointRounding)"/>
    public static double Round(this double a, MidpointRounding mode) => System.Math.Round(a, mode);

    /// <inheritdoc cref="System.Math.Round(double, int, MidpointRounding)"/>
    public static double Round(this double a, int digits, MidpointRounding mode) => System.Math.Round(a, digits, mode);
    /*
    /// <inheritdoc cref="System.Math.ScaleB(double, int)"/>
    public static double ScaleB(this double a, int n) => System.Math.ScaleB(a, n);
    */
    /// <inheritdoc cref="System.Math.Sign(double)"/>
    public static int Sign(this double value) => System.Math.Sign(value);

    /// <inheritdoc cref="System.Math.Sin(double)"/>
    public static double Sin(this double a) => System.Math.Sin(a);
    /*
    /// <inheritdoc cref="System.Math.Acos(double)"/>
    public static (double Sin, double Cos) SinCos(this double x) => System.Math.SinCos(x);

    /// <inheritdoc cref="System.Math.Sinh(double)"/>
    public static double Sinh(this double value) => System.Math.Sinh(value);
    */
    /// <summary>
    /// Returns the square of the number.
    /// </summary>
    /// <param name="d">The number to square.</param>
    /// <returns>The square of the number.</returns>
    public static double Sqr(this double d) => d * d;

    /// <inheritdoc cref="System.Math.Sqrt(double)"/>
    public static double Sqrt(this double d) => System.Math.Sqrt(d);
    /*
    /// <inheritdoc cref="System.Math.Tan(double)"/>
    public static double Tan(this double a) => System.Math.Tan(a);

    /// <inheritdoc cref="System.Math.Tanh(double)"/>
    public static double Tanh(this double value) => System.Math.Tanh(value);
    */
    /// <inheritdoc cref="System.Math.Truncate(double)"/>
    public static double Truncate(this double d) => System.Math.Truncate(d);
}
