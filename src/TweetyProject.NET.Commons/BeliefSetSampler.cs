using System.Collections;

namespace TweetyProject.NET.Commons;


public abstract class BeliefSetSampler<T, TU> : IBeliefSetIterator<T, TU>
    where T : Formula
    where TU : BeliefSet<T, Signature>
{

    /// <summary>
    /// The signature of this sampler.
    /// </summary>
    private Signature _signature;

    /// <summary>
    /// Min length of samples belief sets 
    /// </summary>
    private int _minLength;

    /// <summary>
    /// Max length of samples belief sets.
    /// </summary>
    private int _maxLength;

    /// <summary>
    /// This constant specifies the default maximum length for sampled
    /// belief bases. The interpretation of this int depends on the
    /// actual type of knowledge representation but should 
    /// resemble the maximum number of formulas in the belief base;
    /// </summary>
    public const int DefaultMaximumBeliefbaseLength = 20;

    /// <summary>
    /// This constant specifies the default minimum length for sampled
    /// belief bases. The interpretation of this int depends on the
    /// actual type of knowledge representation but should 
    /// resemble the minimum number of formulas in the belief base;
    /// </summary>
    public const int DefaultMinimumBeliefbaseLength = 15;

    /// <summary>
    /// Creates a new belief base sampler for the given signature. </summary>
    /// <param name="signature"> a signature. </param>
    protected BeliefSetSampler(Signature signature)
        : this(signature, DefaultMinimumBeliefbaseLength, DefaultMaximumBeliefbaseLength)
    {
    }

    /// <summary>
    /// Creates a new belief base sampler for the given signature. </summary>
    /// <param name="signature"> a signature. </param>
    /// <param name="minLength"> the minimum length of knowledge bases </param>
    /// <param name="maxLength"> the maximum length of knowledge bases </param>
    protected BeliefSetSampler(Signature signature, int minLength, int maxLength)
    {
        _signature = signature;
        _minLength = minLength;
        _maxLength = maxLength;
    }


    public virtual bool HasNext()
    {
        // as samplers generate random instances there are
        // always next instances unless the signature is empty.
        return !SamplerSignature.Empty;
    }


    public abstract TU Next();

    /// <summary>
    /// Returns the signature of this sampler. </summary>
    /// <returns> the signature of this sampler. </returns>
    public virtual Signature SamplerSignature => _signature;

    /// <summary>
    /// Returns the min length of kbs of this sampler. </summary>
    /// <returns> the min length of kbs of this sampler. </returns>
    public virtual int MinLength => _minLength;

    /// <summary>
    /// Returns the max length of kbs of this sampler. </summary>
    /// <returns> the max length of kbs of this sampler. </returns>
    public virtual int MaxLength => _maxLength;

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public TU Current { get; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}