using System.Numerics;

namespace Wyrm.Math;

/// <summary>
/// Extension methods for <see cref="decimal"/>s.
/// </summary>
public static class DecimalExtensions
{
    /// <inheritdoc cref="System.Math.Abs(decimal)"/>
    public static decimal Abs(this decimal value) => System.Math.Abs(value);

    /// <summary>
    /// Returns the angle whose cosine is the number.
    /// </summary>
    /// <param name="d">The number to get the acos of (-1 &lt;= d &lt;= 1).</param>
    /// <returns>The angle whose cosine is the number.</returns>
    /// <exception cref="InvalidOperationException">Thrown if d is above 1 or below -1.</exception>
    public static decimal Acos(this decimal d)
    {
        if (d < -1M || d > 1M) throw new InvalidOperationException("Values must be between -1 and 1.");

        if (d == -1M) return Decimal.Pi;
        if (d == 0M) return Decimal.HalfPi;
        if (d == 1M) return 0M;

        return Decimal.HalfPi - d.Asin();
    }
    /*
    /// <inheritdoc cref="System.Math.Acosh(double)"/>
    public static decimal Acosh(this decimal d) => System.Math.Acosh(d);
    */
    /// <summary>
    /// Returns the angle whose sine is the number.
    /// </summary>
    /// <param name="d">The number to get the asin of (-1 &lt;= d &lt;= 1).</param>
    /// <returns>The angle whose sine is the number.</returns>
    /// <exception cref="InvalidOperationException">Thrown if d is above 1 or below -1.</exception>
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
    /// <inheritdoc cref="System.Math.Asinh(double)"/>
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
    /// <inheritdoc cref="System.Math.Atanh(double)"/>
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

    /// <inheritdoc cref="System.Math.BitDecrement(double)"/>
    /// <summary>
    /// Returns the largest value that compares less than a number.
    /// </summary>
    /// <param name="x">The number to decrement.</param>
    /// <returns>The largest value that compares less than a number.</returns>
    /// <exception cref="OverflowException">Thrown if the result will be too small.</exception>
    public static decimal BitDecrement(this decimal x)
    {
        if (x == decimal.MinValue) throw new OverflowException("Result too small for decimal");

        var inc = 0.0000000000000000000000000001M;
        var xd = x - inc;
        while (xd == x)
        {
            inc *= 10M;
            xd = x - inc;
        }
        return xd;
    }

    /// <inheritdoc cref="System.Math.BitIncrement(double)"/>
    /// <summary>
    /// Returns the largest value that compares greater than a number.
    /// </summary>
    /// <param name="x">The number to increment.</param>
    /// <returns>The largest value that compares greater than a number.</returns>
    /// <exception cref="OverflowException">Thrown if the result will be too large.</exception>
    public static decimal BitIncrement(this decimal x)
    {
        if (x == decimal.MaxValue) throw new OverflowException("Result too large for decimal");

        var inc = 0.0000000000000000000000000001M;
        var xi = x + inc;
        while (xi == x)
        {
            inc *= 10M;
            xi = x + inc;
        }
        return xi;
    }

    /// <summary>
    /// Returns the cube root of a number.
    /// </summary>
    /// <param name="d">The number to get the cube root of.</param>
    /// <returns>The cube root of a number.</returns>
    public static decimal Cbrt(this decimal d)
    {
        if (d == 0M) return 0M;

        var estimate = d.Abs().Pow(0.3M);
        if (d < 0M)
        {
            estimate = -estimate;
        }
        var prevEstimate = 1M;
        var term1 = d / 3.0M;
        var term2 = 2M / 3M;

        while (estimate != 0M && estimate != prevEstimate)
        {
            prevEstimate = estimate;
            var divisor = estimate.Sqr();
            if (divisor == 0M) break;
            if (term1 == 0)
            {
                estimate = d / (3.0M * divisor) + term2 * estimate;
            }
            else
            {
                estimate = term1 / divisor + term2 * estimate;
            }
        }
        return estimate;
    }

    /// <inheritdoc cref="System.Math.Ceiling(decimal)"/>
    public static decimal Ceiling(this decimal a) => System.Math.Ceiling(a);

    /// <summary>
    /// Returns a value with the magnitude of x and the sign of y.
    /// </summary>
    /// <param name="x">Magnitude.</param>
    /// <param name="y">Sign.</param>
    /// <returns>A value with the magnitude of x and the sign of y.</returns>
    public static decimal CopySign(this decimal x, decimal y)
    {
        var parts = decimal.GetBits(x);
        return new decimal(parts[0], parts[1], parts[2], y < 0, (byte)((parts[3] >> 16) & 0x7F));
    }

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
    /// <inheritdoc cref="System.Math.Cosh(double)"/>
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

    /// <summary>
    /// Returns (x * y) + z with single rounding.
    /// </summary>
    /// <param name="x">First multiplied term.</param>
    /// <param name="y">Second multiplied term.</param>
    /// <param name="z">Addition term.</param>
    /// <returns>Single rounded result of (x * y) + z.</returns>
    public static decimal FusedMultiplyAdd(this decimal x, decimal y, decimal z) => x * y + z;

    /// <summary>
    /// Returns the remainder resulting from a division of a number by another.
    /// </summary>
    /// <param name="x">The number.</param>
    /// <param name="y">The divisor.</param>
    /// <returns>A number equal to x - (y Q), where Q is the quotient of x / y rounded to the nearest integer (even if exactly halfway).</returns>
    /// <exception cref="OverflowException">Thrown if y is zero.</exception>
    public static decimal IEEERemainder(this decimal x, decimal y)
    {
        if (y == 0M) throw new OverflowException("Cannot divide by zero.");

        var quotient = (x / y).Round(MidpointRounding.ToEven);
        return x - (y * quotient);
    }

    /// <summary>
    /// Returns the base 2 integer (binary) logarithm of a number.
    /// </summary>
    /// <param name="d">The number.</param>
    /// <returns>The base 2 integer logarithm of the number.</returns>
    /// <exception cref="OverflowException">Thrown if the number is zero.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the number is less than zero.</exception>
    public static int ILogB(this decimal d)
    {
        var logD = d.Log2().Round(26);
        return logD >= 0M
            ? (int)logD.Truncate()
            : (int)(logD - 0.9999999999999999999999999999M).Truncate();
    }

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

    /// <summary>
    /// Returns the logrithm of a number in a base.
    /// </summary>
    /// <param name="d">The number to take the logarithm of.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <returns>The logarithm of the number in the base.</returns>
    /// <exception cref="OverflowException">Thrown if the number is zero.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the number is less than zero.</exception>
    public static decimal Log(this decimal d, decimal newBase) => d.Log() / newBase.Log();

    /// <summary>
    /// Returns the base 2 logarithm of a number.
    /// </summary>
    /// <param name="d">The number to take the base 2 logarithm of.</param>
    /// <returns>The base 2 logarithm.</returns>
    /// <exception cref="OverflowException">Thrown if the number is zero.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the number is less than zero.</exception>
    public static decimal Log2(this decimal d) => d.Log() / Decimal.Log2;

    /// <summary>
    /// Returns the base 10 logarithm of a number.
    /// </summary>
    /// <param name="d">The number to take the base 2 logarithm of.</param>
    /// <returns>The base 10 logarithm.</returns>
    /// <exception cref="OverflowException">Thrown if the number is zero.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the number is less than zero.</exception>
    public static decimal Log10(this decimal d) => d.Log() / Decimal.Log10;

    /// <inheritdoc cref="System.Math.Max(decimal, decimal)"/>
    public static decimal Max(this decimal val1, decimal val2) => System.Math.Max(val1, val2);

    /// <summary>
    /// Returns the number with the largest magnitude.
    /// </summary>
    /// <param name="val1">The first number to compare.</param>
    /// <param name="val2">The second number to compare.</param>
    /// <returns>The number with the largest magnitude.</returns>
    public static decimal MaxMagnitude(this decimal val1, decimal val2)
    {
        var v1Abs = val1.Abs();
        var v2Abs = val2.Abs();
        return v1Abs > v2Abs
            ? val1
            : v1Abs != v2Abs
                ? val2
                : val1 >= val2 ? val1 : val2;
    }

    /// <inheritdoc cref="System.Math.Min(decimal, decimal)"/>
    public static decimal Min(this decimal val1, decimal val2) => System.Math.Min(val1, val2);

    /// <summary>
    /// Returns the number with the smallest magnitude.
    /// </summary>
    /// <param name="val1">The first number to compare.</param>
    /// <param name="val2">The second number to compare.</param>
    /// <returns>The number with the smallest magnitude.</returns>
    public static decimal MinMagnitude(this decimal val1, decimal val2)
    {
        var v1Abs = val1.Abs();
        var v2Abs = val2.Abs();
        return v1Abs < v2Abs
            ? val1
            : v1Abs != v2Abs
                ? val2
                : val1 <= val2 ? val1 : val2;
    }

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

    /// <summary>
    /// Returns an estimate of the reciprocal of a number.
    /// </summary>
    /// <param name="d">The number to estimate the reciprocal of.</param>
    /// <returns>The estimate of the reciprocal of the number.</returns>
    /// <exception cref="OverflowException">If d is zero.</exception>
    public static decimal ReciprocalEstimate(this decimal d) => (decimal)((double)d).ReciprocalEstimate();

    /// <inheritdoc cref="System.Math.ReciprocalSqrtEstimate(double)"/>
    /// <summary>
    /// Returns an estimate of the reciprocal square root of a number.
    /// </summary>
    /// <param name="d">The number to estimate the reciprocal square root of.</param>
    /// <returns>The estimate of the reciprocal square root of the number.</returns>
    /// <exception cref="OverflowException">If d is zero or negative.</exception>
    public static decimal ReciprocalSqrtEstimate(this decimal d) => (decimal)((double)d).ReciprocalSqrtEstimate();

    /// <inheritdoc cref="System.Math.Round(decimal)"/>
    public static decimal Round(this decimal a) => System.Math.Round(a);

    /// <inheritdoc cref="System.Math.Round(decimal, int)"/>
    public static decimal Round(this decimal a, int digits) => System.Math.Round(a, digits);

    /// <inheritdoc cref="System.Math.Round(decimal, MidpointRounding)"/>
    public static decimal Round(this decimal a, MidpointRounding mode) => System.Math.Round(a, mode);

    /// <inheritdoc cref="System.Math.Round(decimal, int, MidpointRounding)"/>
    public static decimal Round(this decimal a, int digits, MidpointRounding mode) => System.Math.Round(a, digits, mode);

    /// <summary>
    /// Returns x * 2^n efficiently.
    /// </summary>
    /// <param name="x">The number to scale.</param>
    /// <param name="n">The power to scale by.</param>
    /// <returns>x * 2^n</returns>
    /// <exception cref="OverflowException">Throw if n is above 96 or below -93.</exception>
    public static decimal ScaleB(this decimal x, int n)
    {
        if (n > 96 || n < -93) throw new OverflowException("Power of 2 outside decimal representation.");

        if (n == 0) return x;
        if (n == 1) return x * 2M;
        if (n == -1) return x * 0.5M;

        var inverse = false;
        if (n < 0)
        {
            inverse = true;
            n = -n;
        }
        var multiplier = 1.0M;
        while (n > 0)
        {
            var pn = n > 62 ? 62 : n;
            var power2 = 1L << pn;
            multiplier *= power2;
            n -= pn;
        }
        return x * (inverse ? 1M / multiplier : multiplier);
    }

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

    /// <inheritdoc cref="System.Math.SinCos(double)"/>
    /// <summary>
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static (decimal Sin, decimal Cos) SinCos(this decimal x) =>
        (x.Sin(), x.Cos());

    /// <summary>
    /// Returns the hyperbolic sine of the angle.
    /// </summary>
    /// <param name="value">The angle to take the hyperbolic sine of.</param>
    /// <returns>The hyperbolic sine of the angle.</returns>
    public static decimal Sinh(this decimal value)
    {
        if (value == 0M) return 0M;

        var estimate = value;
        var prevEstimate = 0M;
        var valueSqr = value.Sqr();
        value *= valueSqr / 6M;
        var factor = 5M;
        while (estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate += value;
            value *= valueSqr / ((factor - 1M) * factor);
            factor += 2M;
        }
        return estimate;
    }

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

    /// <summary>
    /// Returns the tangent of the angle.
    /// </summary>
    /// <param name="a">The angle to take the tangent of.</param>
    /// <returns>The tangent of the angle.</returns>
    /// <exception cref="OverflowException"></exception>
    public static decimal Tan(this decimal a)
    {
        var sinCos = a.SinCos();
        var sinAbs = sinCos.Sin.Abs();
        if (sinAbs - sinCos.Cos.Abs() == sinAbs) throw new OverflowException("Result too large for decimal.");

        return sinCos.Sin / sinCos.Cos;
    }
    /*
    /// <inheritdoc cref="System.Math.Tanh(double)"/>
    public static decimal Tanh(this decimal value) => System.Math.Tanh(value);
    */
    /// <inheritdoc cref="System.Math.Truncate(decimal)"/>
    public static decimal Truncate(this decimal d) => System.Math.Truncate(d);
}
