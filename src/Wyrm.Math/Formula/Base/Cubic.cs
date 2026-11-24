using Wyrm.Math.TypeFunctions;

namespace Wyrm.Math.Formula.Base;

internal class Cubic<T1, T2> where T1 : struct where T2 : struct
{
    public static ICollection<T2> Roots(T1 a, T1 b, T1 c, T1 d, ITypeFunctions<T1, T2> funcs)
    {
        var delta0 = funcs.Sub(funcs.Mul(b, b), funcs.Mul(funcs.Three(), a, c));
        var delta1 = funcs.Add(funcs.Sub(funcs.Mul(funcs.Two(), funcs.Mul(b, b, b)), funcs.Mul(funcs.Nine(), a, b, c)), funcs.Mul(funcs.TwentySeven(), a, a, d));

        var sqrtTerm = funcs.Sqrt(funcs.Sub(funcs.Mul(delta1, delta1), funcs.Mul(funcs.Four(), delta0, delta0, delta0)));
        var topTerm = funcs.Add(delta1, sqrtTerm);
        if (funcs.Eqz(topTerm))
        {
            topTerm = funcs.Sub(delta1, sqrtTerm);
        }

        var c0 = funcs.Cbrt(funcs.Div(topTerm, funcs.Two()));
        var c1 = funcs.Mul(funcs.Epsilon(), c0);
        var c2 = funcs.Mul(funcs.Epsilon(), c1);

        var d0 = funcs.Eqz(c0) ? c0 : funcs.Div(delta0, c0);
        var d1 = funcs.Eqz(c1) ? c1 : funcs.Div(delta0, c1);
        var d2 = funcs.Eqz(c2) ? c2 : funcs.Div(delta0, c2);

        var negThirdA = funcs.Div(funcs.NegOne(), funcs.Mul(funcs.Three(), a));
        return
            [
                funcs.Mul(negThirdA, funcs.Add(b, c0, d0)),
                funcs.Mul(negThirdA, funcs.Add(b, c1, d1)),
                funcs.Mul(negThirdA, funcs.Add(b, c2, d2))
            ];
    }
}
