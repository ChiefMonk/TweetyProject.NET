namespace TweetyProject.NET.Commons.streams;

using Helpers;
using Formula = Formula;

/// <summary>
/// This class models a default stream on the formulas of a given collection.
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <S> The type of formulas </param>
public class DefaultFormulaStream<TS> : PeekEnumerator<TS>, IFormulaStream<TS> 
    where TS : Formula
{

    /// <summary>
    /// The collection of formulas. </summary>
    private readonly ICollection<TS> _formulas;

    /// <summary>
    /// Whether this stream is never-ending (formulas are repeated once through). </summary>
    private readonly bool _neverEnding;

    /// <summary>
    /// The actual iterator. </summary>
    private PeekEnumerator<TS> _it;

    /// <summary>
    /// Creates a new default stream with the given formulas that ends after all formulas
    /// have been streamed. </summary>
    /// <param name="formulas"> a collection of formulas. </param>
    public DefaultFormulaStream(ICollection<TS> formulas) : this(formulas, false)
    {
    }

    /// <summary>
    /// Creates a new default stream with the given formulas. </summary>
    /// <param name="formulas"> a collection of formulas. </param>
    /// <param name="neverEnding"> whether this stream is never-ending (formulas are repeated once through). </param>
    public DefaultFormulaStream(ICollection<TS> formulas, bool neverEnding)
        :base(formulas)
    {
        _formulas = formulas;
        _neverEnding = neverEnding;
        _it = (PeekEnumerator<TS>)formulas.GetEnumerator();
    }

    /* (non-Javadoc)
     * @see org.tweetyproject.streams.FormulaStream#hasNext()
     */
    public override bool HasNext()
    {
// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:

        return _it.HasNext() || _neverEnding;
    }

    /* (non-Javadoc)
     * @see org.tweetyproject.streams.FormulaStream#next()
     */
    public virtual TS Next()
    {
// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
        if (!_it.HasNext() && !_neverEnding)
        {
            throw new InvalidOperationException();
        }
// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
        if (!_it.HasNext())
        {
            _it = (PeekEnumerator<TS>)_formulas.GetEnumerator();
        }
// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
        if (!_it.HasNext())
        {
            throw new InvalidOperationException();
        }
        // JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:

        var next = _it.PeekNext();
        _it.MoveNext();

        return next; 
    }

    /* (non-Javadoc)
     * @see org.tweetyproject.streams.FormulaStream#remove()
     */
    public virtual void Remove()
    {
// JAVA TO C# CONVERTER TASK: .NET enumerators are read-only:
        //_it.Remove();
        throw new NotImplementedException();
    }
}