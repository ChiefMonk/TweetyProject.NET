namespace TweetyProject.NET.Commons.util;

/// <summary>
/// This class provides some methods for set operations.
/// @author Matthias Thimm </summary>
/// @param <E> the type of elements </param>
public class SetTools<E>
{
    private static Random _Rand = new Random();

    /// <summary>
    /// This method computes all subsets of the given set of elements
    /// of class "E". </summary>
    /// <param name="elements"> a set of elements of class "E". </param>
    /// <returns> all subsets of "elements". </returns>
    public virtual ISet<ISet<E>> Subsets<T1>(ICollection<T1> Elements) where T1 : E
    {
        ISet<ISet<E>> Subsets = new HashSet<ISet<E>>();
        if (Elements.Count == 0)
        {
            Subsets.Add(new HashSet<E>());
        }
        else
        {
            E Element = Elements.GetEnumerator().Next();

            ISet<E> RemainingElements = new HashSet<E>(Elements);
            RemainingElements.Remove(Element);
            ISet<ISet<E>> Subsubsets = this.Subsets(RemainingElements);
            foreach (ISet<E> Subsubset in Subsubsets)
            {
                Subsets.Add(new HashSet<E>(Subsubset));
                Subsubset.Add(Element);
                Subsets.Add(new HashSet<E>(Subsubset));
            }
        }
        return Subsets;
    }

    /// <summary>
    /// This method computes all subsets of the given set of elements
    /// of class "E" with the given size. </summary>
    /// <param name="elements"> a set of elements of class "E". </param>
    /// <param name="size"> some int. </param>
    /// <returns> all subsets of "elements" of the given size. </returns>
    public virtual ISet<ISet<E>> Subsets<T1>(ICollection<T1> Elements, int Size) where T1 : E
    {
        if (Size < 0)
        {
            throw new System.ArgumentException("Size must be at least zero.");
        }
        ISet<ISet<E>> Subsets = new HashSet<ISet<E>>();
        if (Size == 0)
        {
            Subsets.Add(new HashSet<E>());
            return Subsets;
        }
        if (Elements.Count < Size)
        {
            return Subsets;
        }
        if (Elements.Count == Size)
        {
            Subsets.Add(new HashSet<E>(Elements));
            return Subsets;
        }
        if (Size == 1)
        {
            foreach (E E in Elements)
            {
                ISet<E> Set = new HashSet<E>();
                Set.Add(E);
                Subsets.Add(Set);
            }
            return Subsets;
        }
        E Element = Elements.GetEnumerator().Next();
        ISet<E> RemainingElements = new HashSet<E>(Elements);
        RemainingElements.Remove(Element);
        ISet<ISet<E>> Subsubsets = this.Subsets(RemainingElements,Size-1);
        foreach (ISet<E> Subsubset in Subsubsets)
        {
            Subsubset.Add(Element);
            Subsets.Add(new HashSet<E>(Subsubset));
        }
        Subsubsets = this.Subsets(RemainingElements,Size);
        foreach (ISet<E> Subsubset in Subsubsets)
        {
            Subsets.Add(new HashSet<E>(Subsubset));
        }
        return Subsets;
    }

    /// <summary>
    /// Computes all permutations of elements in partitions as follows.
    /// For any set A in the result and any set B in partitions it holds,
    /// that exactly one element of B is in A. For example<br>
    /// permutations({{a,b},{c,d,e},{f}})<br>
    /// equals to<br>
    /// {{a,c,f},{b,c,f},{a,d,f},{b,d,f},{a,e,f},{b,e,f}} </summary>
    /// <param name="partitions"> a set of sets of E. </param>
    /// <returns> a set of sets of E. </returns>
    public virtual ISet<ISet<E>> Permutations(ISet<ISet<E>> Partitions)
    {
        if (Partitions.Count == 0)
        {
            Partitions.Add(new HashSet<E>());
            return Partitions;
        }
        ISet<ISet<E>> Result = new HashSet<ISet<E>>();
        ISet<E> Set = Partitions.GetEnumerator().Next();
        ISet<Set<E>> Remaining = new HashSet<Set<E>>(Partitions);
        Remaining.Remove(Set);
        ISet<Set<E>> Subresult = this.Permutations(Remaining);
        foreach (Set<E> Subresultset in Subresult)
        {
            foreach (E Item in Set)
            {
                ISet<E> NewSet = new HashSet<E>();
                NewSet.AddAll(Subresultset);
                NewSet.Add(Item);
                Result.Add(NewSet);
            }
        }
        return Result;
    }

    /// <summary>
    /// Computes the set of irreducible hitting sets of "sets". A hitting set
    /// H is a set that has a non-empty intersection with every set in "sets".
    /// H is irreducible if no proper subset of H is a hitting set. </summary>
    /// <param name="sets"> a set of sets </param>
    /// <returns> the set of all irreducible hitting sets of "sets" </returns>
    public virtual ISet<ISet<E>> IrreducibleHittingSets(ISet<ISet<E>> Sets)
    {
        // naive implementation, should be revised at some time
        ISet<ISet<E>> Result;
        // if there is no set to hit, there are no hitting sets
        if (Sets.Count == 0)
        {
            return new HashSet<ISet<E>>();
        }
        // if there is only one set to hit, every element of that set
        // forms a hitting set
        if (Sets.Count == 1)
        {
            Result = new HashSet<ISet<E>>();
            ISet<E> H;
            foreach (E E in Sets.GetEnumerator().Next())
            {
                H = new HashSet<E>();
                H.Add(E);
                Result.Add(H);
            }
            return Result;
        }
        // if more than one set is to be hit, we recursively build up hitting sets
        ISet<E> Current = Sets.GetEnumerator().Next();
        ISet<ISet<E>> NewSets = new HashSet<ISet<E>>();
        NewSets.AddAll(Sets);
        NewSets.Remove(Current);
        // recursively solve the problem 
        Result = IrreducibleHittingSets(NewSets);
        // now check whether the current set is already hit; if not add some element
        ISet<E> Tmp;
        ISet<ISet<E>> NewResult = new HashSet<ISet<E>>();
        foreach (ISet<E> H in Result)
        {
            Tmp = new HashSet<E>();
            Tmp.AddAll(Current);
            Tmp.RetainAll(H);
            if (Tmp.Count == 0)
            {
                foreach (E E in Current)
                {
                    Tmp = new HashSet<E>();
                    Tmp.AddAll(H);
                    Tmp.Add(E);
                    NewResult.Add(Tmp);
                }
            }
            else
            {
                NewResult.Add(H);
            }
        }
        // check for irreducibility
        Result.Clear();
        foreach (ISet<E> H in NewResult)
        {
            Result.Add(H);
            foreach (ISet<E> H2 in NewResult)
            {
                if (H != H2)
                {
                    if (H.ContainsAll(H2))
                    {
                        Result.Remove(H);
                        break;
                    }
                }
            }
        }
        return Result;
    }

    /// <summary>
    /// Checks whether the given set of sets has an empty intersection </summary>
    /// <param name="sets"> some set of sets </param>
    /// <returns> true iff the all sets have an empty intersection. </returns>
    public virtual bool HasEmptyIntersection(ISet<ISet<E>> Sets)
    {
        ISet<E> I = new HashSet<E>();
        I.AddAll(Sets.GetEnumerator().Next());
        foreach (ISet<E> S in Sets)
        {
            I.RetainAll(S);
        }
        return I.Count == 0;
    }

    /// <summary>
    /// Returns the union of the set of sets. </summary>
    /// <param name="sets"> some set of sets </param>
    /// <returns> the union of the set. </returns>
    public virtual ISet<E> GetUnion(ISet<ISet<E>> Sets)
    {
        ISet<E> Result = new HashSet<E>();
        foreach (ISet<E> S in Sets)
        {
            Result.AddAll(S);
        }
        return Result;
    }

    /// <summary>
    /// Computes every bipartition of the given set, e.g. for
    /// a set {a,b,c,d,e,f} this method returns a set containing for example
    /// {{a,b,c,d,e},{f}} and {{a,b,c,},{d,e,f}} and {{a,b,c,d,e,f},{}}
    /// and {{a,d,e},{b,c,f}}. </summary>
    /// <param name="set"> a set of E </param>
    /// <returns> the set of all bipartitions of the given set. </returns>
    public virtual Set<Set<Set<E>>> GetBipartitions(ISet<E> Set)
    {
        ISet<Set<E>> Subsets = this.Subsets(Set);
        ISet<Set<Set<E>>> Bipartitions = new HashSet<Set<Set<E>>>();
        foreach (Set<E> Partition1 in Subsets)
        {
            ISet<E> Partition2 = new HashSet<E>(Set);
            Partition2.RemoveAll(Partition1);
            ISet<Set<E>> Bipartition = new HashSet<Set<E>>();
            Bipartition.Add(Partition1);
            Bipartition.Add(Partition2);
            Bipartitions.Add(Bipartition);
        }
        return Bipartitions;
    }

    /// <summary>
    /// Returns the symmetric difference of the two sets s and t, i.e.
    /// it returns (s \cup t) \setminus (s \cap t). </summary>
    /// <param name="s"> some set </param>
    /// <param name="t"> some set </param>
    /// <returns> the symmetric difference of the two sets </returns>
    public virtual ISet<E> SymmetricDifference(ICollection<E> S, ICollection<E> T)
    {
        ISet<E> Result = new HashSet<E>();
        ISet<E> Isec = new HashSet<E>(S);
        Isec.RetainAll(T);
        Result.AddAll(S);
        Result.AddAll(T);
        Result.RemoveAll(Isec);
        return Result;
    }

    /// <summary>
    /// Returns all independent sets of the given cardinality of the given set of sets.
    /// A set M={M1,...,Mk} is an independent set of N={N1,...,Nl} if M\subseteq N and
    /// for all i,j, i\neq j, Mi\cap Mj=\emptyset.  <br>
    /// This method uses a brute force approach to determine these sets.
    /// </summary>
    /// <param name="sets"> a set of sets </param>
    /// <param name="cardinality"> an int </param>
    /// <returns> all independent sets of the given cardinality of the given set of sets </returns>
    public virtual ISet<ISet<ICollection<E>>> IndependentSets(ISet<ICollection<E>> Sets, int Cardinality)
    {
        ISet<ISet<ICollection<E>>> Result = new HashSet<ISet<ICollection<E>>>();
        ISet<ISet<ICollection<E>>> Candidates = (new SetTools<ICollection<E>>()).Subsets(Sets, Cardinality);
        foreach (ISet<ICollection<E>> Candidate in Candidates)
        {
            if (this.IsIndependent(Candidate))
            {
                Result.Add(Candidate);
            }
        }
        return Result;
    }

    /// <summary>
    /// Checks whether the given set of sets is independent, i.e. whether
    /// all pairs of sets are disjoint. </summary>
    /// <param name="set"> a set of sets </param>
    /// <returns> "true" if the given set of sets is independent. </returns>
    public virtual bool IsIndependent(ISet<ICollection<E>> Set)
    {
        foreach (ICollection<E> S1 in Set)
        {
            foreach (ICollection<E> S2 in Set)
            {
                if (S1 != S2)
                {
                    foreach (E Elem in S1)
                    {
                        if (S2.Contains(Elem))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Picks one element uniformly at random from the set (not very efficient). </summary>
    /// <param name="set"> some set </param>
    /// <returns> one element from the set. </returns>
    public virtual E RandomElement(ICollection<E> Set)
    {
        int Idx = SetTools._Rand.Next(Set.Count);
        IEnumerator<E> It = Set.GetEnumerator();
        while (Idx > 0)
        {
// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
            It.Next();
            Idx--;
        }
// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
        return It.Next();
    }

    public static ISet<ISet<E>> PowerSet<E>(ISet<E> OriginalSet)
    {
        ISet<ISet<E>> Sets = new HashSet<ISet<E>>();
        if (OriginalSet.Count == 0)
        {
            Sets.Add(new HashSet<E>());
            return Sets;
        }
        IList<E> List = new System.Collections.Generic.List<E>(OriginalSet);
        E Head = List[0];
        ISet<E> Rest = new HashSet<E>(List.SubList(1, List.Count));
        foreach (Set<E> Set in PowerSet(Rest))
        {
            ISet<E> NewSet = new HashSet<E>();
            NewSet.Add(Head);
            NewSet.AddAll(Set);
            Sets.Add(NewSet);
            Sets.Add(Set);
        }
        return Sets;
    }
}