using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.TypeFunctions;

internal interface ITypeFunctions<T> where T : struct
{
    T One();
    T Two();
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
    T Mod(T v1, T v2);
}

internal interface IComplexTypeFunctions<T2> where T2 : struct
{
    bool Eqz(T2 v);
    bool Ltz(T2 v);
    T2 Neg(T2 v);
    T2 Add(T2 v1, T2 v2);
    T2 Mul(T2 v1, T2 v2);
    T2 HalfOf(T2 v);
    T2 Div(T2 v1, T2 v2);
    T2 Sqrt(T2 v);
    bool AboveTolerance(T2 v);
    GeneralMatrix<T2> Sub(GeneralMatrix<T2> m1, GeneralMatrix<T2> m2);
    GeneralMatrix<T2> Mul(GeneralMatrix<T2> m, T2 v);
    GeneralMatrix<T2> Mul(GeneralMatrix<T2> m1, GeneralMatrix<T2> m2);
    GeneralMatrix<T2> Div(GeneralMatrix<T2> m, T2 v);
    bool IsInteger(T2 v);
}

internal interface ITypeFunctions<T1, T2> : ITypeFunctions<T1>, IComplexTypeFunctions<T2> where T1 : struct where T2 : struct
{
    T2 Cast(T1 v);
    T1 Abs(T2 v);
    T2 Add(T1 v1, T2 v2);
    T2 Sub(T1 v1, T2 v2);
    T2 Div(T2 v1, T1 v2);
    T2 Sqrt(T1 v);
    GeneralMatrix<T2> CopyAs(GeneralMatrix<T1> m);
    GeneralMatrix<T2> Sub(GeneralMatrix<T1> m1, GeneralMatrix<T2> m2);
    GeneralMatrix<T2> Mul(T2 v, GeneralMatrix<T1> m);
    GeneralMatrix<T2> Div(GeneralMatrix<T2> m, T1 v);
}

internal interface IComplexTypeFunctions<T2, T3> : IComplexTypeFunctions<T2> where T2 : struct where T3 : struct
{
    T3 ComplexMatrix(GeneralMatrix<T2> m);
}

internal interface ITypeFunctions<T1, T2, T3> : ITypeFunctions<T1, T2>, IComplexTypeFunctions<T2, T3> where T1 : struct where T2 : struct where T3 : struct;
