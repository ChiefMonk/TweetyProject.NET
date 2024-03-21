namespace TweetyProject.NET.Commons;

public interface Interpretation<TB, TS> where TB : BeliefBase where TS : Formula
{

    bool Satisfies(TS formula);

    public bool Satisfies(TB beliefBase);


    bool Satisfies(ICollection<TS> formulas);
}