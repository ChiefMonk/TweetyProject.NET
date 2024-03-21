namespace TweetyProject.NET.Commons.streams;

using Formula = Formula;

public interface IFormulaStream<out TS> : IEnumerator<TS> 
    where TS : Formula
{
    bool HasNext();

    TS Next();

    void Remove();
}