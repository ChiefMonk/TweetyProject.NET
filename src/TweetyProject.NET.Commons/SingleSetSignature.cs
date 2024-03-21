namespace TweetyProject.NET.Commons;

public abstract class SingleSetSignature<T> : Signature, IEnumerable<T>
{
    public abstract void add(object obj);

    /// <summary>
    /// The set of formulas that represents this signature.
    /// </summary>
    protected internal ISet<T> Formulas;

    /// <summary>
    /// Creates a new empty signature.
    /// </summary>
    public SingleSetSignature()
    {
        Formulas = new HashSet<T>();
    }

    /// <summary>
    /// Creates a new signature with the given set of elements. </summary>
    /// <param name="formulas"> set of formulas  </param>
    public SingleSetSignature(ISet<T> formulas)
    {
        this.Formulas = formulas;
    }

    public virtual bool IsSubSignature(Signature Other)
    {
        if (!(Other is SingleSetSignature))
        {
            return false;
        }
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: SingleSetSignature<?> o = (SingleSetSignature<?>) other;
        SingleSetSignature<object> O = (SingleSetSignature<object>) Other;
        if (!O.Formulas.ContainsAll(this.Formulas))
        {
            return false;
        }
        return true;
    }

    public virtual bool IsOverlappingSignature(Signature Other)
    {
        if (!(Other is SingleSetSignature))
        {
            return false;
        }
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: SingleSetSignature<?> o = (SingleSetSignature<?>) other;
        SingleSetSignature<object> O = (SingleSetSignature<object>) Other;
        foreach (object Obj in O.Formulas)
        {
            if (this.Formulas.Contains(Obj))
            {
                return true;
            }
        }
        return false;
    }

    public virtual void AddSignature(Signature Other)
    {
        if (!(Other is SingleSetSignature))
        {
            return;
        }
// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @SuppressWarnings("unchecked") SingleSetSignature<T> sig = (SingleSetSignature<T>) other;
        SingleSetSignature<T> Sig = (SingleSetSignature<T>) Other;
        this.Formulas.AddAll(Sig.Formulas);
    }

    public override int GetHashCode()
    {
        const int Prime = 31;
        int Result = 1;
        Result = Prime * Result + ((Formulas == null) ? 0 : Formulas.GetHashCode());
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
// ORIGINAL LINE: SingleSetSignature<?> other = (SingleSetSignature<?>) obj;
        SingleSetSignature<object> Other = (SingleSetSignature<object>) Obj;
        if (Formulas == null)
        {
            if (Other.Formulas != null)
            {
                return false;
            }
        }
        else if (!Formulas.SetEquals(Other.Formulas))
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

    public virtual void Remove(object O)
    {
        Formulas.Remove(O);
    }

    public virtual bool Empty
    {
        get
        {
            return Formulas.Count == 0;
        }
    }

    public virtual void RemoveAll<T1>(ICollection<T1> C)
    {
        foreach (object Obj in C)
        {
            this.Remove(Obj);
        }

    }
    /// <summary>
    /// retainAll
    /// </summary>
    /// <param name="c"> Collection </param>
    public virtual void RetainAll<T1>(ICollection<T1> C)
    {
        ICollection<object> ToBeRemoved = new HashSet<object>();
        foreach (object Obj in this)
        {
            if (!C.Contains(Obj))
            {
                ToBeRemoved.Add(Obj);
            }
        }
        this.RemoveAll(ToBeRemoved);
    }

    public virtual void Clear()
    {
        Formulas = new HashSet<T>();
    }

    public virtual IEnumerator<T> GetEnumerator()
    {
        return Formulas.GetEnumerator();
    }

    public override string ToString()
    {
        return Formulas.ToString();
    }

    /// <summary>
    /// Returns the number of elements in this signature, 
    /// i.e. the the size of the set that represents the signature. </summary>
    /// <returns> size of the signature </returns>
    public virtual int Size()
    {
        return Formulas.Count;
    }

    /// <summary>
    /// Returns true if this signature contains the specified formula. </summary>
    /// <param name="f"> a formula </param>
    /// <returns> true if the signature contains f, false otherwise </returns>
    public virtual bool Contains(T F)
    {
        return Formulas.Contains(F);
    }

    /// <summary>
    /// Returns true if this signature contains all of the elements 
    /// in the specified collection. </summary>
    /// <param name="c"> collection of formulas </param>
    /// <returns> true if the signature contains c, false otherwise </returns>
    public virtual bool ContainsAll(ICollection<T> C)
    {
        foreach (T F in C)
        {
            if (!Formulas.Contains(F))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Returns an array containing all of the elements in this signature. </summary>
    /// <returns> signature as array </returns>
    public virtual object[] ToArray()
    {
        return Formulas.ToArray();
    }

    /// <summary>
    /// Returns a collection containing all of the elements in this signature. </summary>
    /// <returns> formulas of this signature </returns>
    public virtual ICollection<T> ToCollection()
    {
        return Formulas;
    }

    public override abstract SingleSetSignature<T> Clone();

}