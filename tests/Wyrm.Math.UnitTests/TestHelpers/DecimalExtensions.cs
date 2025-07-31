using Shouldly;

namespace Wyrm.Math.UnitTests.TestHelpers;

public static class DecimalExtensions
{
    public static void ShouldBeWithinTolerance(this decimal value, decimal expected, int lsds = 1)
    {
        var tolerance = lsds * 0.0000000000000000000000000001M;
        if (tolerance == 0M)
        {
            value.ShouldBe(expected);
        }
        else
        {
            if (expected.Abs() >= 10.0M)
            {
                tolerance *= (decimal)10.0.Pow(((double)expected.Abs()).Log10().Round(MidpointRounding.ToZero));
            }
            value.ShouldBeGreaterThanOrEqualTo(expected - tolerance);
            value.ShouldBeLessThanOrEqualTo(expected + tolerance);
        }
    }
}
