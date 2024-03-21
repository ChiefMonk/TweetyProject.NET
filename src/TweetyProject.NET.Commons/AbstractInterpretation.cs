namespace TweetyProject.NET.Commons;


public abstract class AbstractInterpretation<TB, TS> : Interpretation<TB, TS> 
    where TB : BeliefBase 
    where TS : Formula
{
    public abstract bool Satisfies(TS formula);

    public abstract bool Satisfies(TB beliefBase);

    public virtual bool Satisfies(ICollection<TS> formulas)
    {
        if (formulas == null)
        {
            throw new ArgumentNullException(nameof(formulas), "Argument cannot be null.");
        }

        foreach (TS F in formulas)
        {
            if (!Satisfies(F))
            {
                return false;
            }
        }
        return true;
    }
}