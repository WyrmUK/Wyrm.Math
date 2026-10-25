namespace Wyrm.Math.Matrix.Base;

internal interface ITypeFunctions<T> where T : struct
{
    T One();
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
