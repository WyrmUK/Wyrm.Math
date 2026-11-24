using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Formula.Base;
using Wyrm.Math.TypeFunctions;

namespace Wyrm.Math.Formula;

/// <summary>
/// Cubic formulae.
/// </summary>
public static class Cubic
{
    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#179; + bx&#178; + cx + d = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#179; term.</param>
    /// <param name="b">Coefficient of x&#178; term.</param>
    /// <param name="c">Coefficient of x term.</param>
    /// <param name="d">Constant.</param>
    /// <returns>The roots.</returns>
    /// <exception cref="DivideByZeroException">Thrown if a, b, and c are 0).</exception>
    public static ICollection<GeneralComplexNumberDecimal> Roots(decimal a, decimal b, decimal c, decimal d) =>
        a == 0M
            ? Quadratic.Roots(b, c, d)
            : Cubic<decimal, GeneralComplexNumberDecimal>.Roots(a, b, c, d, DecimalFunctions.TypeFunctions);

    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#179; + bx&#178; + cx + d = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#179; term.</param>
    /// <param name="b">Coefficient of x&#178; term.</param>
    /// <param name="c">Coefficient of x term.</param>
    /// <param name="d">Constant.</param>
    /// <returns>The roots.</returns>
    /// <exception cref="DivideByZeroException">Thrown if a, b, and c are 0).</exception>
    public static ICollection<GeneralComplexNumberDouble> Roots(double a, double b, double c, double d) =>
        a == 0D
            ? Quadratic.Roots(b, c, d)
            : Cubic<double, GeneralComplexNumberDouble>.Roots(a, b, c, d, DoubleFunctions.TypeFunctions);

    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#179; + bx&#178; + cx + d = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#179; term.</param>
    /// <param name="b">Coefficient of x&#178; term.</param>
    /// <param name="c">Coefficient of x term.</param>
    /// <param name="d">Constant.</param>
    /// <returns>The roots.</returns>
    /// <exception cref="DivideByZeroException">Thrown if a, b, and c are 0).</exception>
    public static ICollection<GeneralComplexNumberDecimal> Roots(GeneralComplexNumberDecimal a, GeneralComplexNumberDecimal b, GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal d) =>
        a.Real == 0M && a.Imaginary == 0M
            ? Quadratic.Roots(b, c, d)
            : Cubic<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal>.Roots(a, b, c, d, GeneralComplexNumberDecimalFunctions.TypeFunctions);

    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#179; + bx&#178; + cx + d = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#179; term.</param>
    /// <param name="b">Coefficient of x&#178; term.</param>
    /// <param name="c">Coefficient of x term.</param>
    /// <param name="d">Constant.</param>
    /// <returns>The roots.</returns>
    /// <exception cref="DivideByZeroException">Thrown if a, b, and c are 0).</exception>
    public static ICollection<GeneralComplexNumberDouble> Roots(GeneralComplexNumberDouble a, GeneralComplexNumberDouble b, GeneralComplexNumberDouble c, GeneralComplexNumberDouble d) =>
        a.Real == 0D && a.Imaginary == 0D
            ? Quadratic.Roots(b, c, d)
            : Cubic<GeneralComplexNumberDouble, GeneralComplexNumberDouble>.Roots(a, b, c, d, GeneralComplexNumberDoubleFunctions.TypeFunctions);
}
