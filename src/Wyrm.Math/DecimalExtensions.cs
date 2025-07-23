namespace Wyrm.Math;

/// <summary>
/// Extension methods for <see cref="decimal"/>s.
/// </summary>
public static class DecimalExtensions
{
    /// <inheritdoc cref="System.Math.Abs(decimal)"/>
    public static decimal Abs(this decimal value) => System.Math.Abs(value);
    /*
    /// <inheritdoc cref="System.Math.Acos(decimal)"/>
    public static decimal Acos(this decimal d) => System.Math.Acos(d);

    /// <inheritdoc cref="System.Math.Acosh(decimal)"/>
    public static decimal Acosh(this decimal d) => System.Math.Acosh(d);
    */
    /// <summary>
    /// Returns the angle whose sine is the number.
    /// </summary>
    /// <param name="d">The number to get the asin of (-1 &lt;= d &lt;= 1).</param>
    /// <returns>The angle whose sine is the number.</returns>
    public static decimal Asin(this decimal d)
    {
        if (d < -1M || d > 1M) throw new InvalidOperationException("Values must be between -1 and 1.");

        if (d == 0M) return 0M;
        if (d == 1M) return Decimal.HalfPi;
        if (d < 0M) return -Asin(-d);

        var estimate = d;
        var prevEstimate = 0M;
        var dSqr = d.Sqr();
        d = d * dSqr / 2;
        var factor = 3M;
        while (estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate += d / factor;
            d *= dSqr * factor / (factor + 1M);
            factor += 2M;
        }
        return estimate;
    }
    /*
    /// <inheritdoc cref="System.Math.Asinh(decimal)"/>
    public static decimal Asinh(this decimal d) => System.Math.Asinh(d);
    */
    /// <inheritdoc cref="System.Math.Atan(double)"/>
    /// <summary>
    /// Returns the angle whose tangent is the number.
    /// </summary>
    /// <param name="d">The number to get the Atan of.</param>
    /// <returns>An angle, Θ, in radians between negative pi/2 to positive pi/2.</returns>
    public static decimal Atan(this decimal d)
    {
        if (d < 0M) return -Atan(-d);

        if (d > Decimal.MaxSqrVal) return Decimal.HalfPi;
        if (d > 0.99M) return Asin(d / Sqrt(1M + Sqr(d)));

        var estimate = d;
        var prevEstimate = 0M;
        var dSqr = d.Sqr();
        d = d * -dSqr;
        var factor = 3M;
        while (estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate += d / factor;
            d *= -dSqr;
            factor += 2M;
        }
        return estimate;
    }
    /*
    /// <inheritdoc cref="System.Math.Atanh(decimal)"/>
    public static decimal Atanh(this decimal d) => System.Math.Atanh(d);
    */
    /// <inheritdoc cref="System.Math.Atan2(double, double)"/>
    /// <summary>
    /// Returns the angle whose tangent is the quotient of two specified numbers.
    /// </summary>
    /// <param name="y">First number.</param>
    /// <param name="x">Second number.</param>
    /// <returns>
    /// An angle, Θ, in radians such that tan(Θ) = y/x, where (x, y) is a point in the Cartesian plane.
    /// </returns>
    public static decimal Atan2(this decimal y, decimal x)
    {
        if (y == 0M) return x >= 0M ? 0M : Decimal.Pi;

        if (x == 0M) return (y >= 0M ? Decimal.Pi : -Decimal.Pi) / 2M;

        if (x < 0M)
        {
            if (y < 0M) return (-y).Atan2(-x) - Decimal.Pi;
            else return Decimal.Pi - y.Atan2(-x);
        }

        if (y < 0M) return -(-y).Atan2(x);

        return Atan(y / x);
    }
    /*
    /// <inheritdoc cref="System.Math.BitDecrement(decimal)"/>
    public static decimal BitDecrement(this decimal x) => System.Math.BitDecrement(x);

    /// <inheritdoc cref="System.Math.BitIncrement(decimal)"/>
    public static decimal BitIncrement(this decimal x) => System.Math.BitIncrement(x);

    /// <inheritdoc cref="System.Math.Cbrt(decimal)"/>
    public static decimal Cbrt(this decimal d) => System.Math.Cbrt(d);
    */
    /// <inheritdoc cref="System.Math.Ceiling(decimal)"/>
    public static decimal Ceiling(this decimal a) => System.Math.Ceiling(a);
    /*
    /// <inheritdoc cref="System.Math.CopySign(decimal, decimal)"/>
    public static decimal CopySign(this decimal y, decimal x) => System.Math.CopySign(y, x);
    */
    /// <inheritdoc cref="System.Math.Clamp(decimal, decimal, decimal)"/>
    public static decimal Clamp(this decimal value, decimal min, decimal max) => System.Math.Clamp(value, min, max);

    /// <summary>
    /// Returns the cosine of the angle.
    /// </summary>
    /// <param name="a">The angle.</param>
    /// <returns>The cosine of the angle.</returns>
    public static decimal Cos(this decimal a)
    {
        a = a.Abs();

        if (a > Decimal.TwoPi)
        {
            a -= (a / Decimal.TwoPi).Truncate() * Decimal.TwoPi;
        }

        if (a >= Decimal.Pi)
        {
            a = Decimal.TwoPi - a;
        }

        if (a > Decimal.HalfPi) return -(Decimal.Pi - a).Cos();

        if (a == 0M) return 1M;
        if (a == Decimal.HalfPi) return 0M;

        var estimate = 1M;
        var prevEstimate = 0M;
        var aSqr = a = a.Sqr();
        a /= 2M;
        var factor = 4M;
        while (estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate -= a;
            a *= -aSqr / ((factor - 1M) * factor);
            factor += 2M;
        }
        return estimate;
    }
    /*
    /// <inheritdoc cref="System.Math.Cosh(decimal)"/>
    public static decimal Cosh(this decimal value) => System.Math.Cosh(value);
    */
    /// <summary>
    /// Returns e raised to the number.
    /// Throws an <see cref="OverflowException"/> if the result is too big.
    /// </summary>
    /// <param name="d">The number to raise e by.</param>
    /// <returns>The specified power of e.</returns>
    /// <exception cref="OverflowException">Thrown if the result is too big.</exception>
    public static decimal Exp(this decimal d)
    {
        if (d == 0M) return 1M;
        if (d < Decimal.MinExpPow) return 0M;
        if (d > Decimal.MaxExpPow) throw new OverflowException("Result too big for a decimal.");

        var multiplier = 1M;
        while (d >= 2M)
        {
            d -= 2M;
            multiplier *= Decimal.Exp2;
        }

        while (d <= -2M)
        {
            d += 2M;
            multiplier *= Decimal.ExpNeg2;
        }

        var estimate = 1M;
        var prevEstimate = 0M;
        var dVal = d;
        var factor = 2M;
        while (estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate += d;
            try
            {
                d *= dVal / factor;
            }
            catch (OverflowException)
            {
                break;
            }
            factor += 1M;
        }
        return multiplier * estimate;
    }

    /// <inheritdoc cref="System.Math.Floor(decimal)"/>
    public static decimal Floor(this decimal d) => System.Math.Floor(d);
    /*
    /// <inheritdoc cref="System.Math.FusedMultiplyAdd(decimal, decimal, decimal)"/>
    public static decimal FusedMultiplyAdd(this decimal x, decimal y, decimal z) => System.Math.FusedMultiplyAdd(x, y, z);

    /// <inheritdoc cref="System.Math.IEEERemainder(decimal, decimal)"/>
    public static decimal IEEERemainder(this decimal y, decimal x) => System.Math.IEEERemainder(y, x);

    /// <inheritdoc cref="System.Math.ILogB(decimal)"/>
    public static int ILogB(this decimal d) => System.Math.ILogB(d);
    */
    /// <summary>
    /// Returns the natural logarithm (base e) of a number.
    /// Throws an <see cref="OverflowException"/> if the number is zero.
    /// Throws an <see cref="InvalidOperationException"/> if the number of less than zero.
    /// </summary>
    /// <param name="d">The number to get the log of.</param>
    /// <returns>The natural logarithm of d.</returns>
    /// <exception cref="OverflowException">Thrown if the number is zero.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the number is less than zero.</exception>
    public static decimal Log(this decimal d)
    {
        if (d == 0M) throw new OverflowException("Log of zero is negative infinity.");
        if (d < 0M) throw new InvalidOperationException("Logs of negative numbers are not valid numbers.");
        if (d == 1M) return 0M;

        var addition = 0M;

        while (d >= 1M)
        {
            d *= Decimal.ExpNeg1;
            addition += 1M;
        }

        while (d <= Decimal.ExpNeg1)
        {
            d /= Decimal.ExpNeg1;
            addition -= 1M;
        }

        d -= 1M;
        var estimate = 0M;
        var prevEstimate = 1M;
        var dVal = d;
        var factor = 1M;
        while (estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate += d / factor;
            try
            {
                d *= -dVal;
            }
            catch (OverflowException)
            {
                break;
            }
            factor += 1M;
        }
        return addition + estimate;
    }
    /*
    /// <inheritdoc cref="System.Math.Log(decimal, decimal)"/>
    public static decimal Log(this decimal d, decimal newBase) => System.Math.Log(d, newBase);

    /// <inheritdoc cref="System.Math.Log2(decimal)"/>
    public static decimal Log2(this decimal x) => System.Math.Log2(x);

    /// <inheritdoc cref="System.Math.Log10(decimal)"/>
    public static decimal Log10(this decimal d) => System.Math.Log10(d);
    */
    /// <inheritdoc cref="System.Math.Max(decimal, decimal)"/>
    public static decimal Max(this decimal val1, decimal val2) => System.Math.Max(val1, val2);
    /*
    /// <inheritdoc cref="System.Math.MaxMagnitude(decimal, decimal)"/>
    public static decimal MaxMagnitude(this decimal val1, decimal val2) => System.Math.MaxMagnitude(val1, val2);
    */
    /// <inheritdoc cref="System.Math.Min(decimal, decimal)"/>
    public static decimal Min(this decimal val1, decimal val2) => System.Math.Min(val1, val2);
    /*
    /// <inheritdoc cref="System.Math.MinMagnitude(decimal, decimal)"/>
    public static decimal MinMagnitude(this decimal val1, decimal val2) => System.Math.MinMagnitude(val1, val2);
    */
    /// <summary>
    /// Returns a number raised to a power.
    /// Throws an <see cref="InvalidOperationException"/> if x is 0 and y is zero or negative, or if raising a negative number to a non integer power.
    /// </summary>
    /// <param name="x">The number to raise.</param>
    /// <param name="y">The power to raise to.</param>
    /// <returns>The number x raised to the power y.</returns>
    /// <exception cref="InvalidOperationException">Thrown if taking a zero or negative power of 0 or a non integer power of a negative number.</exception>
    public static decimal Pow(this decimal x, decimal y)
    {
        if (x == 0M && y == 0M) throw new InvalidOperationException("Cannot take a zero power of zero.");

        if (y == 0M || x == 1M) return 1M;
        if (y == 1M) return x;

        if (x == 0M) return y > 0M ? x : throw new InvalidOperationException("Cannot take a negative power of zero.");

        if (y == -1M) return 1M / x;

        if (y.Floor() != y && x < 0M) throw new InvalidOperationException("Negative values can only be raised to integer powers.");

        return x >= 0M
            ? (y * x.Log()).Exp()
            : (y % 2M == 0M)
                ? (y * (-x).Log()).Exp()
                : -(y * (-x).Log()).Exp();
    }
    /*
    /// <inheritdoc cref="System.Math.ReciprocalEstimate(decimal)"/>
    public static decimal ReciprocalEstimate(this decimal d) => System.Math.ReciprocalEstimate(d);

    /// <inheritdoc cref="System.Math.ReciprocalSqrtEstimate(decimal)"/>
    public static decimal ReciprocalSqrtEstimate(this decimal d) => System.Math.ReciprocalSqrtEstimate(d);
    */
    /// <inheritdoc cref="System.Math.Round(decimal)"/>
    public static decimal Round(this decimal a) => System.Math.Round(a);

    /// <inheritdoc cref="System.Math.Round(decimal, int)"/>
    public static decimal Round(this decimal a, int digits) => System.Math.Round(a, digits);

    /// <inheritdoc cref="System.Math.Round(decimal, MidpointRounding)"/>
    public static decimal Round(this decimal a, MidpointRounding mode) => System.Math.Round(a, mode);

    /// <inheritdoc cref="System.Math.Round(decimal, int, MidpointRounding)"/>
    public static decimal Round(this decimal a, int digits, MidpointRounding mode) => System.Math.Round(a, digits, mode);
    /*
    /// <inheritdoc cref="System.Math.ScaleB(decimal, int)"/>
    public static decimal ScaleB(this decimal a, int n) => System.Math.ScaleB(a, n);
    */
    /// <inheritdoc cref="System.Math.Sign(decimal)"/>
    public static int Sign(this decimal value) => System.Math.Sign(value);

    /// <summary>
    /// Returns the sine of the angle.
    /// </summary>
    /// <param name="a">The angle.</param>
    /// <returns>The sine of the angle.</returns>
    public static decimal Sin(this decimal a)
    {
        if (a.Abs() > Decimal.TwoPi)
        {
            a -= (a / Decimal.TwoPi).Truncate() * Decimal.TwoPi;
        }

        if (a < 0M)
        {
            a += Decimal.TwoPi;
        }

        if (a > Decimal.Pi) return -(a - Decimal.Pi).Sin();

        if (a > Decimal.HalfPi) return (Decimal.Pi - a).Sin();

        if (a == 0M) return 0M;
        if (a == Decimal.HalfPi) return 1M;

        var estimate = a;
        var prevEstimate = 0M;
        var aSqr = a.Sqr();
        a *= aSqr / 6M;
        var factor = 5M;
        while (estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate -= a;
            a *= -aSqr / ((factor - 1M) * factor);
            factor += 2M;
        }
        return estimate;
    }
    /*
    /// <inheritdoc cref="System.Math.SinCos(decimal)"/>
    public static (decimal Sin, decimal Cos) SinCos(this decimal x) => System.Math.SinCos(x);

    /// <inheritdoc cref="System.Math.Sinh(decimal)"/>
    public static decimal Sinh(this decimal value) => System.Math.Sinh(value);
    */
    /// <summary>
    /// Returns the square of a number.
    /// </summary>
    /// <param name="d">The number to square.</param>
    /// <returns>The square of the number.</returns>
    public static decimal Sqr(this decimal d) => d * d;

    /// <summary>
    /// Returns the square root of a number.
    /// Throws a <see cref="InvalidOperationException"/> if the number is negative (use GeneralComplexNumberDecimal for square roots of negative numbers).
    /// </summary>
    /// <param name="d">The number to get the square root of.</param>
    /// <returns>
    /// The positive square root.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when d &lt; 0.</exception>
    public static decimal Sqrt(this decimal d)
    {
        if (d < 0) throw new InvalidOperationException("Number must be positive. Use GeneralComplexNumberDecimal for square roots of negative numbers.");

        var estimate = (decimal)((double)d).Sqrt();
        var prevEstimate = 0M;

        while (estimate != 0M && estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate = (prevEstimate + d / prevEstimate) / 2M;
        }

        return estimate;
    }
    /*
    /// <inheritdoc cref="System.Math.Tan(decimal)"/>
    public static decimal Tan(this decimal a) => System.Math.Tan(a);

    /// <inheritdoc cref="System.Math.Tanh(decimal)"/>
    public static decimal Tanh(this decimal value) => System.Math.Tanh(value);
    */
    /// <inheritdoc cref="System.Math.Truncate(decimal)"/>
    public static decimal Truncate(this decimal d) => System.Math.Truncate(d);
}
