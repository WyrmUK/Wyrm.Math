using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Formula.Base;
using Wyrm.Math.TypeFunctions;

namespace Wyrm.Math.Formula;

/// <summary>
/// Quadratic formulae.
/// </summary>
public static class Quadratic
{
    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#178; + bx + c = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#178; term.</param>
    /// <param name="b">Coefficient of x term.</param>
    /// <param name="c">Constant.</param>
    /// <returns>The roots.</returns>
    /// <exception cref="DivideByZeroException">Thrown if a and b are 0).</exception>
    public static ICollection<GeneralComplexNumberDecimal> Roots(decimal a, decimal b, decimal c) =>
        a == 0M
            ? [(GeneralComplexNumberDecimal)(-c / b)]
            : Quadratic<decimal, GeneralComplexNumberDecimal>.Roots(a, b, c, DecimalFunctions.TypeFunctions);

    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#178; + bx + c = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#178; term.</param>
    /// <param name="b">Coefficient of x term.</param>
    /// <param name="c">Constant.</param>
    /// <returns>The roots.</returns>
    /// <exception cref="DivideByZeroException">Thrown if a and b are 0).</exception>
    public static ICollection<GeneralComplexNumberDouble> Roots(double a, double b, double c) =>
        a == 0D
            ? [(GeneralComplexNumberDouble)(-c / b)]
            : Quadratic<double, GeneralComplexNumberDouble>.Roots(a, b, c, DoubleFunctions.TypeFunctions);

    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#178; + bx + c = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#178; term.</param>
    /// <param name="b">Coefficient of x term.</param>
    /// <param name="c">Constant.</param>
    /// <returns>The roots.</returns>
    /// <exception cref="DivideByZeroException">Thrown if a and b are 0).</exception>
    public static ICollection<GeneralComplexNumberDecimal> Roots(GeneralComplexNumberDecimal a, GeneralComplexNumberDecimal b, GeneralComplexNumberDecimal c) =>
        a.Real == 0M && a.Imaginary == 0M
            ? [-c / b]
            : Quadratic<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal>.Roots(a, b, c, GeneralComplexNumberDecimalFunctions.TypeFunctions);

    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#178; + bx + c = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#178; term.</param>
    /// <param name="b">Coefficient of x term.</param>
    /// <param name="c">Constant.</param>
    /// <returns>The roots.</returns>
    /// <exception cref="DivideByZeroException">Thrown if a and b are 0).</exception>
    public static ICollection<GeneralComplexNumberDouble> Roots(GeneralComplexNumberDouble a, GeneralComplexNumberDouble b, GeneralComplexNumberDouble c) =>
        a.Real == 0D && a.Imaginary == 0D
            ? [-c / b]
            : Quadratic<GeneralComplexNumberDouble, GeneralComplexNumberDouble>.Roots(a, b, c, GeneralComplexNumberDoubleFunctions.TypeFunctions);
}
