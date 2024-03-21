using System.Collections;
using System.Numerics;

namespace TweetyProject.NET.Commons.util;

/// <summary>
/// This class provides some utility methods for converting different
/// data types.
/// 
/// @author Matthias Thimm
/// 
/// </summary>
public static class ConversionTools
{

    /// <summary>
    /// Provides a string representation of the bits in the given BigInteger. </summary>
    /// <param name="value"> some BigInteger </param>
    /// <returns> a string representation of the bits in the given BigInteger. </returns>
    public static string BigInteger2BinaryString2(BigInteger value)
    {
        return "str";
    }



    public static string BigInteger2BinaryString(BigInteger value)
    {
        var str = "";

        var bitLength = value.ToBitLength(); // Custom extension method to mimic Java's bitLength() method.

        for (var j = bitLength - 1; j >= 0; j--)
        {
            // Test whether the bit at position j is set.
            var isBitSet = (value & (BigInteger.One << j)) != 0;

            str += isBitSet ? "1" : "0";
        }

        return str;
    }

    private static int ToBitLength(this BigInteger value)
    {
        // In C#, there's no direct equivalent of Java's bitLength method.
        // This is a simple implementation that finds the position of the highest bit set.
        if (value.Sign == 0)
            return 1; // Return 1 to account for the 0th bit.

        int length = 0;
        value = BigInteger.Abs(value); // Ensure the value is positive to simplify calculation.
        while (value != 0)
        {
            length++;
            value >>= 1;
        }

        return length;
    }



    /// <summary>
///Creates a bit set from the given BigInteger. </summary>
/// <param name="value"> value some BigInteger </param>
/// <returns> a bitset representing the BigInteger. </returns>
public static BitArray BigInteger2BitSet(BigInteger value)
    {
        return ConversionTools.BinaryString2BitSet(ConversionTools.BigInteger2BinaryString(value));
    }

    /// <summary>
    /// Provides a string representation of the bits in the given BitSet. </summary>
    /// <param name="s"> some BitSet </param>
    /// <returns> a string representation of the bits in the given BitSet. </returns>
    public static string BitSet2BinaryString(BitArray s)
    {
        string str = "";
        for (int I = 0; I < s.Count; I++)
        {
            str += s.Get(I) ? "1" : "0";
        }
        return str;
    }

    /// <summary>
    /// Creates a bit set from the given string of zeros and
    /// ones. Additional zeros are added to the prefix in, so
    /// that the string is aligned on the right side of the bitset </summary>
    /// <param name="s"> some string of zeros and ones </param>
    /// <returns> a bitset representing the bitvector encoded by the string. </returns>
    public static BitArray BinaryString2BitSet(string s)
    {
        BitArray bs = new BitArray(s.Length);

        for (int i = 1; i <= s.Length; i++)
        {
            if (s[^i] == '1')
            {
                bs.Set(bs.Count - i, true);
            }
        }
        return bs;
    }
}