namespace TweetyProject.NET.Commons.util;

public class SetTrie<T> where T : IComparable<T>
{
    private class SetTrieNode
    {
        private readonly SetTrie<T> _OuterInstance;

        internal bool Marked;
        internal IDictionary<T, SetTrieNode> Children;

        public SetTrieNode(SetTrie<T> outerInstance, SetTrieNode Parent)
        {
            this._OuterInstance = OuterInstance;
            this.Marked = false;
            this.Children = new Dictionary<T, SetTrieNode>();
        }

        public virtual bool Add(IList<T> Set, int Index, bool OnlyForSubsetTests)
        {
            if (Index >= Set.Count)
            {
                if (this.Marked)
                {
                    return false;
                }
                this.Marked = true;
                if (OnlyForSubsetTests)
                {
                    // no children needed
                    this.Children = new Dictionary<T, SetTrieNode>();
                }
                return true;
            }
            else
            {
                if (!this.Children.ContainsKey(Set[Index]))
                {
                    this.Children[Set[Index]] = new SetTrieNode(_OuterInstance, this);
                }
                return this.Children[Set[Index]].Add(Set, Index + 1, OnlyForSubsetTests);
            }
        }

        public virtual bool Contains(IList<T> Set, int Index)
        {
            if (Index >= Set.Count)
            {
                return this.Marked;
            }
            else
            {
                if (!this.Children.ContainsKey(Set[Index]))
                {
                    return false;
                }
                return this.Children[Set[Index]].Contains(Set, Index + 1);
            }
        }

        public virtual bool ContainsSubsetOf(IList<T> Set, int Index)
        {
            if (this.Marked)
            {
                return true;
            }
            if (Index >= Set.Count)
            {
                return false;
            }
            if (this.Children.ContainsKey(Set[Index]) && this.Children[Set[Index]].ContainsSubsetOf(Set, Index + 1))
            {
                return true;
            }
            return this.ContainsSubsetOf(Set, Index + 1);
        }

        public virtual int ActualSize()
        {
            int N = 0;
            if (this.Marked)
            {
                N++;
            }
            foreach (SetTrieNode Node in this.Children.Values)
            {
                N += Node.ActualSize();
            }
            return N;
        }

        public virtual int NumberOfNodes()
        {
            int N = 1;
            foreach (SetTrieNode Node in this.Children.Values)
            {
                N += Node.NumberOfNodes();
            }
            return N;
        }
    }

    private SetTrieNode _Root;
    private bool _OnlyForSubsetTests;
    private int _Size;

    /// <summary>
    /// Creates a new set-trie
    /// </summary>
    public SetTrie() : this(false)
    {
    }

    /// <summary>
    /// Creates a new set-trie </summary>
    /// <param name="onlyForSubsetTests"> if set to true, then
    /// this trie does not store all sets, but (basically)
    /// only minimal ones. Whenever a set is added and there
    /// is already a superset of that set contained, the larger
    /// is deleted. </param>
    public SetTrie(bool OnlyForSubsetTests)
    {
        this._Root = new SetTrieNode(this, null);
        this._OnlyForSubsetTests = OnlyForSubsetTests;
        this._Size = 0;
    }

    public virtual int Size()
    {
        return this._Size;
    }

    public virtual int ActualSize()
    {
        return this._Root.ActualSize();
    }

    public virtual int NumberOfNodes()
    {
        return this._Root.NumberOfNodes();
    }

    /// <summary>
    /// Inserts the given set into this set-trie </summary>
    /// <param name="set"> some set </param>
    /// <returns> "true" if indeed a set has been added </returns>
    public virtual bool Add(ICollection<T> Set)
    {
        if (this._OnlyForSubsetTests && this.ContainsSubsetOf(Set))
        {
            return false;
        }
        IList<T> Sorted = new List<T>(Set);
        Sorted.Sort();
        bool Reply = this._Root.Add(Sorted,0,this._OnlyForSubsetTests);
        if (Reply)
        {
            this._Size++;
        }
        return Reply;
    }

    /// <summary>
    /// Checks whether the given set is contained in this set-trie. </summary>
    /// <param name="set"> some set </param>
    /// <returns> "true" iff the given set is contained in this set-trie. </returns>
    public virtual bool Contains(ICollection<T> Set)
    {
        IList<T> Sorted = new List<T>(Set);
        Sorted.Sort();
        return this._Root.Contains(Sorted, 0);
    }

    /// <summary>
    /// Checks whether there is a subset of the given
    /// set contained. </summary>
    /// <param name="set"> some set </param>
    /// <returns> true if there is a subset of the given set </returns>
    public virtual bool ContainsSubsetOf(ICollection<T> Set)
    {
        IList<T> Sorted = new List<T>(Set);
        Sorted.Sort();
        return this._Root.ContainsSubsetOf(Sorted, 0);
    }
}