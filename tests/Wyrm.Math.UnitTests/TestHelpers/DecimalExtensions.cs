using Shouldly;

namespace Wyrm.Math.UnitTests.TestHelpers;

public static class DecimalExtensions
{
    public static void ShouldBeWithinTolerance(this decimal value, decimal expected, decimal tolerance)
    {
        if (tolerance == 0M)
        {
            value.ShouldBe(expected);
        }
        else
        {
            value.ShouldBeGreaterThanOrEqualTo(expected - tolerance);
            value.ShouldBeLessThanOrEqualTo(expected + tolerance);
        }
    }
}
