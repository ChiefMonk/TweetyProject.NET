using System.Collections;
using TweetyProject.NET.Commons.Extensions;

namespace TweetyProject.NET.Commons;

public abstract class SingleSetSignature<T> : ISignature, IEnumerable<T>
{
	/**
	 * Creates a new empty signature.
	 */
	protected SingleSetSignature()
	{
		Formulas = new HashSet<T>();
	}

	/**
	 * Creates a new signature with the given set of elements.
	 * @param formulas set of formulas
	 */
	protected SingleSetSignature(ISet<T> formulas)
	{
		Formulas = formulas;
	}

	/**
	 * The set of formulas that represents this signature.
	 */
	protected ISet<T>? Formulas
	{
		get;
		set;
	}

	public virtual bool IsSubSignature(ISignature? other)
	{
		if (other is null || Formulas is null)
			return false;

		if (other is not SingleSetSignature<T> signature)
			return false;

		return signature.Formulas != null && signature.Formulas.DoesFirstContainAllSecond(Formulas);
	}

	public virtual bool IsOverlappingSignature(ISignature? other)
	{
		if (other is null || Formulas is null)
			return false;

		if (other is not SingleSetSignature<T> signature)
			return false;

		return signature.Formulas is not null && signature.Formulas.Any(entry => Formulas.Contains(entry));
	}

	public virtual void AddSignature(ISignature? other)
	{
		if (other is null)
			return;

		if (other is not SingleSetSignature<T> signature)
			return;

		Formulas ??= new HashSet<T>();

		Formulas.AddAll(signature.Formulas);
	}

	public int HashCode()
	{
		throw new NotImplementedException();
	}

	public void Add(object obj)
	{
		throw new NotImplementedException();
	}

	public void AddAll<T1>(ICollection<T1> c)
	{
		throw new NotImplementedException();
	}

	public void Add(params object[] objects)
	{
		throw new NotImplementedException();
	}

	public bool IsEmpty()
	{
		throw new NotImplementedException();
	}

	public void Remove(object obj)
	{
		throw new NotImplementedException();
	}

	public void RemoveAll<T1>(ICollection<T1> c)
	{
		throw new NotImplementedException();
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	public ISignature Clone()
	{
		throw new NotImplementedException();
	}

	object ICloneable.Clone()
	{
		return Clone();
	}

	public IEnumerator<T> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}