using System.Collections.ObjectModel;
using TweetyProject.NET.Commons.Helpers;

namespace TweetyProject.NET.Commons;


public abstract class BeliefSet<T, S> : Collection<T>, BeliefBase
    where T : Formula
    where S : Signature
{
    public abstract Signature MinimalSignature { get; }

    /// <summary>
    /// Flag that determines whether <seealso cref="java.lang.Object.equals(Object)"/> checks 
    /// only for equality of the beliefs in the belief sets 
    /// or whether it also checks for equality of the signatures attached to the belief sets.
    /// </summary>
    public const bool EQUALS_USES_SIGNATURE = false;

    /// <summary>
    /// The set of formulas of this belief base.
    /// </summary>
    protected ISet<T> _formulas;

    /// <summary>
    /// The signature of this belief base. It is always larger than or equal to 
    /// <seealso cref="BeliefSet{T,S}.getMinimalSignature()"/> 
    /// (the signature of the language of <seealso cref="BeliefSet{T,S}.formulas"/>).
    /// </summary>
    protected S _signature;

    /// <summary>
    /// Creates a new (empty) belief set.
    /// </summary>
    protected BeliefSet() : this(new HashSet<T>())
    {
        _signature = InstantiateSignature();
    }

    /// <summary>
    /// Creates a new belief set with the given collection of
    /// formulae. </summary>
    /// <param name="c"> a collection of formulae. </param>
    protected BeliefSet(ICollection<T> c)
    {
        _signature = InstantiateSignature();
        _formulas = InstantiateSet();

        AddAll(c);
    }

    /// <summary>
    /// Creates a new belief set with the given type of signature. </summary>
    /// <param name="sig"> a signature </param>
    protected BeliefSet(S sig)
    {
        _signature = InstantiateSignature(sig);
        _formulas = InstantiateSet();
    }

    /// <summary>
    /// Instantiates the set which is used as data holder for the belief set.
    /// Subclasses might override this method if the do not want to use HashSet
    /// as container implementation </summary>
    /// <returns> an new set </returns>
    protected virtual ISet<T> InstantiateSet()
    {
        return new HashSet<T>();
    }

    /// <summary>
    /// Instantiates the signature which is attached to the belief base. </summary>
    /// <returns> the signature of this belief base </returns>
    protected abstract S InstantiateSignature();

    /// <summary>
    /// Instantiates the signature which is attached to the belief base 
    /// as an instance of the class of the given signature. </summary>
    /// <param name="sig"> some signature </param>
    /// <returns> the signature which is attached to the belief base 
    /// as an instance of the class of the given signature. </returns>
    private S? InstantiateSignature(S sig)
    {
        try
        {
            return (S)Activator.CreateInstance(sig.GetType());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);

            return default(S);
        }
    }

    /// <summary>
    /// Returns a copy of the signature that is attached to his belief base (it is
    /// always equal to or larger than <seealso cref="BeliefBase.getMinimalSignature()"/>). </summary>
    /// <returns> the signature of this knowledge base. </returns>
// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @SuppressWarnings("unchecked") public S getSignature()
    public virtual S Signature
    {
        get => (S)_signature.Clone();
        set
        {
            if (_signature.IsSubSignature(value))
            {
                _signature = (S)value.Clone();
            }
            else
            {
                throw new ArgumentException(
                    "The given signature is smaller than the signature of the belief base's formulas.");
            }
        }
    }


    /* (non-Javadoc)
     * @see java.util.Collection#add(java.lang.Object)
     */
    public bool Add(T F)
    {
        if (_formulas.Add(F))
        {
            _signature.Add(F);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds the specified elements to the end of this collection (optional operation). </summary>
    /// <param name="formulas"> to be appended to collection </param>
    /// <returns> true if all elements were added, false otherwise </returns
    public virtual bool Add(params T[] formulas)
    {
        bool Result = true;
        foreach (T F in formulas)
        {
            bool Sub = Add(F);
            Result = Result && Sub;
        }

        return Result;
    }


    public bool AddAll(ICollection<T> C)
    {
        bool Result = true;
        foreach (T t in C)
        {
            bool Sub = Add(t);
            Result = Result && Sub;
        }

        return Result;
    }


    public virtual void Clear()
    {
        _formulas.Clear();
    }


    public virtual bool Contains(T o)
    {
        return _formulas.Contains(o);
    }

    /* (non-Javadoc)
     * @see java.util.Collection#containsAll(java.util.Collection)
     */
    public bool ContainsAll(ICollection<T> c)
    {
        return _formulas.ContainsAll(c);
    }

    /* (non-Javadoc)
     * @see java.lang.Object#hashCode()
     */
    public override int GetHashCode()
    {
        const int prime = 31;
        var result = 1;

        result = prime * result + (_formulas == null ? 0 : _formulas.GetHashCode());

        return result;
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }

        if (obj == null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        BeliefSet<T, S> Other = (BeliefSet<T, S>)obj;

        if (_formulas == null)
        {
            if (Other._formulas != null)
            {
                return false;
            }
        }
        else if (!_formulas.SetEquals(Other._formulas))
        {
            return false;
        }

        if (EQUALS_USES_SIGNATURE)
        {
            if (_formulas == null)
            {
                if (Other._formulas != null)
                {
                    return false;
                }
            }
            else if (!_formulas.Equals(Other._formulas))
            {
                return false;
            }
        }

        return true;
    }


    public bool Empty => _formulas.Count == 0;

    public virtual IEnumerator<T> GetEnumerator()
    {
        return _formulas.GetEnumerator();
    }

    public virtual IEnumerator<T> Iterator()
    {
        return GetEnumerator();
    }

    /* (non-Javadoc)
     * @see java.util.Collection#remove(java.lang.Object)
     */
    public virtual bool Remove(T o)
    {
        return _formulas.Remove(o);
    }

    /* (non-Javadoc)
     * @see java.util.Collection#removeAll(java.util.Collection)
     */
    public bool RemoveAll(ICollection<T> C)
    {
        bool Result = true;
        foreach (T t in C)
        {
            bool Sub = Remove(t);
            Result = Result && Sub;
        }

        return Result;
    }


    public bool RetainAll(ICollection<T> c)
    {
        bool Result = false;
        ICollection<T> NewFormulas = new HashSet<T>(_formulas);
        foreach (T t in this)
        {
            if (!c.Contains(t))
            {
                NewFormulas.Remove(t);
                Result = true;
            }
        }

        Clear();
        AddAll(NewFormulas);
        return Result;
    }


    public virtual int Count => _formulas.Count;

    public int Size()
    {
        return _formulas.Count();
    }



    /* (non-Javadoc)
     * @see java.util.Collection#toArray()
     */
    public T[] ToArray()
    {
        return _formulas.ToArray();
    }

    /* (non-Javadoc)
     * @see java.util.Collection#toArray(T[])
     */
    public T[] ToArray(T[] a)
    {
        var data = new List<T>();

        data.AddRange(_formulas);
        data.AddRange(a);

        return data.ToArray();
    }

    public override string ToString()
    {
        return toString(false);
    }

    /// <summary>
    /// returns a string representation of this belief set </summary>
    /// <param name="showSignature"> whether to show the underlying signature </param>
    /// <returns> a string representation of this belief set </returns>
    public virtual string toString(bool ShowSignature)
    {
        string s = "{ ";

        foreach (var form in _formulas)
        {
            s += form;
        }


        foreach (var form in _formulas)
        {
            s += ", " + form;
        }

        s += " }";

        if (ShowSignature)
        {
            s += "[Signature: " + _signature + "]";
        }

        return s;
    }
}