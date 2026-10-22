using System.Globalization;
using Wyrm.Math.UnitTests.TestHelpers;
using Xunit.Sdk;

[assembly:RegisterXunitSerializer(typeof(CultureInfoDataSerialiser), typeof(CultureInfo))]
