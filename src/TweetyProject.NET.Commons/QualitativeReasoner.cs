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
 *  Copyright 2018 The TweetyProject Team <http://tweetyproject.org/contact/>
 */

namespace TweetyProject.NET.Commons;

/// <summary>
/// The general interface for objects that are able to query a belief base
/// with some formula and return either TRUE or FALSE as answer.
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <B> the belief base type that can be queried </param>
/// @param <F> the type of formulas that can be queries </param>
public interface QualitativeReasoner<B, F> : IReasoner<bool, B, F> where B : BeliefBase where F : Formula
{
    /* (non-Javadoc)
     * @see org.tweetyproject.commons.Reasoner#query(org.tweetyproject.commons.BeliefBase, org.tweetyproject.commons.Formula)
     */
    bool? Query(B beliefbase, F formula);
    /// 
    /// <returns> if the solver is installed </returns>
    public abstract bool Installed { get; }
}