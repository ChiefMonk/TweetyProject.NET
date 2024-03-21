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
namespace TweetyProject.NET.Commons.util;

/// <summary>
/// This class implements a simple triple of elements.
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <E> the type of the first element </param>
/// @param <F> the type of the second element </param>
/// @param <G> the type of the third element </param>
public class Triple<E, F, G>
{
    /// <summary>
    /// The first element of this triple
    /// </summary>
    internal E Obj1;

    /// <summary>
    /// The second element of this triple
    /// </summary>
    internal F Obj2;

    /// <summary>
    /// The third element of this triple
    /// </summary>
    internal G Obj3;

    /// <summary>
    /// Initializes the elements of this triple with the given parameters </summary>
    /// <param name="obj1"> the first element of this triple </param>
    /// <param name="obj2"> the second element of this triple </param>
    /// <param name="obj3"> the third element of this triple </param>
    public Triple(E obj1, F obj2, G obj3)
    
			this.Obj1 = obj1;
			this.Obj2 = obj2;
			this.Obj3 = obj3;
		}

    /// <summary>
    /// Initializes an empty triple.
    /// </summary>
    public Triple()
    
		}

    // Misc Methods

    /// <summary>
    /// returns the first element of this triple </summary>
    /// <returns> the first element of this triple </returns>
    public virtual E First
    {
        get
        
				return Obj1;
			}
        set
        
				this.Obj1 = value;
			}
    }


    /// <summary>
    /// returns the second element of this triple </summary>
    /// <returns> the second element of this triple </returns>
    public virtual F Second
    {
        get
        
				return Obj2;
			}
        set
        
				this.Obj2 = value;
			}
    }


    /// <summary>
    /// returns the third element of this triple </summary>
    /// <returns> the third element of this triple </returns>
    public virtual G Third
    {
        get
        
				return Obj3;
			}
        set
        
				this.Obj3 = value;
			}
    }


    /* (non-Javadoc)
     * @see java.lang.Object#hashCode()
     */
    public override int GetHashCode()
    
			const int Prime = 31;
			int Result = 1;
			Result = Prime * Result + ((Obj1 == null) ? 0 : Obj1.GetHashCode());
			Result = Prime * Result + ((Obj2 == null) ? 0 : Obj2.GetHashCode());
			Result = Prime * Result + ((Obj3 == null) ? 0 : Obj3.GetHashCode());
			return Result;
		}

    /* (non-Javadoc)
     * @see java.lang.Object#equals(java.lang.Object)
     */
    public override bool Equals(object Obj)
    
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
// ORIGINAL LINE: Triple<?,?,?> other = (Triple<?,?,?>) obj;
			Triple<object, object, object> Other = (Triple<object, object, object>) Obj;
			if (Obj1 == null)
			{
				if (Other.Obj1 != null)
				{
					return false;
				}
			}
			else if (!Obj1.Equals(Other.Obj1))
			{
				return false;
			}
			if (Obj2 == null)
			{
				if (Other.Obj2 != null)
				{
					return false;
				}
			}
			else if (!Obj2.Equals(Other.Obj2))
			{
				return false;
			}
			if (Obj3 == null)
			{
				if (Other.Obj3 != null)
				{
					return false;
				}
			}
			else if (!Obj3.Equals(Other.Obj3))
			{
				return false;
			}
			return true;
		}

}