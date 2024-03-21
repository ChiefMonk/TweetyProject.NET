using System.Collections;

namespace TweetyProject.NET.Commons.util;

/// <summary>
/// Iterates over all subsets of a given sets in a random order.
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <T> The element class which is iterated. </param>
public class RandomSubsetIterator<T> : SubsetIterator<T>
{

    /// <summary>
    /// The set over which subsets are iterated. </summary>
    private readonly IList<T> _set;

    /// <summary>
    /// Whether to avoid duplicates in the iteration. </summary>
    private readonly bool _avoidDuplicates;

    /// <summary>
    /// The random number generator. </summary>
    private readonly Random _random;

    /// <summary>
    /// Only used when avoidDuplicats is set to true. Then
    /// this set contains all (representations of) subsets
    /// already generated (if those are less than half the number
    /// of all subsets) or the subsets not generated yet (otherwise). 
    /// </summary>
    private ISet<BitArray> _temp;

    /// <summary>
    /// Only used when avoidDuplicats is set to true. The number
    /// of already generated subsets. 
    /// </summary>
    private long _generatedSubsets;

    /// <summary>
    /// Only used when avoidDuplicats is set to true. The number
    /// of all subsets. 
    /// </summary>
    private readonly double _allSubsets;

    /// <summary>
    /// Only used when avoidDuplicats is set to true. Whether
    /// the mode of using this.temp has been switched. 
    /// </summary>
    private bool _switched;

    /// <summary>
    /// Creates a new subset iterator for the given set. </summary>
    /// <param name="set"> some set. </param>
    /// <param name="avoidDuplicates"> whether to avoid duplicates in the iteration.
    ///  NOTE: setting this value to true might increase computation time
    ///  and needed space drastically. </param>
    public RandomSubsetIterator(ISet<T> set, bool avoidDuplicates) : base(set)
    {
        this._set = new List<T>(set);
        this._avoidDuplicates = avoidDuplicates;
        this._random = new Random();
        if (this._avoidDuplicates)
        {
            this._temp = new HashSet<BitArray>();
            this._generatedSubsets = 0;
            this._allSubsets = Math.Pow(2, this._set.Count);
            this._switched = false;
        }
    }

    /* (non-Javadoc)
     * @see java.util.Iterator#hasNext()
     */
    public override bool HasNext()
    {
        return !this._avoidDuplicates || this._generatedSubsets < this._allSubsets;
    }

    /* (non-Javadoc)
     * @see java.util.Iterator#next()
     */
    public override ISet<T> Next()
    {
        if (!this._avoidDuplicates)
        {
            ISet<T> result = new HashSet<T>();

            foreach (T elem in this._set)
            {
                if (this._random.NextBoolean())
                {
                    result.Add(elem);
                }
            }
            return result;
        }
        else
        {
            bool firstHalf = this._generatedSubsets == 0 || this._allSubsets / this._generatedSubsets > 2;
            BitArray bitSet = this.Generate(this._set.Count, firstHalf);
            ISet<T> result = new HashSet<T>();
            for (int I = 0; I < this._set.Count; I++)
            {
                if (bitSet.Length() > I && bitSet.Get(I))
                {
                    result.Add(this._set[I]);
                }
            }
            this._generatedSubsets++;
            if (!this._switched && this._allSubsets / this._generatedSubsets <= 2)
            {
                this._switched = true;
                ISet<BitArray> temp2 = new HashSet<BitArray>();
                bitSet = new BitArray();
                BitArray tmp;
                double numberOfBitSets = Math.Pow(2, this._set.Count);
                for (long I = 0; I < numberOfBitSets; I++)
                {
                    if (!this._temp.Contains(bitSet))
                    {
                        tmp = new BitArray();
                        tmp.Or(bitSet);
                        temp2.Add(tmp);
                    }
                    this.Increment(bitSet);
                }
                this._temp = temp2;
            }
            return result;
        }
    }

    /// <summary>
    /// Increments the given bit set </summary>
    /// <param name="bitSet"> some bit set. </param>
    private void _Increment(BitArray bitSet)
    {
        bool carry = true, tmp;
        int I = 0;
        while (carry)
        {
            tmp = carry;
            carry = carry && bitSet.Get(I);
            bitSet.Set(I, tmp ^ bitSet.Get(I));
            I++;
        }
    }

    /// <summary>
    /// Generates a new bit set of the given length. If checkForDuplicates
    /// is true then the all bit sets in this.temp are regarded as already
    /// being generated and the new one will be different from all of those.
    /// Furthermore, the new bit set is added to this.temp. If checkForDuplicates
    /// is false, the new bit set will be chosen from this.temp and removed there. </summary>
    /// <param name="length"> the length of the bit set. </param>
    /// <param name="checkForDuplicates"> whether to check for duplicates (see above). </param>
    /// <returns> a bit set. </returns>
    private BitArray _Generate(int length, bool checkForDuplicates)
    {
        BitArray result;
        if (checkForDuplicates)
        {
            do
            {
                result = this.GenerateRandomly(length);
            }while (this._temp.Contains(result));
            this._temp.Add(result);
            return result;
        }
        else
        {
            long idx = this._random.Next(this._temp.Count);
            foreach (BitArray elem in this._temp)
            {
                if (idx == 0)
                {
                    this._temp.Remove(elem);
                    return elem;
                }
                idx--;
            }
        }
        //this should not happen
        throw new Exception("this should not happen");
    }

    /// <summary>
    /// Generates a random bit set of the given length. </summary>
    /// <param name="length"> some length. </param>
    /// <returns> a random bit set. </returns>
    private BitArray _GenerateRandomly(int length)
    {
        BitArray result = new BitArray();
        for (int I = 0; I < length; I++)
        {
            if (this._random.NextBoolean())
            {
                result.Set(I, true);
            }
        }
        return result;
    }
}