using System.Collections;

namespace TweetyProject.NET.Commons.util;

public abstract class SubsetIterator<T> : IEnumerator<ISet<T>>
{
    /** The set this iterator is iterating over. */
    protected readonly ISet<T> _set;

    /** Tracks the current position for the IEnumerator interface. */
    protected ISet<T> _current;

    protected List<T> _listSet;

    /** Creates a new subset iterator for the given set.
     * @param set some set.
     */
    protected SubsetIterator(ISet<T> set)
    {
        _set = set;
        _listSet = new List<T>(set);
        Reset(); // Prepare the iterator for use.
    }

    /**
     * Returns the set this iterator is iterating over. 
     * @return The set this iterator is iterating over. 
     */
    protected ISet<T> GetSet()
    {
        return _set;
    }

    /** 
     * C# version of the remove() method. 
     * In C#, typically, modifying the collection during enumeration is not supported, 
     * so this method will not be directly applicable as in Java.
     */
    public void Remove()
    {
        throw new NotSupportedException("This operation is not supported by this class.");
    }

    /**
     * C# equivalent for java.util.Iterator's hasNext(). 
     * In .NET, IEnumerator.MoveNext is used to move to the next element and check for the end of the collection.
     */
    public abstract bool MoveNext();

    /**
     * C# does not use a next() method in its iterator pattern. Instead, the current property is used to get the current element.
     */
    public virtual ISet<T> Current => _current;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        // Implement IDisposable if necessary, often used for manual resource management in C#.
        // This is optional and may be left empty if not needed.
    }

    public virtual void Reset()
    {
        // Reset the iterator to its initial state if necessary. This is part of the IEnumerator interface.
        // The exact implementation depends on how you manage the iteration state in your subclass.
    }
}