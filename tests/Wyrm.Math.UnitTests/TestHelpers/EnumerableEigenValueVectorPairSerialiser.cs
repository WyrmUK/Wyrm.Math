using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;
using Xunit.Sdk;

namespace Wyrm.Math.UnitTests.TestHelpers;

public abstract class EnumerableEigenValueVectorPairSerialiser<T1, T2> : XunitSerializer<IEnumerable<EigenValueVectorPair<T1, T2>>> where T1 : struct where T2 : struct
{
    public override IEnumerable<EigenValueVectorPair<T1, T2>> Deserialize(Type type, string serializedValue)
    {
        return serializedValue[1..^1]
            .Split('|')
            .Select(p => p.Split('*'))
            .Select(p => new EigenValueVectorPair<T1, T2>(ParseT1(p[0]), ParseT2(p[1])))
            .ToList();
    }

    protected abstract T1 ParseT1(string serializedValue);
    protected abstract T2 ParseT2(string serializedValue);

    public override string Serialize(IEnumerable<EigenValueVectorPair<T1, T2>> value)
    {
        return $"[{string.Join("|", value.Select(v => $"{v.Value}*{v.Matrix}"))}]";
    }
}

public class EnumerableEigenValueVectorPairDoubleSerialiser : EnumerableEigenValueVectorPairSerialiser<GeneralComplexNumberDouble, ComplexMatrixDouble>
{
    protected override GeneralComplexNumberDouble ParseT1(string serializedValue) => GeneralComplexNumberDouble.Parse(serializedValue);
    protected override ComplexMatrixDouble ParseT2(string serializedValue) => ComplexMatrixDouble.Parse(serializedValue);
}

public class EnumerableEigenValueVectorPairDecimalSerialiser : EnumerableEigenValueVectorPairSerialiser<GeneralComplexNumberDecimal, ComplexMatrixDecimal>
{
    protected override GeneralComplexNumberDecimal ParseT1(string serializedValue) => GeneralComplexNumberDecimal.Parse(serializedValue);
    protected override ComplexMatrixDecimal ParseT2(string serializedValue) => ComplexMatrixDecimal.Parse(serializedValue);
}