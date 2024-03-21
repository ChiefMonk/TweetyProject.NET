

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
/// Iterates over all subsets of a given set. This iterator first returns the empty
/// set, then all 1-element subsets, then all 2-element subsets,... 
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <T> The element class which is iterated. </param>
public class IncreasingSubsetIterator<T> : SubsetIterator<T>
{

    /// <summary>
    /// The actual set in a list. </summary>
    private IList<T> _Set;

    /// <summary>
    /// The indices of the generated subsets. </summary>
    private int[] _Indices;

    /// <summary>
    /// The current size of the subsets generated. </summary>
    private int _CurrentSize;

    /// <summary>
    /// For hasNext(). </summary>
    private bool _HasNext;

    /// <summary>
    /// Creates a new subset iterator for the given set. </summary>
    /// <param name="set"> some set. </param>
    public IncreasingSubsetIterator(ISet<T> Set) : base(Set)
    {
        this._Set = new List<T>(Set);
        this._Indices = new int[Set.Count];
        this._CurrentSize = 0;
        this._HasNext = true;
    }

    /* (non-Javadoc)
     * @see org.tweetyproject.util.SubsetIterator#hasNext()
     */
    public override bool HasNext()
    {
        return this._HasNext;
    }

    /* (non-Javadoc)
     * @see org.tweetyproject.util.SubsetIterator#next()
     */
    public override ISet<T> Next()
    {
        ISet<T> Result = new HashSet<T>();
        for (int I = 0; I < this._CurrentSize; I++)
        {
            Result.Add(this._Set[this._Indices[I]]);
        }
        if (this._CurrentSize != this._Set.Count)
        {
            this.Increment();
        }
        else
        {
            this._HasNext = false;
        }
        return Result;
    }

    /// <summary>
    /// Increments the indices.
    /// </summary>
    private void _Increment()
    {
        if (this._CurrentSize == 0)
        {
            this._CurrentSize = 1;
            this._Indices[0] = 0;
        }
        else
        {
            this.Increment(0);
        }
    }

    /// <summary>
    /// Increments the indices. </summary>
    /// <param name="lvl"> the level </param>
    /// <returns> the new index </returns>
    private int _Increment(int Lvl)
    {
        if (Lvl >= this._CurrentSize)
        {
            this._Indices[Lvl] = 0;
            this._CurrentSize++;
        }
        else if (this._Indices[Lvl] < this._Set.Count - Lvl - 1)
        {
            this._Indices[Lvl]++;
        }
        else
        {
            this._Indices[Lvl] = this.Increment(Lvl + 1) + 1;
        }
        return this._Indices[Lvl];
    }
}