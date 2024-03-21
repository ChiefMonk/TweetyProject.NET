namespace TweetyProject.NET.Commons.analysis;

using BeliefBase = BeliefBase;
using Formula = Formula;


public class DrasticDistance<TT, TB, TS> : InterpretationDistance<TT, TB, TS> 
    where TT : Interpretation<TB,TS> 
    where TB : BeliefBase 
    where TS : Formula
{

    public virtual double Distance(TT a, TT b)
    {
        return a.Equals(b) ? 0 : 1;
    }

    public virtual double Distance(TS f, TT b)
    {
        return b.Satisfies(f) ? 0 : 1;
    }
}