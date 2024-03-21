
namespace TweetyProject.NET.Commons.util;

/// <summary>
/// This class implements a simple pair of elements.
/// 
/// @author Matthias Thimm
/// @author Bastian Wolf
/// </summary>
/// @param <E> the type of the first element </param>
/// @param <F> the type of the second element </param>
[Serializable]
public class Pair<TE, TF>
{
    /// <summary>
    /// The first element of this pair
    /// </summary>
    public TE Obj1
    {
        get;
        set;
    }

    /// <summary>
    /// The second element of this pair
    /// </summary>
    public TF Obj2
    {
        get;
        set;
    }

    /// <summary>
    /// Initializes an empty pair.
    /// </summary>
    public Pair()
    {
    }

    /// <summary>
    /// Initializes the elements of this pair with the given parameters </summary>
    /// <param name="obj1"> the first element of this pair </param>
    /// <param name="obj2"> the second element of this pair </param>
    public Pair(TE obj1, TF obj2)
    {
        Obj1 = obj1;
        Obj2 = obj2;
    }

    // Misc Methods

    /// <summary>
    /// returns the first element of this pair </summary>
    /// <returns> the first element of this pair </returns>
    public virtual TE First
    {
        get => Obj1;
        set => Obj1 = value;
    }


    /// <summary>
    /// returns the second element of this pair </summary>
    /// <returns> the second element of this pair </returns>
    public virtual TF Second
    {
        get => Obj2;
        set => Obj2 = value;
    }


    /* (non-Javadoc)
     * @see java.lang.Object#hashCode()
     */
    public override int GetHashCode()
    {
        const int prime = 31;
        int result = 1;

        result = prime * result + ((Obj1 == null) ? 0 : Obj1.GetHashCode());
        result = prime * result + ((Obj2 == null) ? 0 : Obj2.GetHashCode());

        return result;
    }

    /* (non-Javadoc)
     * @see java.lang.Object#equals(java.lang.Object)
     */
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
// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
// ORIGINAL LINE: Pair<?,?> other = (Pair<?,?>) obj;
        Pair<object, object> other = (Pair<object, object>) obj;
        if (Obj1 == null)
        {
            if (other.Obj1 != null)
            {
                return false;
            }
        }
        else if (!Obj1.Equals(other.Obj1))
        {
            return false;
        }
        if (Obj2 == null)
        {
            if (other.Obj2 != null)
            {
                return false;
            }
        }
        else if (!Obj2.Equals(other.Obj2))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// returns a string representation of a pair as "(obj1, obj2)" </summary>
    /// <returns> a string representation of a pair as "(obj1, obj2)" </returns>
    public override string ToString()
    {
        string s = "(" + Obj1.ToString() + ", " + Obj2.ToString() + ")";
        return s;
    }

}