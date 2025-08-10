# Wyrm.Math
A collection of mathematical methods and algorithms.

## Wyrm.Math
Provides double and decimal extensions.
### Decimal
Decimal constants E, Pi, and Tau.
### Double extensions
Applies all the System.Math methods as extension methods and adds Sqr (square).
### Decimal extensions
Replicates the System.Math methods for decimals as extension methods.
Abs, Acos, Acosh, Asin, Asinh, Atan, Atanh, Atan2,
BitDecrement, BitIncrement,
Cbrt, Ceiling, CopySign, Clamp, Cos, Cosh,
Exp, Floor, FusedMultiplyAdd, IEEERemainder, ILogB,
Log, Log (to base), Log2, Log10,
Max, MaxMagnitude, Min, MinMagnitude, Pow,
ReciprocalEstimate, ReciprocalSqrtEstimate,
Round, Round (to digits), Round (with algorithm), Round (to digits with algorithm),
ScaleB, Sign, Sin, SinCos, Sinh, Sqr, Sqrt,
Tan, Tanh, Truncate.
### Complex Square Root
Z = N.Sqrti()

Calculates the square root of a number (either decimal or double) and returns the result as a complex number.

## Wyrm.Math.ComplexNumbers
Provides complex number arithmetic.
### GeneralComplexNumberDouble
This class allows complex numbers with double values to be created, added, subtracted, and multiplied.
### GeneralComplexNumberDecimal
This class allows complex numbers with decimal values to be created, added, subtracted, and multiplied.
### Operations
#### Cast
You can cast a complex number with no imaginary part to a scalar and cast a scalar to a complex number.
#### Complex Conjugate
Z' = Z.ComplexConjugate()

Calculates the complex conjugate of the complex number.
#### Abs
|Z| = Z.Abs()

Calculates the absolute value of the complex number.
#### Argument
P = Z.Argument()

Calculates the argument (or phase) of the complex number.
#### Inverse
1/Z = Z.Inverse()

Calculates the inverse of the complex number.
#### Add a scalar
Zv = Z + n

Zv = n + Z

Adds a scalar value to the complex number, creating a new complex number.
#### Add another complex number
Zv = Z1 + Z2

Adds one complex number to another, creating a new complex number.
#### Duplicate a complex number
Z = + Z

Duplicates a complex number.
#### Subtract a scalar
Zv = Z - n

Zv = n - Z

Subtracts a scalar value from a complex number or subtracts a complex number from a scalar, creating a new complex number.
#### Subtract another complex number
Zv = Z1 - Z2

Subtracts one complex number from another, creating a new complex number.
#### Negate a complex number
Zv = - Z

Negates the complex number, creating a new complex number.
#### Multiply by a scalar
Zv = Z * n

Zv = n * Z

Multiplies a scalar value with a complex number, creating a new complex number.
#### Multiply another complex number
Zv = Z1 * Z2

Multiplies one complex number with another, creating a new complex number.
#### Divide by a scalar
Zv = Z / n

Zv = n / Z

Divides a scalar value into a complex number or divides a complex number into a scalar, creating a new complex number.
#### Raise to a Power
Z^2 = Z.Sqr()

Z^0.5 = Z.Sqrt()

Z^n = Z.Pow(n)

Z^z = Z.Pow(z)

Raises a complex number by a power either real or complex.
#### Sine
Zv = Z.Sin()

Returns the sine of a complex number (radians).
#### Cosine
Zv = Z.Cos()

Returns the cosine of a complex number (radians).
#### Tangent
Zv = Z.Tan()

Returns the tangent of a complex number (radians).
#### Arsine
Zv = Z.Asin()

Returns the complex angle (radians) that has the sine.
#### Arcosine
Zv = Z.Acos()

Returns the complex angle (radians) that has the cosine.
#### Artangent
Zv = Z.Atan()

Returns the complex angle (radians) that has the tangent.
#### Hyperbolic Sine
Zv = Z.Sinh()

Returns the hyperbolic sine of a complex number (radians).
#### Hyperbolic Cosine
Zv = Z.Cosh()

Returns the hyperbolic cosine of a complex number (radians).
#### Hyperbolic Tangent
Zv = Z.Tanh()

Returns the hyperbolic tangent of a complex number (radians).
#### Principal Natural Logarithm
Zv = Z.Log()

Returns the principal natural logarithm of a complex number.

## Wyrm.Math.Matrix
Provides matrix arithmetic.
### GeneralMatrixDouble
This class allows matrices with double values to be created, added, subtracted, and multiplied.
### GeneralMatrixDecimal
This class allows matrices with decimal values to be created, added, subtracted, and multiplied.
### Operations
#### Transpose
{M}' = {M}.Transpose()

Swaps the values of the matrix around the diagonal, creating a new matrix.
#### Trace
V = {M}.Trace()

Calculates the trace of a matrix.

The matrix must be square.
#### Determinant
|{M}| = {M}.Determinant()

Calculates the determinant of a matrix using Gaussian Elimination.

The matrix must be square.
#### Rank
V = {M}.Rank()

Calculates the rank of a matrix using Gaussian Elimination.
#### Nullity
V = {M}.Nullity()

Calculates the nullity of a matrix using Gaussian Elimination.

This is the number of columns minus the rank.
#### Inverse
{M}^-1 = {M}.Inverse()

Calculates the inverse of a matrix using Gaussian Elimination.

The matrix must be square and invertible (non-zero determinant).
#### Add a scalar
{Mv} = {M} + n

{Mv} = n + {M}

Adds a scalar value to every element in a matrix, creating a new matrix.
#### Add another matrix
{Mv} = {M1} + {M2}

Adds one matrix to another, creating a new matrix.

The matrices must be of the same dimensions.
#### Duplicate a matrix
{M} = + {M}

Duplicates a matrix.
#### Subtract a scalar
{Mv} = {M} - n

{Mv} = n - {M}

Subtracts a scalar value from every element in a matrix or subtracts each value in a matrix from a scalar, creating a new matrix.
#### Subtract another matrix
{Mv} = {M1} - {M2}

Subtracts one matrix from another, creating a new matrix.

The matrices must be of the same dimensions.
#### Negate a matrix
{Mv} = - {M}

Negates every value in a matrix, creating a new matrix.
#### Multiply by a scalar
{Mv} = {M} * n

{Mv} = n * {M}

Multiplies a scalar value with every element in a matrix, creating a new matrix.
#### Multiply another matrix
{Mv} = {M1} * {M2}

Multiplies one matrix with another using the naive algorithm, creating a new matrix.

The number of columns of the first matrix must be the same as the number of rows of the second matrix.
#### Divide by a scalar
{Mv} = {M} / n

{Mv} = n / {M}

Divides a scalar value into every element in a matrix or divides each value in a matrix into a scalar, creating a new matrix.