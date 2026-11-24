using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Formula.Base;
using Wyrm.Math.TypeFunctions;

namespace Wyrm.Math.Formula;

/// <summary>
/// Quadratic formulas.
/// </summary>
public static class Quadratic
{
    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#178; + bx + c = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#178; term.</param>
    /// <param name="b">Coefficient of x term.</param>
    /// <param name="c">Constant.</param>
    /// <returns>The roots (one or two).</returns>
    public static ICollection<GeneralComplexNumberDecimal> Roots(decimal a, decimal b, decimal c) =>
        Quadratic<decimal, GeneralComplexNumberDecimal>.Roots(a, b, c, DecimalFunctions.TypeFunctions);

    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#178; + bx + c = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#178; term.</param>
    /// <param name="b">Coefficient of x term.</param>
    /// <param name="c">Constant.</param>
    /// <returns>The roots (one or two).</returns>
    public static ICollection<GeneralComplexNumberDouble> Roots(double a, double b, double c) =>
        Quadratic<double, GeneralComplexNumberDouble>.Roots(a, b, c, DoubleFunctions.TypeFunctions);

    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#178; + bx + c = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#178; term.</param>
    /// <param name="b">Coefficient of x term.</param>
    /// <param name="c">Constant.</param>
    /// <returns>The roots (one or two).</returns>
    public static ICollection<GeneralComplexNumberDecimal> Roots(GeneralComplexNumberDecimal a, GeneralComplexNumberDecimal b, GeneralComplexNumberDecimal c) =>
        Quadratic<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal>.Roots(a, b, c, GeneralComplexNumberDecimalFunctions.TypeFunctions);

    /// <summary>
    /// Calculate roots of a quadratic equation of form ax&#178; + bx + c = 0.
    /// </summary>
    /// <param name="a">Coefficient of x&#178; term.</param>
    /// <param name="b">Coefficient of x term.</param>
    /// <param name="c">Constant.</param>
    /// <returns>The roots (one or two).</returns>
    public static ICollection<GeneralComplexNumberDouble> Roots(GeneralComplexNumberDouble a, GeneralComplexNumberDouble b, GeneralComplexNumberDouble c) =>
        Quadratic<GeneralComplexNumberDouble, GeneralComplexNumberDouble>.Roots(a, b, c, GeneralComplexNumberDoubleFunctions.TypeFunctions);
}
