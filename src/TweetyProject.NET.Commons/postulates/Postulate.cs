namespace TweetyProject.NET.Commons.postulates;

using Formula = Formula;

/// <summary>
/// Models a general (rationality) postulate, i.e. a property that
/// can be satisfied or violated by some approach. This class 
/// contains methods for checking whether an approach satisfies
/// certain instances wrt. this postulate.
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <TS> The type of formulas this postulate is about. </param>
public interface Postulate<TS> 
    where TS : Formula
{

    /// <summary>
    /// Checks whether the given kb represents a non-trivial
    /// instance for this postulate, i.e., whether assumptions
    /// of this postulates are satisfied (evaluating an approach
    /// on a non-applicable instance always succeeds).
    /// </summary>
    /// <param name="kb"> some knowledge base </param>
    /// <returns> true if the knowledge base is a non trivial instance
    ///  of this postulate. </returns>
    bool IsApplicable(ICollection<TS> kb);

    /// <summary>
    /// Checks whether this postulate is satisfied by the given approach
    /// <code>ev</code> wrt. the given instance <code>kb</code> (note
    /// that evaluating an approach on a non-applicable instance always succeeds). </summary>
    /// <param name="kb"> some knowledge base </param>
    /// <param name="ev"> some approach </param>
    /// <returns> true if the postulate is satisfied on the instance </returns>
    bool IsSatisfied(ICollection<TS> kb, PostulateEvaluatable<TS> ev);

    /// <summary>
    /// The textual name of the postulate </summary>
    /// <returns> a string </returns>
    string Name { get; }
}