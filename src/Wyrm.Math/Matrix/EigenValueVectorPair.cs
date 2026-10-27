namespace Wyrm.Math.Matrix;

/// <summary>
/// An Eigenvalue/Eigenvector pair.
/// </summary>
/// <typeparam name="T1">The type of the eigenvalue.</typeparam>
/// <typeparam name="T2">The type of the eigenvector.</typeparam>
/// <param name="Value">The eigenvalue.</param>
/// <param name="Matrix">The eigenvector.</param>
public record EigenValueVectorPair<T1, T2>(T1 Value, T2 Matrix);
