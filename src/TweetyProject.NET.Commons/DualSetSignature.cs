namespace TweetyProject.NET.Commons;

public abstract class DualSetSignature<T, S> : Signature
{
    public abstract void remove(object obj);
    public abstract void add(object obj);

    /// <summary>
    /// The first set of formulas of this signature.
    /// </summary>
    protected internal ISet<T> FirstSet;

    /// <summary>
    /// The second set of formulas of this signature.
    /// </summary>
    protected internal ISet<S> SecondSet;

    /// <summary>
    /// Creates a new empty signature.
    /// </summary>
    public DualSetSignature()
    {
        FirstSet = new HashSet<T>();
        SecondSet = new HashSet<S>();
    }

    /// <summary>
    /// Creates a new signature with the given sets of formulas. </summary>
    /// <param name="args1"> a set of formulas </param>
    /// <param name="args2"> a set of formulas </param>
    public DualSetSignature(ISet<T> Args1, ISet<S> Args2)
    {
        FirstSet = Args1;
        SecondSet = Args2;
    }

    public virtual bool IsSubSignature(Signature Other)
    {
        if (!(Other is DualSetSignature))
        {
            return false;
        }
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: DualSetSignature<?,?> o = (DualSetSignature<?,?>) other;
        DualSetSignature<object, object> O = (DualSetSignature<object, object>) Other;
        if (!O.FirstSet.ContainsAll(this.FirstSet))
        {
            return false;
        }
        if (!O.SecondSet.ContainsAll(this.SecondSet))
        {
            return false;
        }
        return true;
    }

    public virtual bool IsOverlappingSignature(Signature Other)
    {
        if (!(Other is DualSetSignature))
        {
            return false;
        }
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: DualSetSignature<?,?> o = (DualSetSignature<?,?>) other;
        DualSetSignature<object, object> O = (DualSetSignature<object, object>) Other;
        foreach (object Obj in O.FirstSet)
        {
            if (this.FirstSet.Contains(Obj))
            {
                return true;
            }
        }
        foreach (object Obj in O.SecondSet)
        {
            if (this.SecondSet.Contains(Obj))
            {
                return true;
            }
        }
        return true;
    }

    public virtual void AddSignature(Signature Other)
    {
        if (!(Other is DualSetSignature))
        {
            return;
        }
// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @SuppressWarnings("unchecked") DualSetSignature<T,S> sig = (DualSetSignature<T,S>) other;
        DualSetSignature<T, S> Sig = (DualSetSignature<T, S>) Other;
        this.FirstSet.AddAll(Sig.FirstSet);
        this.SecondSet.AddAll(Sig.SecondSet);
    }

    public override int GetHashCode()
    {
        const int Prime = 31;
        int Result = 1;
        Result = Prime * Result + ((FirstSet == null) ? 0 : FirstSet.GetHashCode());
        Result = Prime * Result + ((SecondSet == null) ? 0 : SecondSet.GetHashCode());
        return Result;
    }

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
// ORIGINAL LINE: DualSetSignature<?,?> other = (DualSetSignature<?,?>) obj;
        DualSetSignature<object, object> Other = (DualSetSignature<object, object>) Obj;
        if (FirstSet == null)
        {
            if (Other.FirstSet != null)
            {
                return false;
            }
        }
        else if (!FirstSet.SetEquals(Other.FirstSet))
        {
            return false;
        }
        if (SecondSet == null)
        {
            if (Other.SecondSet != null)
            {
                return false;
            }
        }
        else if (!SecondSet.SetEquals(Other.SecondSet))
        {
            return false;
        }
        return true;
    }

    public virtual void AddAll<T1>(ICollection<T1> C)
    {
        foreach (object Obj in C)
        {
            this.Add(Obj);
        }
    }

// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: @Override public void add(Object... objects) throws IllegalArgumentException
    public virtual void Add(params object[] Objects)
    {
        foreach (object F in Objects)
        {
            this.Add(F);
        }
    }

    public virtual bool Empty
    {
        get
        {
            return (FirstSet.Count == 0 && SecondSet.Count == 0);
        }
    }

    public virtual void RemoveAll<T1>(ICollection<T1> C)
    {
        foreach (object Obj in C)
        {
            this.Remove(Obj);
        }
    }

    public virtual void Clear()
    {
        FirstSet = new HashSet<T>();
        SecondSet = new HashSet<S>();
    }

    public override string ToString()
    {
        return FirstSet.ToString() + ", " + SecondSet.ToString();
    }

    public override abstract DualSetSignature<T, S> Clone();

}