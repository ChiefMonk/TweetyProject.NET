

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
/// This class contains some auxiliary methods for
/// working with vectors.
/// 
/// @author Matthias Thimm
/// </summary>
public class VectorTools
{

    /// <summary>
    /// Computes the midpoint of the two given vectors. </summary>
    /// <param name="left"> some vector </param>
    /// <param name="right"> some vector </param>
    /// <returns> the midpoint of the two given vectors. </returns>
    public static double[] MidPoint(double[] Left, double[] Right)
    {
        if (Left.Length != Right.Length)
        {
            throw new System.ArgumentException("The given arrays differ in their dimension.");
        }
        double[] Result = new double[Left.Length];
        for (int I = 0; I < Left.Length; I++)
        {
            Result[I] = (Left[I] + Right[I]) / 2;
        }
        return Result;
    }

    /// <summary>
    /// Computes the sum of the elements in v </summary>
    /// <param name="v">  some vector </param>
    /// <returns> the sum of the elements in v </returns>
    public static double Sum(double[] V)
    {
        double Result = 0;
        foreach (double D in V)
        {
            Result += D;
        }
        return Result;
    }

    /// <summary>
    /// Computes the Manhattan distance between the two given vectors. </summary>
    /// <param name="left">  some vector </param>
    /// <param name="right">  some vector </param>
    /// <returns> the Manhattan distance between the two given vectors. </returns>
    public static double ManhattanDistance(double[] Left, double[] Right)
    {
        if (Left.Length != Right.Length)
        {
            throw new System.ArgumentException("The given arrays differ in their dimension.");
        }
        double Result = 0;
        for (int I = 0; I < Left.Length; I++)
        {
            Result += Math.Abs(Left[I] - Right[I]);
        }
        return Result;
    }

    /// <summary>
    /// Computes the Manhattan distance between the two given lists. </summary>
    /// <param name="left">  some vector </param>
    /// <param name="right">  some vector </param>
    /// <returns> the Manhattan distance between the two given lists. </returns>
    public static double ManhattanDistance(IList<double> Left, IList<double> Right)
    {
        if (Left.Count != Right.Count)
        {
            throw new System.ArgumentException("The given lists differ in their dimension.");
        }
        double Result = 0;
        for (int I = 0; I < Left.Count; I++)
        {
            Result += Math.Abs(Left[I] - Right[I]);
        }
        return Result;
    }

    /// <summary>
    /// Computes the Manhattan distance of the given value vector to zero </summary>
    /// <param name="values"> a list of doubles. </param>
    /// <returns> the distance to zero </returns>
    public static double ManhattanDistanceToZero(IList<double> Values)
    {
        IList<double> Zero = new LinkedList<double>();
        for (int I = 0; I < Values.Count;I++)
        {
            Zero.Add(0d);
        }
        return VectorTools.ManhattanDistance(Values, Zero);
    }

    /// <summary>
    /// Computes the Manhattan distance of the given value vector to zero </summary>
    /// <param name="values"> a list of doubles. </param>
    /// <returns> the distance to zero </returns>
    public static double ManhattanDistanceToZero(double[] Values)
    {
        double[] Zero = new double[Values.Length];
        for (int I = 0; I < Values.Length;I++)
        {
            Zero[I] = 0;
        }
        return VectorTools.ManhattanDistance(Values, Zero);
    }

    /// <summary>
    /// Normalizes the given vector such that the sum of the elements
    /// equals "sum" </summary>
    /// <param name="v"> some vector </param>
    /// <param name="sum"> some vector </param>
    /// <returns> the normalized array </returns>
    public static double[] Normalize(double[] V, double Sum)
    {
        double[] Result = new double[V.Length];
        double Oldsum = VectorTools.Sum(V);
        for (int I = 0; I < V.Length; I++)
        {
            Result[I] = (V[I] / Oldsum) * Sum;
        }
        return Result;
    }

}