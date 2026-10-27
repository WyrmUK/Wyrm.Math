using System.Globalization;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;
using Wyrm.Math.UnitTests.TestHelpers;
using Xunit.Sdk;

[assembly: RegisterXunitSerializer(typeof(CultureInfoDataSerialiser), typeof(CultureInfo))]
[assembly: RegisterXunitSerializer(typeof(EnumerableEnumerableDecimalDataSerialiser), typeof(IEnumerable<IEnumerable<decimal>>))]
[assembly: RegisterXunitSerializer(typeof(EnumerableEnumerableDoubleDataSerialiser), typeof(IEnumerable<IEnumerable<double>>))]
[assembly: RegisterXunitSerializer(typeof(EnumerableEnumerableComplexDecimalDataSerialiser), typeof(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>>))]
[assembly: RegisterXunitSerializer(typeof(EnumerableEnumerableComplexDoubleDataSerialiser), typeof(IEnumerable<IEnumerable<GeneralComplexNumberDouble>>))]
[assembly: RegisterXunitSerializer(typeof(EnumerableEigenValueVectorPairDecimalSerialiser), typeof(IEnumerable<EigenValueVectorPair<GeneralComplexNumberDecimal, ComplexMatrixDecimal>>))]
[assembly: RegisterXunitSerializer(typeof(EnumerableEigenValueVectorPairDoubleSerialiser), typeof(IEnumerable<EigenValueVectorPair<GeneralComplexNumberDouble, ComplexMatrixDouble>>))]
