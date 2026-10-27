using Wyrm.Math.ComplexNumbers.Base;

namespace Wyrm.Math.Matrix.Base;

internal interface ITypeFunctions<T> where T : struct
{
    T One();
    T Four();
    bool Eqz(T v);
    bool Nez(T v);
    bool Eq1(T v);
    bool Ne1(T v);
    T Neg(T v);
    T Add(T v1, T v2);
    T Sub(T v1, T v2);
    T Mul(T v1, T v2);
    T Div(T v1, T v2);
}

internal interface ITypeFunctions<T1, T2> : ITypeFunctions<T1> where T1 : struct where T2 : struct
{
    T2 Cast(T1 v);
    T2 Neg(T2 v);
    T2 Add(T1 v1, T2 v2);
    T2 HalfOf(T2 v);
    T2 Sqrt(T1 v);
}

internal interface ITypeFunctions<T1, T2, T3> : ITypeFunctions<T1, T2> where T1 : struct where T2 : struct where T3 : struct
{
    T3 ComplexMatrix(IEnumerable<IEnumerable<T2>> v);
}