# Wyrm.Math
A collection of mathematical methods and algorithms.
## Wyrm.Math.Matrix
Provides matrix arithmetic.
### GeneralMatrixDouble
This class allows matrices with double values to be created, added, subtracted, and multiplied.
### GeneralMatrixDecimal
This class allows matrices with decimal values to be created, added, subtracted, and multiplied.
### Operations
#### Transpose
{Mv} = {M}'

Swaps the values of the matrix around the diagonal, creating a new matrix.
#### Trace
V = tr(M)

Calculates the trace of a matrix.

The matrix must be square.
#### Determinant
V = |M|

Calculates the determinant of a matrix using Gaussian Elimination.

The matrix must be square.
#### Rank
V = rank({M})

Calculates the rank of a matrix using Gaussian Elimination.
#### Nullity
V = nullity({M})

Calculates the nullity of a matrix using Gaussian Elimination.

This is the number of columns minus the rank.
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
