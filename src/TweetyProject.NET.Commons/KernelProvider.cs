namespace TweetyProject.NET.Commons;

/// <summary>
/// Classes implementing this interface are able to provide kernels (=minimal proofs).
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <T> the type of formulas </param>
public interface KernelProvider<T> where T : Formula
{
    /// <summary>
    /// Retrieves the set of kernels for the given formula
    /// from the given set of formulas. </summary>
    /// <param name="formulas"> a set of formulas. </param>
    /// <param name="formula"> a formula. </param>
    /// <returns> the collection of kernels </returns>
    ICollection<ICollection<T>> GetKernels(ICollection<T> formulas, T formula);
}