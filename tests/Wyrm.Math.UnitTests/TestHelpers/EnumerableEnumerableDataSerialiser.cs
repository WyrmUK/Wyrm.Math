using Wyrm.Math.ComplexNumbers;
using Xunit.Sdk;

namespace Wyrm.Math.UnitTests.TestHelpers;

public abstract class EnumerableEnumerableDataSerialiser<T> : XunitSerializer<IEnumerable<IEnumerable<T>>> where T : struct
{
    public override IEnumerable<IEnumerable<T>> Deserialize(Type type, string serializedValue)
    {
        return serializedValue[2..^2]
            .Split("][")
            .Select(r => r
                .Split(",")
                .Select(v => Parse(v))
                .ToList())
            .ToList();
    }

    protected abstract T Parse(string v);

    public override string Serialize(IEnumerable<IEnumerable<T>> value)
    {
        return $"[{value.Select(r => $"[{string.Join(",", r.Select(v => v.ToString()))}]")}]";
    }
}

public class EnumerableEnumerableDecimalDataSerialiser : EnumerableEnumerableDataSerialiser<decimal>
{
    protected override decimal Parse(string v) => decimal.Parse(v);
}

public class EnumerableEnumerableDoubleDataSerialiser : EnumerableEnumerableDataSerialiser<double>
{
    protected override double Parse(string v) => double.Parse(v);
}

public class EnumerableEnumerableComplexDecimalDataSerialiser : EnumerableEnumerableDataSerialiser<GeneralComplexNumberDecimal>
{
    protected override GeneralComplexNumberDecimal Parse(string v) => GeneralComplexNumberDecimal.Parse(v);
}

public class EnumerableEnumerableComplexDoubleDataSerialiser : EnumerableEnumerableDataSerialiser<GeneralComplexNumberDouble>
{
    protected override GeneralComplexNumberDouble Parse(string v) => GeneralComplexNumberDouble.Parse(v);
}
