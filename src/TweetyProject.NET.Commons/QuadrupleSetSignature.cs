

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
/// This class models a signature as four sets of formulas.
/// 
/// @author Matthias Thimm
/// @author Anna Gessler
/// </summary>
/// @param <T> The types of formulas in this signature. </param>
/// @param <S> The types of formulas in this signature. </param>
/// @param <U> The types of formulas in this signature. </param>
/// @param <R> The types of formulas in this signature. </param>
public abstract class QuadrupleSetSignature<T, S, U, R> : Signature
{
    public abstract void remove(object obj);
    public abstract void add(object obj);

    /// <summary>
    /// The first set of formulas in this signature.
    /// </summary>
    protected internal ISet<T> FirstSet;

    /// <summary>
    /// The second set of formulas in this signature.
    /// </summary>
    protected internal ISet<S> SecondSet;

    /// <summary>
    /// The third set of formulas in this signature.
    /// </summary>
    protected internal ISet<U> ThirdSet;

    /// <summary>
    /// The fourth set of formulas in this signature.
    /// </summary>
    protected internal ISet<R> FourthSet;

    /// <summary>
    /// Creates a new empty signature.
    /// </summary>
    public QuadrupleSetSignature()
    {
        FirstSet = new HashSet<T>();
        SecondSet = new HashSet<S>();
        ThirdSet = new HashSet<U>();
        FourthSet = new HashSet<R>();
    }

    public virtual bool IsSubSignature(Signature Other)
    {
        if (!(Other is QuadrupleSetSignature))
        {
            return false;
        }
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: QuadrupleSetSignature<?,?,?,?> o = (QuadrupleSetSignature<?,?,?,?>) other;
        QuadrupleSetSignature<object, object, object, object> O = (QuadrupleSetSignature<object, object, object, object>) Other;
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
        if (!O.FourthSet.ContainsAll(this.FourthSet))
        {
            return false;
        }
        return true;
    }

    public virtual bool IsOverlappingSignature(Signature Other)
    {
        if (!(Other is QuadrupleSetSignature))
        {
            return false;
        }
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: QuadrupleSetSignature<?,?,?,?> o = (QuadrupleSetSignature<?,?,?,?>) other;
        QuadrupleSetSignature<object, object, object, object> O = (QuadrupleSetSignature<object, object, object, object>) Other;
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
        foreach (object Obj in O.FourthSet)
        {
            if (this.FourthSet.Contains(Obj))
            {
                return true;
            }
        }
        return true;
    }

    public virtual void AddSignature(Signature Other)
    {
        if (!(Other is QuadrupleSetSignature))
        {
            return;
        }
// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @SuppressWarnings("unchecked") QuadrupleSetSignature<T,S,U,R> sig = (QuadrupleSetSignature<T,S,U,R>) other;
        QuadrupleSetSignature<T, S, U, R> Sig = (QuadrupleSetSignature<T, S, U, R>) Other;
        this.FirstSet.AddAll(Sig.FirstSet);
        this.SecondSet.AddAll(Sig.SecondSet);
        this.ThirdSet.AddAll(Sig.ThirdSet);
        this.FourthSet.AddAll(Sig.FourthSet);
    }

    public override int GetHashCode()
    {
        const int Prime = 31;
        int Result = 1;
        Result = Prime * Result + ((FirstSet == null) ? 0 : FirstSet.GetHashCode());
        Result = Prime * Result + ((SecondSet == null) ? 0 : SecondSet.GetHashCode());
        Result = Prime * Result + ((ThirdSet == null) ? 0 : ThirdSet.GetHashCode());
        Result = Prime * Result + ((FourthSet == null) ? 0 : FourthSet.GetHashCode());
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
// ORIGINAL LINE: QuadrupleSetSignature<?,?,?,?> other = (QuadrupleSetSignature<?,?,?,?>) obj;
        QuadrupleSetSignature<object, object, object, object> Other = (QuadrupleSetSignature<object, object, object, object>) Obj;
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
        if (FourthSet == null)
        {
            if (Other.FourthSet != null)
            {
                return false;
            }
        }
        else if (!FourthSet.SetEquals(Other.FourthSet))
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
            return (FirstSet.Count == 0 && SecondSet.Count == 0 && ThirdSet.Count == 0 && FourthSet.Count == 0);
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
        FourthSet = new HashSet<R>();
    }

    public override string ToString()
    {
        return FirstSet.ToString() + ", " + SecondSet.ToString() + ", " + ThirdSet.ToString() + ", " + FourthSet.ToString();
    }

    public override abstract QuadrupleSetSignature<T, S, U, R> Clone();

}