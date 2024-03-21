using System.Collections;
namespace TweetyProject.NET.Commons.util;

public class DefaultSubsetIterator<T> : SubsetIterator<T>
{
    private BitArray _currentBitArray;
    private bool _hasNext = true;

    public DefaultSubsetIterator(ISet<T> set) : base(set)
    {
        _currentBitArray = new BitArray(set.Count);
    }

    public override bool MoveNext()
    {
        if (!_hasNext)
        {
            return false;
        }

        // Generate the current subset based on the BitArray
        _current = new HashSet<T>();
        for (int i = 0; i < _currentBitArray.Count; i++)
        {
            if (_currentBitArray[i])
            {
                _current.Add(_listSet[i]);
            }
        }

        // Increment the BitArray for the next call
        _hasNext = Increment(_currentBitArray);

        return true;
    }

    private bool Increment(BitArray bitArray)
    {
        for (int i = 0; i < bitArray.Count; i++)
        {
            if (bitArray[i] == false)
            {
                bitArray[i] = true;
                return true;
            }
            bitArray[i] = false;
        }

        // If we've looped through the entire BitArray and flipped all bits to false,
        // then we've iterated through all subsets and should not move next.
        return false;
    }

    public override void Reset()
    {
        _currentBitArray = new BitArray(_set.Count);
        _hasNext = true;
    }
}

