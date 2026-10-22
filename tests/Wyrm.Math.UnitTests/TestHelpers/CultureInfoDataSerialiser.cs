using System.Globalization;
using Xunit.Sdk;

namespace Wyrm.Math.UnitTests.TestHelpers;

public class CultureInfoDataSerialiser : XunitSerializer<CultureInfo>
{
    public override CultureInfo Deserialize(Type type, string serializedValue)
    {
        return new CultureInfo(serializedValue);
    }

    public override string Serialize(CultureInfo value)
    {
        return value.ToString();
    }
}
