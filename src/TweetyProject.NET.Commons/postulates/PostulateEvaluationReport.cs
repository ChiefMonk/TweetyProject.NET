namespace TweetyProject.NET.Commons.postulates;

using Formula = Formula;

/// <summary>
/// Summarises the results of a postulate evaluation.
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <S> The type of formulas </param>
public class PostulateEvaluationReport<TS> where TS : Formula
{

    /// <summary>
    /// The approach that is evaluated
    /// </summary>
    private readonly PostulateEvaluatable<TS> _ev;

    /// <summary>
    /// the instances that satisfy the given postulate (and are applicable)
    /// </summary>
    private readonly IDictionary<Postulate<TS>, ICollection<ICollection<TS>>> _positiveInstances;

    /// <summary>
    /// the instances that are not applicable for the given postulate (thus also
    /// satisfy the postulate)
    /// </summary>
    private readonly IDictionary<Postulate<TS>, ICollection<ICollection<TS>>> _notApplicableInstances;

    /// <summary>
    /// the instances that violate the given postulate
    /// </summary>
    private readonly IDictionary<Postulate<TS>, ICollection<ICollection<TS>>> _negativeInstances;

    /// <summary>
    /// Creates a new evaluation report for the given approach and set of postulates
    /// </summary>
    /// <param name="ev">         some approach </param>
    /// <param name="postulates"> a set of postulates </param>
    public PostulateEvaluationReport(PostulateEvaluatable<TS> ev, IList<Postulate<TS>> postulates)
    {
        _ev = ev;
        _positiveInstances = new Dictionary<Postulate<TS>, ICollection<ICollection<TS>>>();
        _negativeInstances = new Dictionary<Postulate<TS>, ICollection<ICollection<TS>>>();
        _notApplicableInstances = new Dictionary<Postulate<TS>, ICollection<ICollection<TS>>>();
        foreach (Postulate<TS> p in postulates)
        {
            _positiveInstances[p] = new LinkedList<ICollection<TS>>();
            _negativeInstances[p] = new LinkedList<ICollection<TS>>();
            _notApplicableInstances[p] = new LinkedList<ICollection<TS>>();
        }
    }

    /// <summary>
    /// Adds a positive instance for the given postulate (that is applicable)
    /// </summary>
    /// <param name="postulate"> some postulate </param>
    /// <param name="instance">  some instance </param>
    public virtual void AddPositiveInstance(Postulate<TS> postulate, ICollection<TS> instance)
    {
        _positiveInstances[postulate].Add(instance);
    }

    /// <summary>
    /// Adds an instance that is not applicable for the given postulate
    /// </summary>
    /// <param name="postulate"> some postulate </param>
    /// <param name="instance">  some instance </param>
    public virtual void AddNotApplicableInstance(Postulate<TS> postulate, ICollection<TS> instance)
    {
        _notApplicableInstances[postulate].Add(instance);
    }

    /// <summary>
    /// Adds a negative instance for the given postulate
    /// </summary>
    /// <param name="postulate"> some postulate </param>
    /// <param name="instance">  some instance </param>
    public virtual void AddNegativeInstance(Postulate<TS> postulate, ICollection<TS> instance)
    {
        _negativeInstances[postulate].Add(instance);
    }

    /// <summary>
    /// Returns the negative instances for the given postulate </summary>
    /// <param name="postulate"> some postulate </param>
    /// <returns> a collection of negative instances. </returns>
    public virtual ICollection<ICollection<TS>> GetNegativeInstances(Postulate<TS> postulate)
    {
        if (_negativeInstances.TryGetValue(postulate, out var instances))
        {
            return instances;
        }

        return new HashSet<ICollection<TS>>();
    }
    /*
     * (non-Javadoc)
     *
     * @see java.lang.Object#toString()
     */
    public override string ToString()
    {
        string result = "[" + _ev.ToString() + ":";
        foreach (Postulate<TS> p in _positiveInstances.Keys)
        {
            result += p.Name + "<" + _positiveInstances[p].Count + "," + _notApplicableInstances[p].Count + "," + _negativeInstances[p].Count + ">;";
        }
        return result + "]";

    }

    /// <returns> an easy-to-read string representation of the report in which 
    /// the results are ordered alphabetically by postulate name. </returns>
    public virtual string PrettyPrint()
    {
        int longest = 10;
        foreach (Postulate<TS> p in _positiveInstances.Keys)
        {
            if (p.Name.Length > longest)
            {
                longest = p.Name.Length;
            }
        }
        longest++;

// JAVA TO C# CONVERTER TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
        string result = _ev.GetType().Name + " RESULTS\n----------\n" + string.Format("%-" + longest + "s%-13s%-14s%s", "Postulate ", "posInstances ", "notApplicable ", "negInstances\n");

        SortedDictionary<string, string> orderedPostulateStrings = new SortedDictionary<string, string>();
        foreach (Postulate<TS> p in _positiveInstances.Keys)
        {
// JAVA TO C# CONVERTER TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
            string s = string.Format("%-" + longest + "s%-13s%-14s%s", p.Name + " ", _positiveInstances[p].Count, _notApplicableInstances[p].Count, _negativeInstances[p].Count) + "\n";
            orderedPostulateStrings[p.Name] = s; //TreeMap sorts postulates alphabetically by their names

        }

        foreach (string s in orderedPostulateStrings.Values)
        {
            result += s;
        }

        return result;
    }
}