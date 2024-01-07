namespace TweetyProject.NET.Commons;

/**
 * This class models a belief set, i.e. a set of formulae
 * of some formalism, and a signature.
 *
 * @author Matthias Thimm
 * @author Tim Janus
 * @author Anna Gessler
 *
 * @param <T> The type of the beliefs in this belief set.
 * @param <S> The type of signature attached to this belief set.
 */
public abstract class BeliefSet<T, S> : IBeliefBase, ICollection<T> where T : IFormula where S : ISignature
{
	/**
	 *Flag that determines whether {@link java.lang.Object#equals(Object)} checks
	 *only for equality of the beliefs in the belief sets
	 *or whether it also checks for equality of the signatures attached to the belief sets.
	 */
	public const bool EQUALS_USES_SIGNATURE = false;

	/**
	 * Creates a new (empty) belief set.
	 */
	protected BeliefSet()
	{
		this(new HashSet<T>());
		this.Signature = instantiateSignature();
	}

	public BeliefSet(IEnumerable<T> collection)
	{
		this.Signature = InstantiateSignature();
		this.Formulas = InstantiateSet();
		this.AddRange(collection);
	}

	/**
	 * Creates a new belief set with the given collection of
	 * formulae.
	 * @param c a collection of formulae.
	 */
	protected BeliefSet(ICollection<? extends T> c)
	{
		this.signature = instantiateSignature();
		this.formulas = instantiateSet();
		this.addAll(c);
	}

	/**
	 * Creates a new belief set with the given type of signature.
	 * @param sig a signature
	 */
	protected BeliefSet(S sig)
	{
		this.signature = instantiateSignature(sig);
		this.formulas = instantiateSet();
	}


	/**
	 * The set of formulas of this belief base.
	 */
	protected ISet<T> Formulas
	{
		get;
		set;
	}

	/**
	 * The signature of this belief base. It is always larger than or equal to
	 * {@link org.tweetyproject.commons.BeliefSet#getMinimalSignature()}
	 * (the signature of the language of {@link org.tweetyproject.commons.BeliefSet#formulas}).
	 */
	protected S Signature
	{
		get;
		set;
	}
}
