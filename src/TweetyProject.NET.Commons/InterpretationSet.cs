
namespace TweetyProject.NET.Commons;

/// <summary>
/// This class models an interpretation that is a set of some formula and as such
/// implements the java.util.Collection interface. This class should be used as
/// ancestor for collection-based interpretations.
/// </summary>
/// @param <T> The actual class of the formulas stored in this interpretation </param>
/// @param <B> The class of belief bases this interpretation can handle </param>
/// @param <S> The actual class of formulas this interpretation can handle
/// 
/// @author Matthias Thimm </param>
public abstract class InterpretationSet<T, B, S> : AbstractInterpretation<B, S>, ICollection<T> 
    where T : Formula 
    where B : BeliefBase 
    where S : Formula
{
    public override abstract bool satisfies(B beliefBase);
    public override abstract bool satisfies(S formula);

    /// <summary>
    /// The set of formulas of this interpretation.
    /// </summary>
    private ISet<T> _Formulas;

    /// <summary>
    /// Creates a new empty interpretation.
    /// </summary>
    public InterpretationSet() : this(new HashSet<T>())
    {
    }

    /// <summary>
    /// Creates a new interpretation with the given collection of formulas.
    /// </summary>
    /// <param name="formulas"> a collection of formulas </param>
// JAVA TO C# CONVERTER TASK: Wildcard generics in method parameters are not converted:
// ORIGINAL LINE: public InterpretationSet(Collection<? extends T> formulas)
    public InterpretationSet(ICollection<T> Formulas)
    {
        this._Formulas = new HashSet<T>(Formulas);
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#add(java.lang.Object)
     */
    public override bool Add(T E)
    {
        return this._Formulas.Add(E);
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#addAll(java.util.Collection)
     */
    public override bool AddAll<T1>(ICollection<T1> C) where T1 : T
    {
        return this._Formulas.AddAll(C);
    }

    /// <summary>
    /// Adds the specified elements to the end of this collection (optional operation). </summary>
    /// <param name="elements"> to be appended to collection </param>
    /// <returns> true if all elements were added, false otherwise </returns>
// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @SuppressWarnings("unchecked") public boolean add(T... elements)
    public virtual bool Add(params T[] Elements)
    {
        bool Result = true;
        foreach (T F in Elements)
        {
            bool Sub = this.Add(F);
            Result = Result && Sub;
        }
        return Result;
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#clear()
     */
    public virtual void Clear()
    {
        this._Formulas.Clear();
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#contains(java.lang.Object)
     */
    public virtual bool Contains(object O)
    {
        return this._Formulas.Contains(O);
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#containsAll(java.util.Collection)
     */
    public override bool ContainsAll<T1>(ICollection<T1> C)
    {
        return this._Formulas.ContainsAll(C);
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#isEmpty()
     */
    public override bool Empty
    {
        get
        {
            return this._Formulas.Count == 0;
        }
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#iterator()
     */
    public virtual IEnumerator<T> GetEnumerator()
    {
        return this._Formulas.GetEnumerator();
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#remove(java.lang.Object)
     */
    public virtual bool Remove(object O)
    {
        return this._Formulas.Remove(O);
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#removeAll(java.util.Collection)
     */
    public override bool RemoveAll<T1>(ICollection<T1> C)
    {
        return this._Formulas.RemoveAll(C);
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#retainAll(java.util.Collection)
     */
    public override bool RetainAll<T1>(ICollection<T1> C)
    {
        return this._Formulas.RetainAll(C);
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#size()
     */
    public virtual int Count
    {
        get
        {
            return this._Formulas.Count;
        }
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#toArray()
     */
    public override object[] ToArray()
    {
        return this._Formulas.ToArray();
    }

    /*
     * (non-Javadoc)
     *
     * @see java.util.Collection#toArray(T[])
     */
    public override R[] ToArray<R>(R[] A)
    {
        return this._Formulas.ToArray(A);
    }

    /*
     * (non-Javadoc)
     *
     * @see java.lang.Object#hashCode()
     */
    public override int GetHashCode()
    {
        const int Prime = 31;
        int Result = 1;
        Result = Prime * Result + ((_Formulas == null) ? 0 : _Formulas.GetHashCode());
        return Result;
    }

    /*
     * (non-Javadoc)
     *
     * @see java.lang.Object#equals(java.lang.Object)
     */
    public override bool Equals(object Obj)
    {
        if (this == Obj)
        {
            return true;
        }
        if (Obj == null)
        {
            return false;
        }
        if (this.GetType() != Obj.GetType())
        {
            return false;
        }
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: InterpretationSet<?, ?, ?> other = (InterpretationSet<?, ?, ?>) obj;
        InterpretationSet<object, object, object> Other = (InterpretationSet<object, object, object>) Obj;
        if (_Formulas == null)
        {
            if (Other._Formulas != null)
            {
                return false;
            }
        }
        else if (!_Formulas.SetEquals(Other._Formulas))
        {
            return false;
        }
        return true;
    }

    /*
     * (non-Javadoc)
     *
     * @see java.lang.Object#toString()
     */
    public override string ToString()
    {
        return this._Formulas.ToString();
    }
}