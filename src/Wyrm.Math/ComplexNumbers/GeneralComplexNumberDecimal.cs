using System.Diagnostics.CodeAnalysis;
using Wyrm.Math.ComplexNumbers.Base;

namespace Wyrm.Math.ComplexNumbers;

/// <summary>
/// A general complex number struct for <see cref="decimal"/> values.
/// </summary>
public readonly struct GeneralComplexNumberDecimal
{
    private static readonly GeneralComplexNumberDecimal I = new GeneralComplexNumberDecimal(0.0M, 1.0M);
    private static readonly GeneralComplexNumberDecimal I2 = new GeneralComplexNumberDecimal(0.0M, 0.5M);

    internal GeneralComplexNumber<decimal> ComplexNumber { get; }

    /// <summary>
    /// Creates a new <see cref="GeneralComplexNumberDecimal"/> with specific values.
    /// </summary>
    /// <param name="real">The real value.</param>
    /// <param name="imaginary">The imaginary value.</param>
    public GeneralComplexNumberDecimal(decimal real, decimal imaginary)
    {
        ComplexNumber = new GeneralComplexNumber<decimal>(real, imaginary);
    }

    /// <summary>
    /// Creates a new <see cref="GeneralComplexNumberDecimal"/>.
    /// This will be a copy of the source number.
    /// </summary>
    /// <param name="complexNumber">The complex number to copy from.</param>
    public GeneralComplexNumberDecimal(GeneralComplexNumberDecimal complexNumber)
    {
        ComplexNumber = new GeneralComplexNumber<decimal>(complexNumber.ComplexNumber);
    }

    internal GeneralComplexNumberDecimal(GeneralComplexNumber<decimal> complexNumber)
    {
        ComplexNumber = complexNumber;
    }

    /// <summary>
    /// Returns a human-readable representation of this complex number.
    /// </summary>
    /// <returns>The human-readable representation.</returns>
    public override string ToString() => ComplexNumber.ToString();

    /// <inheritdoc cref="GeneralComplexNumber{T}.GetHashCode()"/>
    public override int GetHashCode() => ComplexNumber.GetHashCode();

    /// <inheritdoc cref="GeneralComplexNumber{T}.Equals(object?)"/>
    public override bool Equals([NotNullWhen(true)] object? obj) => ComplexNumber.Equals((obj as GeneralComplexNumberDecimal?)?.ComplexNumber ?? obj);

    /// <summary>
    /// Casts a <see cref="GeneralComplexNumberDecimal"/> to a decimal.
    /// </summary>
    /// <param name="complexNumber">The <see cref="GeneralComplexNumberDecimal"/> to convert.</param>
    /// <exception cref="ArgumentException">Thrown if the imaginary part is non zero.</exception>
    public static implicit operator decimal(GeneralComplexNumberDecimal complexNumber) =>
        complexNumber.Imaginary != 0M ? throw new ArgumentException("Imaginary part is non-zero.") : complexNumber.Real;

    /// <summary>
    /// Casts a decimal to a <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <param name="real">The <see cref="decimal"/> to convert.</param>
    public static explicit operator GeneralComplexNumberDecimal(decimal real) =>
        new GeneralComplexNumberDecimal(real, 0M);

    /// <summary>
    /// Indicates whether a scalar and a <see cref="GeneralComplexNumberDecimal"/> are equal.
    /// </summary>
    /// <param name="left">A <see cref="decimal"/>.</param>
    /// <param name="right">A <see cref="GeneralComplexNumberDecimal"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator ==(decimal left, GeneralComplexNumberDecimal? right) => left.Equals(right);

    /// <summary>
    /// Indicates whether a <see cref="GeneralComplexNumberDecimal"/> decimal are equal.
    /// </summary>
    /// <param name="left">A <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="right">A <see cref="decimal"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator ==(GeneralComplexNumberDecimal left, decimal? right) => left.Equals(right);

    /// <summary>
    /// Indicates whether two <see cref="GeneralComplexNumberDecimal"/>s are equal.
    /// </summary>
    /// <param name="left">A <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="right">A <see cref="GeneralComplexNumberDecimal"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator ==(GeneralComplexNumberDecimal left, GeneralComplexNumberDecimal? right) => left.Equals(right);

    /// <summary>
    /// Indicates whether a scalar and a <see cref="GeneralComplexNumberDecimal"/> are not equal.
    /// </summary>
    /// <param name="left">A <see cref="decimal"/>.</param>
    /// <param name="right">A <see cref="GeneralComplexNumberDecimal"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator !=(decimal left, GeneralComplexNumberDecimal? right) => !(left == right);

    /// <summary>
    /// Indicates whether two <see cref="GeneralComplexNumberDecimal"/> and a scalar are not equal.
    /// </summary>
    /// <param name="left">A <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="right">A <see cref="decimal"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator !=(GeneralComplexNumberDecimal left, decimal? right) => !(left == right);

    /// <summary>
    /// Indicates whether two <see cref="GeneralComplexNumberDecimal"/>s are not equal.
    /// </summary>
    /// <param name="left">A <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="right">A <see cref="GeneralComplexNumberDecimal"/> to compare.</param>
    /// <returns>True if both instances are not equal.</returns>
    public static bool operator !=(GeneralComplexNumberDecimal left, GeneralComplexNumberDecimal? right) => !(left == right);

    /// <summary>
    /// Gets the real part of the number.
    /// </summary>
    public decimal Real => ComplexNumber.Real;

    /// <summary>
    /// Gets the imaginary part of the number.
    /// </summary>
    public decimal Imaginary => ComplexNumber.Imaginary;

    /// <summary>
    /// Returns the complex conjugate as a new <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <returns>The complex conjugate as a <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal ComplexConjugate() =>
        new(Real, -Imaginary);

    /// <summary>
    /// Returns the absolute value.
    /// </summary>
    /// <returns>The absolute value.</returns>
    public decimal Abs() =>
        (Real.Sqr() + Imaginary.Sqr()).Sqrt();

    /// <summary>
    /// Returns the argument value.
    /// </summary>
    /// <returns>The argument value.</returns>
    public decimal Argument() =>
        Imaginary.Atan2(Real);

    /// <summary>
    /// Returns the inverse value.
    /// </summary>
    /// <returns>The inverse value.</returns>
    public GeneralComplexNumberDecimal Inverse()
    {
        var divisor = Real.Sqr() + Imaginary.Sqr();
        var real = Real / divisor;
        var imaginary = -(Imaginary / divisor);
        return new(real, imaginary);
    }

    /// <summary>
    /// Adds a scalar value to a <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <param name="c">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="decimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the sum of the two operands.</returns>
    public static GeneralComplexNumberDecimal operator +(GeneralComplexNumberDecimal c, decimal scalar) =>
        new(c.Real + scalar, c.Imaginary);

    /// <summary>
    /// Adds a scalar value to a <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="decimal"/>.</param>
    /// <param name="c">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the sum of the two operands.</returns>
    public static GeneralComplexNumberDecimal operator +(decimal scalar, GeneralComplexNumberDecimal c) =>
        new(scalar + c.Real, c.Imaginary);

    /// <summary>
    /// Adds two <see cref="GeneralComplexNumberDecimal"/>s together.
    /// </summary>
    /// <param name="c1">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="c2">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the sum of the two operands.</returns>
    public static GeneralComplexNumberDecimal operator +(GeneralComplexNumberDecimal c1, GeneralComplexNumberDecimal c2) =>
        new(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);

    /// <summary>
    /// Generates a new <see cref="GeneralComplexNumberDecimal"/> from an existing one.
    /// </summary>
    /// <param name="c">The <see cref="GeneralComplexNumberDecimal"/> to copy from.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> with the values of the operand.</returns>
    public static GeneralComplexNumberDecimal operator +(GeneralComplexNumberDecimal c) =>
        new(c.ComplexNumber);

    /// <summary>
    /// Subtracts a scalar value from a <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <param name="c">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="decimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the of the left hand operand minus the right hand value.</returns>
    public static GeneralComplexNumberDecimal operator -(GeneralComplexNumberDecimal c, decimal scalar) =>
        new(c.Real - scalar, c.Imaginary);

    /// <summary>
    /// Subtracts a <see cref="GeneralComplexNumberDecimal"/> from a scalar.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="decimal"/>.</param>
    /// <param name="c">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the of the left hand operand minus the right hand value.</returns>
    public static GeneralComplexNumberDecimal operator -(decimal scalar, GeneralComplexNumberDecimal c) =>
        new(scalar - c.Real, 0M - c.Imaginary);

    /// <summary>
    /// Subtracts one <see cref="GeneralComplexNumberDecimal"/> from another.
    /// </summary>
    /// <param name="c1">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="c2">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the left hand operand minus the right hand operand.</returns>
    public static GeneralComplexNumberDecimal operator -(GeneralComplexNumberDecimal c1, GeneralComplexNumberDecimal c2) =>
        new(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);

    /// <summary>
    /// Negates a <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <param name="c">The <see cref="GeneralComplexNumberDecimal"/> to negate.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> which is the negated operand.</returns>
    public static GeneralComplexNumberDecimal operator -(GeneralComplexNumberDecimal c) =>
        new(-c.Real, -c.Imaginary);

    /// <summary>
    /// Multiplies a scalar value with a <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <param name="c">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="decimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralComplexNumberDecimal operator *(GeneralComplexNumberDecimal c, decimal scalar) =>
        new(c.Real * scalar, c.Imaginary * scalar);

    /// <summary>
    /// Multiplies a <see cref="GeneralComplexNumberDecimal"/> with a scalar.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="decimal"/>.</param>
    /// <param name="c">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralComplexNumberDecimal operator *(decimal scalar, GeneralComplexNumberDecimal c) =>
        new(scalar * c.Real, scalar * c.Imaginary);

    /// <summary>
    /// Multiplies one <see cref="GeneralComplexNumberDecimal"/> with another.
    /// </summary>
    /// <param name="c1">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="c2">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the left hand operand multiplied by the right hand operand.</returns>
    public static GeneralComplexNumberDecimal operator *(GeneralComplexNumberDecimal c1, GeneralComplexNumberDecimal c2) =>
        new(c1.Real * c2.Real - c1.Imaginary * c2.Imaginary, c1.Real * c2.Imaginary + c1.Imaginary * c2.Real);

    /// <summary>
    /// Divides a scalar value by a <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <param name="c">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="decimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralComplexNumberDecimal operator /(GeneralComplexNumberDecimal c, decimal scalar) =>
        new(c.Real / scalar, c.Imaginary / scalar);

    /// <summary>
    /// Divides a <see cref="GeneralComplexNumberDecimal"/> by a scalar.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="decimal"/>.</param>
    /// <param name="c">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralComplexNumberDecimal operator /(decimal scalar, GeneralComplexNumberDecimal c) =>
        new(scalar * c.Inverse());

    /// <summary>
    /// Divides one <see cref="GeneralComplexNumberDecimal"/> by another.
    /// </summary>
    /// <param name="c1">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="c2">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDecimal"/> of the left hand operand multiplied by the right hand operand.</returns>
    public static GeneralComplexNumberDecimal operator /(GeneralComplexNumberDecimal c1, GeneralComplexNumberDecimal c2) =>
        new(c1 * c2.Inverse());

    /// <summary>
    /// Squares a <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <returns>The square as a <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Sqr() =>
        this * this;

    /// <summary>
    /// Squares a <see cref="GeneralComplexNumberDecimal"/>.
    /// </summary>
    /// <returns>The square as a <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Sqrt()
    {
        var multiplier = Abs().Sqrt();
        var angle = 0.5M * Argument();
        var real = angle.Cos();
        var imaginary = angle.Sin();
        return new(multiplier * real, multiplier * imaginary);
    }

    /// <summary>
    /// Raises a <see cref="GeneralComplexNumberDecimal"/> by a power.
    /// </summary>
    /// <param name="power">The power to raise by.</param>
    /// <returns>The power as a <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Pow(decimal power)
    {
        var multiplier = Abs().Pow(power);
        var angle = power * Argument();
        var real = angle.Cos();
        var imaginary = angle.Sin();
        return new(multiplier * real, multiplier * imaginary);
    }

    /// <summary>
    /// Raises a <see cref="GeneralComplexNumberDecimal"/> by a power.
    /// </summary>
    /// <param name="power">The power to raise by.</param>
    /// <returns>The power as a <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Pow(GeneralComplexNumberDecimal power)
    {
        var factor = Real.Sqr() + Imaginary.Sqr();
        var atan = (Imaginary / Real).Atan();
        var multiplier = factor.Sqrt().Pow(power.Real) * (-power.Imaginary * atan).Exp();
        var angle = power.Imaginary * factor.Log() / 2M + power.Real * atan;
        var real = angle.Cos();
        var imaginary = angle.Sin();
        return new(multiplier * real, multiplier * imaginary);
    }

    /// <summary>
    /// Gets the sine of this complex angle (radians).
    /// </summary>
    /// <returns>The sine of the angle as a new <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Sin()
    {
        return new GeneralComplexNumberDecimal(
            Real.Sin() * Imaginary.Cosh(),
            Real.Cos() * Imaginary.Sinh());
    }

    /// <summary>
    /// Gets the cosine of this complex angle (radians).
    /// </summary>
    /// <returns>The cosine of the angle as a new <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Cos()
    {
        return new GeneralComplexNumberDecimal(
            Real.Cos() * Imaginary.Cosh(),
            -Real.Sin() * Imaginary.Sinh());
    }

    /// <summary>
    /// Gets the tangent of this complex angle (radians).
    /// </summary>
    /// <returns>The tangent of the angle as a new <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Tan()
    {
        return Sin() / Cos();
    }

    /// <summary>
    /// Gets the angle that this complex number is the sine of.
    /// </summary>
    /// <returns>The angle in radians.</returns>
    public GeneralComplexNumberDecimal Asin()
    {
        return ((1.0M - Sqr()).Sqrt() + (I * this)).Log() * -I;
    }

    /// <summary>
    /// Gets the angle that this complex number is the cosine of.
    /// </summary>
    /// <returns>The angle in radians.</returns>
    public GeneralComplexNumberDecimal Acos()
    {
        return new GeneralComplexNumberDecimal(Decimal.HalfPi, 0.0M) - Asin();
    }

    /// <summary>
    /// Gets the angle that this complex number is the tangent of.
    /// </summary>
    /// <returns>The angle in radians.</returns>
    public GeneralComplexNumberDecimal Atan()
    {
        var iz = I * this;
        return ((1.0M + iz) / (1.0M - iz)).Log() * -I2;
    }

    /// <summary>
    /// Gets the hyperbolic sine of this complex angle (radians).
    /// </summary>
    /// <returns>The hyperbolic sine of the angle as a new <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Sinh()
    {
        return new GeneralComplexNumberDecimal(
            Real.Sinh() * Imaginary.Cos(),
            Real.Cosh() * Imaginary.Sin());
    }

    /// <summary>
    /// Gets the hyperbolic cosine of this complex angle (radians).
    /// </summary>
    /// <returns>The hyperbolic cosine of the angle as a new <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Cosh()
    {
        return new GeneralComplexNumberDecimal(
            Real.Cosh() * Imaginary.Cos(),
            Real.Sinh() * Imaginary.Sin());
    }

    /// <summary>
    /// Gets the hyperbolic tangent of this complex angle (radians).
    /// </summary>
    /// <returns>The hyperbolic tangent of the angle as a new <see cref="GeneralComplexNumberDecimal"/>.</returns>
    public GeneralComplexNumberDecimal Tanh()
    {
        return new GeneralComplexNumberDecimal(Real.Tanh(), Imaginary.Tan()) /
               new GeneralComplexNumberDecimal(1.0M, Real.Tanh() * Imaginary.Tan());
    }

    /// <summary>
    /// Gets the principal complex angle (radians) that this value is the hyperbolic sine of.
    /// </summary>
    /// <returns>The principal complex angle (radians).</returns>
    public GeneralComplexNumberDecimal Asinh()
    {
        return (this + (Sqr() + 1.0M).Sqrt()).Log();
    }

    /// <summary>
    /// Gets the principal complex angle (radians) that this value is the hyperbolic cosine of.
    /// </summary>
    /// <returns>The principal complex angle (radians).</returns>
    public GeneralComplexNumberDecimal Acosh()
    {
        return (this + ((this + 1.0M).Sqrt() * (this - 1.0M).Sqrt())).Log();
    }

    /// <summary>
    /// Gets the principal complex angle (radians) that this value is the hyperbolic tangent of.
    /// </summary>
    /// <returns>The principal complex angle (radians).</returns>
    public GeneralComplexNumberDecimal Atanh()
    {
        return ((1.0M + this) / (1.0M - this)).Log() / 2.0M;
    }

    /// <summary>
    /// Returns the complex cube root of this complex number.
    /// </summary>
    /// <returns>The complex cube root.</returns>
    public GeneralComplexNumberDecimal Cbrt()
    {
        return (Log() / 3.0M).Exp();
    }

    /// <summary>
    /// Returns a complex number with the magnitudes of this but the signs of y.
    /// </summary>
    /// <param name="y">The complex number to take the signes from.</param>
    /// <returns>A complex number with the magnitudes of this but the signs of y.</returns>
    public GeneralComplexNumberDecimal CopySign(GeneralComplexNumberDecimal y)
    {
        return new GeneralComplexNumberDecimal(Real.CopySign(y.Real), Imaginary.CopySign(y.Imaginary));
    }

    /// <summary>
    /// Returns this * 2^n efficiently.
    /// </summary>
    /// <param name="n">The power to scale by.</param>
    /// <returns>this * 2^n</returns>
    /// <exception cref="OverflowException">Throw if n is above 96 or below -93.</exception>
    public GeneralComplexNumberDecimal ScaleB(int n)
    {
        if (n > 96 || n < -93) throw new OverflowException("Power of 2 outside decimal representation.");

        if (n == 0) return this;
        if (n == 1) return this * 2M;
        if (n == -1) return this * 0.5M;

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
        return this * (inverse ? 1M / multiplier : multiplier);
    }

    /// <summary>
    /// Gets the value of e to the power of this complex number.
    /// </summary>
    /// <returns>The complex value of e to the power of this.</returns>
    public GeneralComplexNumberDecimal Exp()
    {
        var estimate = new GeneralComplexNumberDecimal(1.0M, 0.0M);
        var prevEstimate = new GeneralComplexNumberDecimal(0.0M, 0.0M);
        var dVal = this;
        var factor = 1.0M;

        while (estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate += dVal;
            factor += 1.0M;
            try
            {
                dVal *= this / factor;
            }
            catch (Exception)
            {
                break;
            }
        }

        return estimate;
    }

    /// <summary>
    /// Gets the principal natural logarithm of this complex number.
    /// </summary>
    /// <returns>The principal natural logarithm.</returns>
    public GeneralComplexNumberDecimal Log()
    {
        return new GeneralComplexNumberDecimal(Abs().Log(), Argument());
    }

    /// <summary>
    /// Gets the principal logarithm of this complex number in a specific base.
    /// </summary>
    /// <param name="newBase">The base for the logarithm.</param>
    /// <returns>The principal logarithm in the base.</returns>
    public GeneralComplexNumberDecimal Log(decimal newBase)
    {
        return Log() / newBase.Logi();
    }

    /// <summary>
    /// Gets the principal logarithm of this complex number in a specific base.
    /// </summary>
    /// <param name="newBase">The base for the logarithm.</param>
    /// <returns>The principal logarithm in the base.</returns>
    public GeneralComplexNumberDecimal Log(GeneralComplexNumberDecimal newBase)
    {
        return Log() / newBase.Log();
    }
    // TODO: Log2, Log10,
    // TODO: Round, Round (digits), Round (algorithm), Round (digits, algorithm)
}
