namespace TweetyProject.NET.Commons;

public interface InterpretationIterator<TS, TB, TT> : IEnumerator<TT> where TS : Formula where TB : BeliefBase where TT : Interpretation<TB,TS>
{

    /* (non-Javadoc)
     * @see java.util.Iterator#hasNext()
     */
    bool HasNext();

    /* (non-Javadoc)
     * @see java.util.Iterator#next()
     */
    TT Next();

    /* (non-Javadoc)
     * @see java.util.Iterator#remove()
     */
    void Remove();

    /// <summary>
    /// Initializes a new reseted iterator. </summary>
    /// <returns> a reseted iterator. </returns>
    InterpretationIterator<TS, TB, TT> Reset();

    /// <summary>
    /// Initializes a new reseted iterator for the given signature. </summary>
    /// <param name="sig"> some signature. </param>
    /// <returns> a reseted iterator for the given signature. </returns>
    InterpretationIterator<TS, TB, TT> Reset(Signature sig);

    /// <summary>
    /// Initializes a new reseted iterator for the given signature derived from
    /// the given set of formulas. </summary>
    /// <param name="formulas"> a set of formulas. </param>
    /// <returns> a reseted iterator for the given signature derived from
    /// the given set of formulas.  </returns>
    InterpretationIterator<TS, TB, TT> Reset(ICollection<TS> formulas);
}