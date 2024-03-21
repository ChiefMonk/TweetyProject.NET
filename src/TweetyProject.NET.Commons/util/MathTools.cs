

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
/// This class contains some useful math tools.
/// 
/// @author Matthias Thimm
/// </summary>
public class MathTools
{

    /// <summary>
    /// This method computes "n choose k". If n &lt; 0 or k &lt; 0
    /// the result is defined to be 0. </summary>
    /// <param name="n"> an integer. </param>
    /// <param name="k"> an integer </param>
    /// <returns> the value of "n choose k". </returns>
    public static int? Binomial(int? N, int? K)
    {
        if (N < 0 || K < 0)
        {
            return 0;
        }
        if (N < K)
        {
            return 0;
        }
        if (K == 0)
        {
            return 1;
        }
        if (N == 0)
        {
            return 0;
        }
        return MathTools.Binomial(N.Value-1,K.Value-1).Value + MathTools.Binomial(N.Value-1,K).Value;
    }

    /// <summary>
    /// Computes the faculty of the given number. </summary>
    /// <param name="i"> an integer. </param>
    /// <returns> the value 1*...*i or 1 if i==0. </returns>
    public static int Faculty(int I)
    {
        if (I < 0)
        {
            return 0;
        }
        if (I == 0)
        {
            return 1;
        }
        int Result = 1;
        for (int J = 2; J <= I; J++)
        {
            Result *= J;
        }
        return Result;
    }

    /// <summary>
    /// Compute the average value and the variance of the given list of
    /// numbers. </summary>
    /// <param name="values"> some values </param>
    /// <returns> a pair of average value (first element) and variance (second element). </returns>
    public static Pair<double, double> AverageAndVariance(ICollection<double> Values)
    {
        Pair<double, double> Result = new Pair<double, double>();
        double? Sum = 0d;
        foreach (double? D in Values)
        {
            Sum = Sum.Value + D.Value;
        }
        Result.First = Sum.Value / Values.Count;
        Sum = 0d;
        foreach (double? D in Values)
        {
            Sum = Sum.Value + Math.Pow((D.Value - Result.First), 2);
        }
        Result.Second = Sum.Value / (Values.Count - 1);
        return Result;
    }
}