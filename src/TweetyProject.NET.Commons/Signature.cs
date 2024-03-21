namespace TweetyProject.NET.Commons;

public interface Signature : ICloneable
{
    bool IsSubSignature(Signature other);

    bool IsOverlappingSignature(Signature other);

    void AddSignature(Signature other);
    
    int GetHashCode();

    string ToString();
    
    bool Equals(object obj);

    void Add(object obj);

    void Add(params object[] objects);

    void AddAll<T1>(ICollection<T1> c);

    bool Empty { get; }

    void Remove(object obj);

    void RemoveAll<T1>(ICollection<T1> c);

    void Clear();

}