/*
 *  This file is part of "TweetyProject.NET", a collection of .NET libraries for
 *  logical aspects of artificial intelligence and knowledge representation.
 *
 *  TweetyProject is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General License version 3 as
 *  published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General License for more details.
 *
 *  You should have received a copy of the GNU Lesser General License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 *  Copyright 2023 The TweetyProject.NET Team <https://github.com/chiefmonk/tweetyproject.net>
 */

namespace TweetyProject.NET.Commons;

/**
 * A signatures lists the atomic language structures for some language. It is
 * represented by a (multi-)set of formulas.
 * 
 * @author Matthias Thimm
 * @author Anna Gessler
 * @author Chief Monk
 *
 */
public interface ISignature : ICloneable
{
	/**
	 * Checks whether this signature is a sub-signature of the given signature, i.e.
	 * whether each logical expression expressible with this signature is also
	 * expressible with the given signature.
	 *
	 * @param other a signature.
	 * @return "true" iff this signature is a sub-signature of the given one.
	 */
	bool IsSubSignature(ISignature? other);

	/**
 * Checks whether this signature has common elements with the given signature,
 * i.e. whether there are logical expressions expressible with this signature
 * that are also expressible with the given signature.
 *
 * @param other a signature.
 * @return "true" iff this signature is overlapping with the given one.
 */
	bool IsOverlappingSignature(ISignature? other);

	/**
	 * Adds the elements of the given signature to this signature.
	 *
	 * @param other a signature.
	 */
	void AddSignature(ISignature? other);

	/*
	 * (non-Javadoc)
	 *
	 * @see java.lang.Object#hashCode()
	 */

	int HashCode();


	string? ToString();

	/*
	 * (non-Javadoc)
	 *
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	bool Equals(object obj);

	/**
	 * Adds the given formula to this signature.
	 *
	 * @param obj some object
	 *
	 */
	void Add(object obj);

	/**
	 * Adds all elements of this collection to this signature.
	 *
	 * @param c a collection
	 *
	 */
	void AddAll<T>(ICollection<T> c);

	/**
	 * Adds the given formulas to the signature.
	 *
	 * @param objects some objects to be added
	 */
	void Add(params object[] objects);

	/**
	 * Returns true if this signature is empty.
	 *
	 * @return true if this signature is empty.
	 */
	bool IsEmpty();

	/**
	 * Removes the given formula from this signature, if it is present (optional
	 * operation).
	 *
	 * @param obj some object
	 */
	void Remove(object obj);

	/**
	 * Removes all of this signature elements that are also contained in the
	 * specified collection (optional operation). After this call returns, this
	 * signature will contain no elements in common with the specified collection.
	 *
	 * @param c a collection of objects
	 */
	void RemoveAll<T>(ICollection<T> c);

	/**
	 * Removes all elements of this signature. After this call returns, this
	 * signature will contain no elements.
	 */
	void Clear();

	/**
	 * clones signature
	 * @return clone
	 */
	new ISignature Clone();
}