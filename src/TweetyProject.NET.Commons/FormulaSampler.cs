namespace TweetyProject.NET.Commons;

public abstract class FormulaSampler<T> where T : Formula
{

    /// <summary>
    /// The signature of this sampler.
    /// </summary>
    private Signature _signature;

  
    protected FormulaSampler(Signature signature)
    {
        _signature = signature;
    }

    /// <summary>
    /// This constant specifies the default length for sampled
    /// formulas. The interpretation of this int depends on the
    /// actual type of knowledge representation but should 
    /// resemble the number of atomic expressions and 
    /// connectives used.
    /// </summary>
    public const int DefaultMaximalFormulaLength = 2;

    /// <summary>
    /// This method randomly samples a single formula of the given signature
    /// with the given maximal formula length. </summary>
    /// <param name="FormulaLength"> the maximal length of the formula to be sampled. </param>
    /// <returns> a single formula. </returns>
    public abstract T RandomSample(int FormulaLength);

    /// <summary>
    /// This method randomly samples a single formula of the given signature
    /// with the default maximal formula length. </summary>
    /// <returns> a single formula. </returns>
    public virtual T RandomSample()
    {
        return RandomSample(DefaultMaximalFormulaLength);
    }

  
    public virtual ISet<T> RandomSample(int FormulaLength, int NumFormulas)
    {
        ISet<T> Formulas = new HashSet<T>();
        for (int I = 0; I < NumFormulas; I++)
        {
            Formulas.Add(RandomSample(FormulaLength));
        }
        return Formulas;
    }

    /// <summary>
    /// Returns the signature of this sampler. </summary>
    /// <returns> the signature of this sampler. </returns>
    public virtual Signature Signature => _signature;
}