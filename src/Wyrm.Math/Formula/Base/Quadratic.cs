using Wyrm.Math.TypeFunctions;

namespace Wyrm.Math.Formula.Base;

internal static class Quadratic<T1, T2> where T1 : struct where T2 : struct
{
    public static ICollection<T2> Roots(T1 a, T1 b, T1 c, ITypeFunctions<T1, T2> funcs)
    {
        var gap = funcs.Sqrt(funcs.Sub(funcs.Mul(b, b), funcs.Mul(funcs.Mul(funcs.Four(), a), c)));
        var divisor = funcs.Mul(funcs.Two(), a);
        return
            [
                funcs.Div(funcs.Add(funcs.Neg(b), funcs.Neg(gap)), divisor),
                funcs.Div(funcs.Add(funcs.Neg(b), gap), divisor)
            ];
    }
}
