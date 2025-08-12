using System.Diagnostics.CodeAnalysis;
using Wyrm.Math.ComplexNumbers.Base;

namespace Wyrm.Math.ComplexNumbers;

/// <summary>
/// A general complex number struct for <see cref="double"/> values.
/// </summary>
public readonly struct GeneralComplexNumberDouble
{
    private static readonly GeneralComplexNumberDouble I = new GeneralComplexNumberDouble(0.0, 1.0);
    private static readonly GeneralComplexNumberDouble I2 = new GeneralComplexNumberDouble(0.0, 0.5);

    internal GeneralComplexNumber<double> ComplexNumber { get; }

    /// <summary>
    /// Creates a new <see cref="GeneralComplexNumberDouble"/> with specific values.
    /// </summary>
    /// <param name="real">The real value.</param>
    /// <param name="imaginary">The imaginary value.</param>
    public GeneralComplexNumberDouble(double real, double imaginary)
    {
        ComplexNumber = new GeneralComplexNumber<double>(real, imaginary);
    }

    /// <summary>
    /// Creates a new <see cref="GeneralComplexNumberDouble"/>.
    /// This will be a copy of the source number.
    /// </summary>
    /// <param name="complexNumber">The complex number to copy from.</param>
    public GeneralComplexNumberDouble(GeneralComplexNumberDouble complexNumber)
    {
        ComplexNumber = new GeneralComplexNumber<double>(complexNumber.ComplexNumber);
    }

    internal GeneralComplexNumberDouble(GeneralComplexNumber<double> complexNumber)
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
    public override bool Equals([NotNullWhen(true)] object? obj) => ComplexNumber.Equals((obj as GeneralComplexNumberDouble?)?.ComplexNumber ?? obj);

    /// <summary>
    /// Casts a <see cref="GeneralComplexNumberDouble"/> to a double.
    /// If there is an imaginary part then the result is NaN.
    /// </summary>
    /// <param name="complexNumber">The <see cref="GeneralComplexNumberDouble"/> to convert.</param>
    public static implicit operator double(GeneralComplexNumberDouble complexNumber) =>
        complexNumber.Imaginary != 0.0 ? double.NaN : complexNumber.Real;

    /// <summary>
    /// Casts a double to a <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <param name="real">The <see cref="double"/> to convert.</param>
    public static explicit operator GeneralComplexNumberDouble(double real) =>
        new GeneralComplexNumberDouble(real, 0.0);

    /// <summary>
    /// Indicates whether a scalar and a <see cref="GeneralComplexNumberDouble"/> are equal.
    /// </summary>
    /// <param name="left">A <see cref="double"/>.</param>
    /// <param name="right">A <see cref="GeneralComplexNumberDouble"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator ==(double left, GeneralComplexNumberDouble? right) => left.Equals(right);

    /// <summary>
    /// Indicates whether a <see cref="GeneralComplexNumberDouble"/> double are equal.
    /// </summary>
    /// <param name="left">A <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="right">A <see cref="double"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator ==(GeneralComplexNumberDouble left, double? right) => left.Equals(right);

    /// <summary>
    /// Indicates whether two <see cref="GeneralComplexNumberDouble"/>s are equal.
    /// </summary>
    /// <param name="left">A <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="right">A <see cref="GeneralComplexNumberDouble"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator ==(GeneralComplexNumberDouble left, GeneralComplexNumberDouble? right) => left.Equals(right);

    /// <summary>
    /// Indicates whether a scalar and a <see cref="GeneralComplexNumberDouble"/> are not equal.
    /// </summary>
    /// <param name="left">A <see cref="double"/>.</param>
    /// <param name="right">A <see cref="GeneralComplexNumberDouble"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator !=(double left, GeneralComplexNumberDouble? right) => !(left == right);

    /// <summary>
    /// Indicates whether two <see cref="GeneralComplexNumberDouble"/> and a scalar are not equal.
    /// </summary>
    /// <param name="left">A <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="right">A <see cref="double"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator !=(GeneralComplexNumberDouble left, double? right) => !(left == right);

    /// <summary>
    /// Indicates whether two <see cref="GeneralComplexNumberDouble"/>s are not equal.
    /// </summary>
    /// <param name="left">A <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="right">A <see cref="GeneralComplexNumberDouble"/> to compare.</param>
    /// <returns>True if both instances are not equal.</returns>
    public static bool operator !=(GeneralComplexNumberDouble left, GeneralComplexNumberDouble? right) => !(left == right);

    /// <summary>
    /// Gets the real part of the number.
    /// </summary>
    public double Real => ComplexNumber.Real;

    /// <summary>
    /// Gets the imaginary part of the number.
    /// </summary>
    public double Imaginary => ComplexNumber.Imaginary;

    /// <summary>
    /// Returns the complex conjugate as a new <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <returns>The complex conjugate as a <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble ComplexConjugate() =>
        new(Real, -Imaginary);

    /// <summary>
    /// Returns the absolute value.
    /// </summary>
    /// <returns>The absolute value.</returns>
    public double Abs() =>
        (Real.Sqr() + Imaginary.Sqr()).Sqrt();

    /// <summary>
    /// Returns the argument value.
    /// </summary>
    /// <returns>The argument value.</returns>
    public double Argument() =>
        Imaginary.Atan2(Real);

    /// <summary>
    /// Returns the inverse value.
    /// </summary>
    /// <returns>The inverse value.</returns>
    public GeneralComplexNumberDouble Inverse()
    {
        var divisor = Real.Sqr() + Imaginary.Sqr();
        var real = Real / divisor;
        var imaginary = -(Imaginary / divisor);
        return new(real, imaginary);
    }

    /// <summary>
    /// Adds a scalar value to a <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <param name="c">Left hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="scalar">Right hand <see cref="double"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the sum of the two operands.</returns>
    public static GeneralComplexNumberDouble operator +(GeneralComplexNumberDouble c, double scalar) =>
        new(c.Real + scalar, c.Imaginary);

    /// <summary>
    /// Adds a scalar value to a <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="double"/>.</param>
    /// <param name="c">Right hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the sum of the two operands.</returns>
    public static GeneralComplexNumberDouble operator +(double scalar, GeneralComplexNumberDouble c) =>
        new(scalar + c.Real, c.Imaginary);

    /// <summary>
    /// Adds two <see cref="GeneralComplexNumberDouble"/>s together.
    /// </summary>
    /// <param name="c1">Left hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="c2">Right hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the sum of the two operands.</returns>
    public static GeneralComplexNumberDouble operator +(GeneralComplexNumberDouble c1, GeneralComplexNumberDouble c2) =>
        new(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);

    /// <summary>
    /// Generates a new <see cref="GeneralComplexNumberDouble"/> from an existing one.
    /// </summary>
    /// <param name="c">The <see cref="GeneralComplexNumberDouble"/> to copy from.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> with the values of the operand.</returns>
    public static GeneralComplexNumberDouble operator +(GeneralComplexNumberDouble c) =>
        new(c.ComplexNumber);

    /// <summary>
    /// Subtracts a scalar value from a <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <param name="c">Left hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="scalar">Right hand <see cref="double"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the of the left hand operand minus the right hand value.</returns>
    public static GeneralComplexNumberDouble operator -(GeneralComplexNumberDouble c, double scalar) =>
        new(c.Real - scalar, c.Imaginary);

    /// <summary>
    /// Subtracts a <see cref="GeneralComplexNumberDouble"/> from a scalar.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="double"/>.</param>
    /// <param name="c">Right hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the of the left hand operand minus the right hand value.</returns>
    public static GeneralComplexNumberDouble operator -(double scalar, GeneralComplexNumberDouble c) =>
        new(scalar - c.Real, 0.0 - c.Imaginary);

    /// <summary>
    /// Subtracts one <see cref="GeneralComplexNumberDouble"/> from another.
    /// </summary>
    /// <param name="c1">Left hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="c2">Right hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the left hand operand minus the right hand operand.</returns>
    public static GeneralComplexNumberDouble operator -(GeneralComplexNumberDouble c1, GeneralComplexNumberDouble c2) =>
        new(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);

    /// <summary>
    /// Negates a <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <param name="c">The <see cref="GeneralComplexNumberDouble"/> to negate.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> which is the negated operand.</returns>
    public static GeneralComplexNumberDouble operator -(GeneralComplexNumberDouble c) =>
        new(-c.Real, -c.Imaginary);

    /// <summary>
    /// Multiplies a scalar value with a <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <param name="c">Left hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="scalar">Right hand <see cref="double"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralComplexNumberDouble operator *(GeneralComplexNumberDouble c, double scalar) =>
        new(c.Real * scalar, c.Imaginary * scalar);

    /// <summary>
    /// Multiplies a <see cref="GeneralComplexNumberDouble"/> with a scalar.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="double"/>.</param>
    /// <param name="c">Right hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralComplexNumberDouble operator *(double scalar, GeneralComplexNumberDouble c) =>
        new(scalar * c.Real, scalar * c.Imaginary);

    /// <summary>
    /// Multiplies one <see cref="GeneralComplexNumberDouble"/> with another.
    /// </summary>
    /// <param name="c1">Left hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="c2">Right hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the left hand operand multiplied by the right hand operand.</returns>
    public static GeneralComplexNumberDouble operator *(GeneralComplexNumberDouble c1, GeneralComplexNumberDouble c2) =>
        new(c1.Real * c2.Real - c1.Imaginary * c2.Imaginary, c1.Real * c2.Imaginary + c1.Imaginary * c2.Real);

    /// <summary>
    /// Divides a scalar value by a <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <param name="c">Left hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="scalar">Right hand <see cref="double"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralComplexNumberDouble operator /(GeneralComplexNumberDouble c, double scalar) =>
        new(c.Real / scalar, c.Imaginary / scalar);

    /// <summary>
    /// Divides a <see cref="GeneralComplexNumberDouble"/> by a scalar.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="double"/>.</param>
    /// <param name="c">Right hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralComplexNumberDouble operator /(double scalar, GeneralComplexNumberDouble c) =>
        new(scalar * c.Inverse());

    /// <summary>
    /// Divides one <see cref="GeneralComplexNumberDouble"/> by another.
    /// </summary>
    /// <param name="c1">Left hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <param name="c2">Right hand <see cref="GeneralComplexNumberDouble"/>.</param>
    /// <returns>A <see cref="GeneralComplexNumberDouble"/> of the left hand operand multiplied by the right hand operand.</returns>
    public static GeneralComplexNumberDouble operator /(GeneralComplexNumberDouble c1, GeneralComplexNumberDouble c2) =>
        new(c1 * c2.Inverse());

    /// <summary>
    /// Squares a <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <returns>The square as a <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Sqr() =>
        this * this;

    /// <summary>
    /// Squares a <see cref="GeneralComplexNumberDouble"/>.
    /// </summary>
    /// <returns>The square as a <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Sqrt()
    {
        var multiplier = Abs().Sqrt();
        var angle = 0.5 * Argument();
        var real = angle.Cos();
        var imaginary = angle.Sin();
        return new(multiplier * real, multiplier * imaginary);
    }

    /// <summary>
    /// Raises a <see cref="GeneralComplexNumberDouble"/> by a power.
    /// </summary>
    /// <param name="power">The power to raise by.</param>
    /// <returns>The power as a <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Pow(double power)
    {
        var multiplier = Abs().Pow(power);
        var angle = power * Argument();
        var real = angle.Cos();
        var imaginary = angle.Sin();
        return new(multiplier * real, multiplier * imaginary);
    }

    /// <summary>
    /// Raises a <see cref="GeneralComplexNumberDouble"/> by a power.
    /// </summary>
    /// <param name="power">The power to raise by.</param>
    /// <returns>The power as a <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Pow(GeneralComplexNumberDouble power)
    {
        var factor = Real.Sqr() + Imaginary.Sqr();
        var atan = (Imaginary / Real).Atan();
        var multiplier = factor.Sqrt().Pow(power.Real) * (-power.Imaginary * atan).Exp();
        var angle = power.Imaginary * factor.Log() / 2.0 + power.Real * atan;
        var real = angle.Cos();
        var imaginary = angle.Sin();
        return new(multiplier * real, multiplier * imaginary);
    }

    /// <summary>
    /// Gets the sine of this complex angle (radians).
    /// </summary>
    /// <returns>The sine of the angle as a new <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Sin()
    {
        return new GeneralComplexNumberDouble(
            Real.Sin() * Imaginary.Cosh(),
            Real.Cos() * Imaginary.Sinh());
    }

    /// <summary>
    /// Gets the cosine of this complex angle (radians).
    /// </summary>
    /// <returns>The cosine of the angle as a new <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Cos()
    {
        return new GeneralComplexNumberDouble(
            Real.Cos() * Imaginary.Cosh(),
            -Real.Sin() * Imaginary.Sinh());
    }

    /// <summary>
    /// Gets the tangent of this complex angle (radians).
    /// </summary>
    /// <returns>The tangent of the angle as a new <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Tan()
    {
        return Sin() / Cos();
    }

    /// <summary>
    /// Gets the angle that this complex number is the sine of.
    /// </summary>
    /// <returns>The angle in radians.</returns>
    public GeneralComplexNumberDouble Asin()
    {
        return ((1.0 - Sqr()).Sqrt() + (I * this)).Log() * -I;
    }

    /// <summary>
    /// Gets the angle that this complex number is the cosine of.
    /// </summary>
    /// <returns>The angle in radians.</returns>
    public GeneralComplexNumberDouble Acos()
    {
        return new GeneralComplexNumberDouble(double.Pi / 2.0, 0.0) - Asin();
    }

    /// <summary>
    /// Gets the angle that this complex number is the tangent of.
    /// </summary>
    /// <returns>The angle in radians.</returns>
    public GeneralComplexNumberDouble Atan()
    {
        var iz = I * this;
        return ((1.0 + iz) / (1.0 - iz)).Log() * -I2;
    }

    /// <summary>
    /// Gets the hyperbolic sine of this complex angle (radians).
    /// </summary>
    /// <returns>The hyperbolic sine of the angle as a new <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Sinh()
    {
        return new GeneralComplexNumberDouble(
            Real.Sinh() * Imaginary.Cos(),
            Real.Cosh() * Imaginary.Sin());
    }

    /// <summary>
    /// Gets the hyperbolic cosine of this complex angle (radians).
    /// </summary>
    /// <returns>The hyperbolic cosine of the angle as a new <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Cosh()
    {
        return new GeneralComplexNumberDouble(
            Real.Cosh() * Imaginary.Cos(),
            Real.Sinh() * Imaginary.Sin());
    }

    /// <summary>
    /// Gets the hyperbolic tangent of this complex angle (radians).
    /// </summary>
    /// <returns>The hyperbolic tangent of the angle as a new <see cref="GeneralComplexNumberDouble"/>.</returns>
    public GeneralComplexNumberDouble Tanh()
    {
        return new GeneralComplexNumberDouble(Real.Tanh(), Imaginary.Tan()) /
               new GeneralComplexNumberDouble(1.0, Real.Tanh() * Imaginary.Tan());
    }

    /// <summary>
    /// Gets the principal complex angle (radians) that this value is the hyperbolic sine of.
    /// </summary>
    /// <returns>The principal complex angle (radians).</returns>
    public GeneralComplexNumberDouble Asinh()
    {
        return (this + (Sqr() + 1.0).Sqrt()).Log();
    }

    /// <summary>
    /// Gets the principal complex angle (radians) that this value is the hyperbolic cosine of.
    /// </summary>
    /// <returns>The principal complex angle (radians).</returns>
    public GeneralComplexNumberDouble Acosh()
    {
        return (this + ((this + 1.0).Sqrt() * (this - 1.0).Sqrt())).Log();
    }

    /// <summary>
    /// Gets the principal complex angle (radians) that this value is the hyperbolic tangent of.
    /// </summary>
    /// <returns>The principal complex angle (radians).</returns>
    public GeneralComplexNumberDouble Atanh()
    {
        return ((1.0 + this) / (1.0 - this)).Log() / 2.0;
    }

    /// <summary>
    /// Returns the complex cube root of this complex number.
    /// </summary>
    /// <returns>The complex cube root.</returns>
    public GeneralComplexNumberDouble Cbrt()
    {
        return (Log() / 3.0).Exp();
    }

    /// <summary>
    /// Returns a complex number with the magnitudes of this but the signs of y.
    /// </summary>
    /// <param name="y">The complex number to take the signes from.</param>
    /// <returns>A complex number with the magnitudes of this but the signs of y.</returns>
    public GeneralComplexNumberDouble CopySign(GeneralComplexNumberDouble y)
    {
        return new GeneralComplexNumberDouble(Real.CopySign(y.Real), Imaginary.CopySign(y.Imaginary));
    }

    /// <summary>
    /// Returns this * 2^n efficiently.
    /// </summary>
    /// <param name="n">The power to scale by.</param>
    /// <returns>this * 2^n</returns>
    public GeneralComplexNumberDouble ScaleB(int n)
    {
        return new GeneralComplexNumberDouble(Real.ScaleB(n), Imaginary.ScaleB(n));
    }

    /// <summary>
    /// Gets the value of e to the power of this complex number.
    /// </summary>
    /// <returns>The complex value of e to the power of this.</returns>
    public GeneralComplexNumberDouble Exp()
    {
        var estimate = new GeneralComplexNumberDouble(1.0, 0.0);
        var prevEstimate = new GeneralComplexNumberDouble(0.0, 0.0);
        var dVal = this;
        var factor = 1.0;

        while (estimate != prevEstimate)
        {
            prevEstimate = estimate;
            estimate += dVal;
            factor += 1.0;
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
    public GeneralComplexNumberDouble Log()
    {
        return new GeneralComplexNumberDouble(Abs().Log(), Argument());
    }

    /// <summary>
    /// Gets the principal logarithm of this complex number in a specific base.
    /// </summary>
    /// <param name="newBase">The base for the logarithm.</param>
    /// <returns>The principal logarithm in the base.</returns>
    public GeneralComplexNumberDouble Log(double newBase)
    {
        return Log() / newBase.Logi();
    }

    /// <summary>
    /// Gets the principal logarithm of this complex number in a specific base.
    /// </summary>
    /// <param name="newBase">The base for the logarithm.</param>
    /// <returns>The principal logarithm in the base.</returns>
    public GeneralComplexNumberDouble Log(GeneralComplexNumberDouble newBase)
    {
        return Log() / newBase.Log();
    }

    /// <summary>
    /// Gets the principal logarithm of this complex number in base2.
    /// </summary>
    /// <returns>The principal base 2 logarithm.</returns>
    public GeneralComplexNumberDouble Log2()
    {
        return Log() / Double.Log2;
    }

    /// <summary>
    /// Gets the principal logarithm of this complex number in base 10.
    /// </summary>
    /// <returns>The principal base 10 logarithm.</returns>
    public GeneralComplexNumberDouble Log10()
    {
        return Log() / Double.Log10;
    }

    // TODO: Round, Round (digits), Round (algorithm), Round (digits, algorithm)
}
