namespace TweetyProject.NET.Commons;

public interface IBeliefSetIterator<TT, out TU> : IEnumerator<TU>
    where TT : Formula
    where TU : BeliefSet<TT, Signature>
{
    bool HasNext();

    TU Next();
}