using System.Collections;


namespace TweetyProject.NET.Commons.Helpers;

/// <summary>
///   Extends the default <see cref="IEnumerator{T}" /> implementation to allow accessing the next
///   element of the collection without advancing the position of the enumerator
/// </summary>
/// <typeparam name="T">The type of objects to enumerate.</typeparam>
/// <seealso cref="System.Collections.Generic.IEnumerator{T}" />
public class PeekEnumerator<T> : IEnumerator<T>
{
    private const string WasResetExceptionMessage = "Enumeration has not started. Call MoveNext.";
    private IEnumerator<T> _enumerator;
    private Queue<T> _cache;
    private bool _wasReset;

    /// <summary>Initializes a new instance of the <see cref="PeekEnumerator{T}" /> struct.</summary>
    /// <param name="collection">
    ///   The collection for which a <see cref="PeekEnumerator{T}" /> is to be created.
    /// </param>
    public PeekEnumerator(IEnumerable<T> collection) : this(collection.GetEnumerator()) { }

    /// <summary>Initializes a new instance of the <see cref="PeekEnumerator{T}" /> class.</summary>
    /// <param name="enumerator">The enumerator.</param>
    /// <exception cref="ArgumentNullException">enumerator</exception>
    public PeekEnumerator(IEnumerator<T> enumerator)
    {
        _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        _cache = new Queue<T>();
        _wasReset = true;
    }

    /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
    /// <exception cref="InvalidOperationException">Enumeration has not started. Call MoveNext.</exception>
    public T Current
    {
        get
        {
            if (_wasReset) throw new InvalidOperationException(WasResetExceptionMessage);
            if (_cache.Count == 0) throw new InvalidOperationException("Enumeration already finished.");

            return _cache.Peek();
        }
    }

    /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
    object IEnumerator.Current => this.Current;

    /// <summary>Gets a value indicating whether this instance has next.</summary>
    /// <value><c>true</c> if this instance has next; otherwise, <c>false</c>.</value>
    public virtual bool HasNext() => _cache.Count >= 2 || TryFetchAndCache(2);


    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting
    ///   unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        _enumerator.Dispose();
    }

    /// <summary>Advances the enumerator to the next element of the collection.</summary>
    /// <returns>
    ///   <see langword="true" /> if the enumerator was successfully advanced to the next element;
    ///   <see langword="false" /> if the enumerator has passed the end of the collection.
    /// </returns>
    public virtual bool MoveNext()
    {
        _wasReset = false;
        ////// remove the previous Current value
        ////if (_cache.Count != 0) _cache.Dequeue();
        ////
        ////return _cache.Count > 0 || TryFetchAndCache(1);

        if (_cache.Any()) _cache.Dequeue();

        var success = TryFetchAndCache(1); //TODO: Conduct tests and remove unused variable

        return _cache.Any();
    }

    /// <summary>Gets the next item without advancing the position of the enumerator.</summary>
    /// <returns>The next item in the collection.</returns>
    /// <exception cref="InvalidOperationException">Cannot peek beyond end of enumeration.</exception>
    public T PeekNext()
    {
        if (_wasReset) throw new InvalidOperationException(WasResetExceptionMessage);

        if (_cache.Count < 2 && !TryFetchAndCache(2))
            throw new InvalidOperationException("Cannot peek beyond end of enumeration.");

        return _cache.ElementAt(1);
    }

    /// <summary>
    ///   Sets the enumerator to its initial position, which is before the first element in the collection.
    /// </summary>
    public void Reset()
    {
        _enumerator.Reset();
        _cache.Clear();
        _wasReset = true;
    }

    /// <summary>Tries to get the next element without advancing the position of the enumerator.</summary>
    /// <param name="result">
    ///   When this method returns, contains the next element in the collection if there are any
    ///   elements after the current position of the enumerator, or the default value for
    ///   <typeparamref name="T" /> if there are none.
    /// </param>
    /// <returns><c>true</c> if the next element was successfully retrieved; else <c>false</c>.</returns>
    public bool TryPeekNext(out T result)
    {
        try
        {
            // check prevent peeking if enumerator has been previously reset
            if (!_wasReset && this.TryFetchAndCache(2))
            {
                result = this.PeekNext();
                return true;
            }
        }
        catch (Exception ex) when (ex is InvalidOperationException ||   /* from TryFetchAndCache and PeekNext */
                                   ex is ArgumentOutOfRangeException    /* from Enumerable.ElementAt() */)
        {
            /* Empty exception handler to catch known exceptions. */
        }
        result = default(T);
        return false;
    }

    /// <summary>
    ///   Try to fetch the specified number of elements from the collection and cache the result.
    /// </summary>
    /// <param name="count">The number of elements to fetch and cache.</param>
    /// <returns>
    ///   <c>true</c> if at least <paramref name="count" /> elements were successfully fetched; else <c>false</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">count</exception>
    private bool TryFetchAndCache(int count)
    {
        if (count <= 0) throw new InvalidOperationException(nameof(count) + " must be greater than 0");

        while (_cache.Count < count && _enumerator.MoveNext())
            _cache.Enqueue(_enumerator.Current);

        return _cache.Count >= count;
    }
}