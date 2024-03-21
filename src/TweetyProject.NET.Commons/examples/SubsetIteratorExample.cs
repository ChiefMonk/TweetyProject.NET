using TweetyProject.NET.Commons.util;


namespace TweetyProject.NET.Commons.examples;

/// <summary>
/// Class SubsetIteratorExample
/// 
/// </summary>

public class SubsetIteratorExample
{
    /// <summary>
    /// iterator for subsets
    /// </summary>
    public virtual void Iterator()
    {
        var set = new HashSet<int>();

        for (var i = 0; i < 5; i++)
        {
            set.Add(i);
        }

        SubsetIterator<int> it = new IncreasingSubsetIterator<int>(set);

        string result = "";

        while (it.HasNext())
        {
            result += (it.Next().ToString());
        }

       // assertTrue(result.Equals("[][1][3][0, 1][0, 3][1, 2][1, 4][2, 4][0, 1, 2][0, 1, 4][0, 2, 4][1, 2, 3][1, 3, 4][0, 1, 2, 3][0, 1, 3, 4][1, 2, 3, 4]"));

    }
}