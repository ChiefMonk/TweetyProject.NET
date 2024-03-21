namespace TweetyProject.NET.Commons;

public interface IReasoner<out TO, in TB, in TF> where TB : BeliefBase where TF : Formula
{
    TO Query(TB beliefBase, TF formula);
}