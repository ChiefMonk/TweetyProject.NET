

/*
 *  This file is part of "TweetyProject", a collection of Java libraries for
 *  logical aspects of artificial intelligence and knowledge representation.
 *
 *  TweetyProject is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License version 3 as
 *  published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 *  Copyright 2016 The TweetyProject Team <http://tweetyproject.org/contact/>
 */
namespace TweetyProject.NET.Commons;

/// <summary>
/// This class models a signature as three sets of formulas.
/// 
/// @author Matthias Thimm
/// @author Anna Gessler
/// </summary>
/// @param <T> The types of formulas in this signature. </param>
/// @param <S> The types of formulas in this signature. </param>
/// @param <U> The types of formulas in this signature. </param>
public abstract class TripleSetSignature<T, S, U> : Signature
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
    /// The third set of formulas of this signature.
    /// </summary>
    protected internal ISet<U> ThirdSet;

    /// <summary>
    /// Creates a new empty signature.
    /// </summary>
    public TripleSetSignature()
    {
        FirstSet = new HashSet<T>();
        SecondSet = new HashSet<S>();
        ThirdSet = new HashSet<U>();
    }

    public virtual bool IsSubSignature(Signature Other)
    {
        if (!(Other is TripleSetSignature))
        {
            return false;
        }
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: TripleSetSignature<?,?,?> o = (TripleSetSignature<?,?,?>) other;
        TripleSetSignature<object, object, object> O = (TripleSetSignature<object, object, object>) Other;
        if (!O.FirstSet.ContainsAll(this.FirstSet))
        {
            return false;
        }
        if (!O.SecondSet.ContainsAll(this.SecondSet))
        {
            return false;
        }
        if (!O.ThirdSet.ContainsAll(this.ThirdSet))
        {
            return false;
        }
        return true;
    }

    public virtual bool IsOverlappingSignature(Signature Other)
    {
        if (!(Other is TripleSetSignature))
        {
            return false;
        }
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: TripleSetSignature<?,?,?> o = (TripleSetSignature<?,?,?>) other;
        TripleSetSignature<object, object, object> O = (TripleSetSignature<object, object, object>) Other;
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
        foreach (object Obj in O.ThirdSet)
        {
            if (this.ThirdSet.Contains(Obj))
            {
                return true;
            }
        }
        return true;
    }

    public virtual void AddSignature(Signature Other)
    {
        if (!(Other is TripleSetSignature))
        {
            return;
        }
// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @SuppressWarnings("unchecked") TripleSetSignature<T,S,U> sig = (TripleSetSignature<T,S,U>) other;
        TripleSetSignature<T, S, U> Sig = (TripleSetSignature<T, S, U>) Other;
        this.FirstSet.AddAll(Sig.FirstSet);
        this.SecondSet.AddAll(Sig.SecondSet);
        this.ThirdSet.AddAll(Sig.ThirdSet);
    }

    public override int GetHashCode()
    {
        const int Prime = 31;
        int Result = 1;
        Result = Prime * Result + ((FirstSet == null) ? 0 : FirstSet.GetHashCode());
        Result = Prime * Result + ((SecondSet == null) ? 0 : SecondSet.GetHashCode());
        Result = Prime * Result + ((ThirdSet == null) ? 0 : ThirdSet.GetHashCode());
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
// ORIGINAL LINE: TripleSetSignature<?,?,?> other = (TripleSetSignature<?,?,?>) obj;
        TripleSetSignature<object, object, object> Other = (TripleSetSignature<object, object, object>) Obj;
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
        if (ThirdSet == null)
        {
            if (Other.ThirdSet != null)
            {
                return false;
            }
        }
        else if (!ThirdSet.SetEquals(Other.ThirdSet))
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
            return (FirstSet.Count == 0 && SecondSet.Count == 0 && ThirdSet.Count == 0);
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
        ThirdSet = new HashSet<U>();
    }

    public override string ToString()
    {
        return FirstSet.ToString() + ", " + SecondSet.ToString() + ", " + ThirdSet.ToString();
    }

    public override abstract TripleSetSignature<T, S, U> Clone();
}