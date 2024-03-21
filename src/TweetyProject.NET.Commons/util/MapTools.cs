namespace TweetyProject.NET.Commons.util;

public class MapTools<TE, TF>
{
    public virtual ISet<IDictionary<TE, TF>> AllMaps(IDictionary<ISet<TE>, ISet<TF>> relations)
    {
        ISet<ISet<IDictionary<TE, TF>>> maps = new HashSet<ISet<IDictionary<TE, TF>>>();

        foreach (ISet<TE> e in relations.Keys)
        {
            maps.Add(this.AllMaps(e,relations[e]));
        }

        ICollection<ISet<IDictionary<TE, TF>>> permutations = (new SetTools<IDictionary<TE, TF>>()).Permutations(maps);

        ISet<IDictionary<TE, TF>> result = new HashSet<IDictionary<TE, TF>>();

        foreach (ISet<IDictionary<TE, TF>> mapSet in permutations)
        {
            result.Add(this.Combine(mapSet));
        }
        return result;
    }

    public virtual ISet<IDictionary<TE, TF>> AllMapsSingleSource(IDictionary<TE, ISet<TF>> relations)
    {
        ISet<IDictionary<TE, TF>> result = new HashSet<IDictionary<TE, TF>>();
        result.Add(new Dictionary<TE, TF>());

        foreach (TE key in relations.Keys)
        {
            ISet<IDictionary<TE, TF>> newResult = new HashSet<IDictionary<TE, TF>>();

            foreach (var map in result)
            {
                foreach (TF val in relations[key])
                {
                    IDictionary<TE, TF> newMap = new Dictionary<TE, TF>(map);
                    newMap[key] = val;
                    newResult.Add(newMap);
                }
            }
            result = newResult;
        }

        return result;
    }


    /// <summary>
    /// Computes all bijections from E to F.
    /// E and F have to be of the same cardinality. </summary>
    /// <param name="domain"> some set. </param>
    /// <param name="range"> some set. </param>
    /// <returns> all bijections from E to F. </returns>
    public virtual ISet<IDictionary<TE, TF>> AllBijections(ICollection<TE> domain, ICollection<TF> range)
    {
        if (domain.Count != range.Count)
        {
            throw new System.ArgumentException("Domain and range have to be of the same cardinality");
        }

        ISet<IDictionary<TE, TF>> result = new HashSet<IDictionary<TE, TF>>();

        if (domain.Count == 1)
        {
            IDictionary<TE, TF> newMap = new Dictionary<TE, TF>();
            newMap[domain.GetEnumerator().Next()] = range.GetEnumerator().Next();
            result.Add(newMap);
            return result;
        }

        TE elem = domain.GetEnumerator().Next();

        ISet<TE> newDomain = new HashSet<TE>(domain);
        newDomain.Remove(elem);

        foreach (TF elem2 in range)
        {
            ISet<TF> newRange = new HashSet<TF>(range);
            newRange.Remove(elem2);
            ISet<IDictionary<TE, TF>> subResult = this.AllBijections(newDomain, newRange);
            foreach (var map in subResult)
            {
                map[elem] = elem2;
                result.Add(map);
            }
        }
        return result;
    }

    /// <summary>
    /// This methods computes all maps from domain to range. </summary>
    /// <param name="domain"> a set of elements. </param>
    /// <param name="range"> a set of elements </param>
    /// <returns> a set of maps, where every map maps any element of domain to an
    /// 		element of range. </returns>
    public virtual ISet<IDictionary<TE, TF>> AllMaps<T1, T2>(ISet<T1> domain, ISet<T2> range) where T1 : TE where T2 : TF
    {
        ISet<IDictionary<TE, TF>> allMaps = new HashSet<IDictionary<TE, TF>>();
        System.Collections.Generic.Stack<Pair<IDictionary<TE, TF>, Stack<TE>>> stack = new System.Collections.Generic.Stack<Pair<IDictionary<TE, TF>, Stack<TE>>>();
        Pair<IDictionary<TE, TF>, Stack<TE>> elem = new Pair<IDictionary<TE, TF>, Stack<TE>>();
        elem.First = new Dictionary<TE, TF>();
        elem.Second = new System.Collections.Generic.Stack<TE>();
        elem.Second.AddAll(domain);
        stack.Push(elem);
        while (stack.Count > 0)
        {
            elem = stack.Pop();
            if (elem.Second.Count == 0)
            {
                allMaps.Add(elem.First);
            }
            else
            {
                TE domelem = elem.Second.Pop();
                foreach (TF image in range)
                {
                    IDictionary<TE, TF> newMap = new Dictionary<TE, TF>(elem.First);
                    newMap[domelem] = image;
                    System.Collections.Generic.Stack<TE> newStack = new System.Collections.Generic.Stack<TE>();
                    newStack.AddAll(elem.Second);
                    stack.Push(new Pair<IDictionary<TE, TF>, Stack<TE>>(newMap,newStack));
                }
            }
        }
        return allMaps;
    }

    /// <summary>
    /// Combines all maps in singleMaps to one maps containing
    /// every assignment of each map in singleMaps. </summary>
    /// <param name="singleMaps"> the set of maps to be combined. </param>
    /// <returns> a single map. </returns>
    /// <exception cref="IllegalArgumentException"> if one key is used
    ///  	in more than one map of singleMaps. </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public Map<E,F> combine(Set<Map<E,F>> singleMaps)throws IllegalArgumentException
    public virtual IDictionary<TE, TF> Combine(ISet<IDictionary<TE, TF>> singleMaps)
    {
        IDictionary<TE, TF> result = new Dictionary<TE, TF>();
        foreach (var map in singleMaps)
        {
            foreach (TE key in map.Keys)
            {
                if (result.ContainsKey(key))
                {
                    throw new System.ArgumentException("Value of key " + key + " is ambiguous.");
                }
                result[key] = map[key];
            }
        }
        return result;
    }

    /// <summary>
    /// Checks whether the given map is injective, i.e. whether no two different keys
    /// are assigned the same value. </summary>
    /// <param name="map"> a map </param>
    /// <returns> "true" iff the given map is injective. </returns>
    public static bool IsInjective<T1, T2>(IDictionary<T1, T2> map) where T1 : class where T2 : class
    {
        foreach (T1 key1 in map.Keys)
        {
            foreach (T1 key2 in map.Keys)
            {
                if (key1 != key2)
                {
                    if (map[key1].Equals(map[key2]))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}