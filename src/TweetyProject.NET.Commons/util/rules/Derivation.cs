using TweetyProject.NET.Commons.Helpers;

namespace TweetyProject.NET.Commons.util.rules;

using System.Data;
using Formula = Formula;


public class Derivation<T> : List<T> 
    where T : Rule<Formula, Formula>
{
    /// <summary>
    /// Creates a new derivation with the given sequence of rules. </summary>
    /// <param name="rules"> a sequence of rules. </param>
    public Derivation(IList<T> rules) 
        : base(rules)
    {
    }

    /// <summary>
    /// Returns the conclusion of this derivation. </summary>
    /// <returns> the conclusion of this derivation. </returns>
    public virtual Formula Conclusion
    {
        get
        {
            var ruleSet = new RuleSet<T>(this);
            var conclusions = new HashSet<Formula>(ruleSet.Conclusions);
            var premises = new HashSet<Formula>(ruleSet.Conclusions);
            conclusions.ExceptWith(premises);
            return conclusions.First();
        }
    }

    /// <summary>
    /// Returns the set of all possible derivations from the set of rules. </summary>
    /// <param name="rules"> a set of rules </param>
    /// <returns> the set of all possible derivations </returns>
    /// @param <S> the type of rules </param>
 public static HashSet<Derivation<TS>> AllDerivations<TS>(ICollection<TS> rules) where TS : Rule<Formula, Formula>
    {
        var theRules = new RuleSet<TS>(rules);
        var allDerivations = new HashSet<Derivation<TS>>();

        foreach (var f in theRules.Conclusions)
        {
            allDerivations.UnionWith(AllDerivations(rules, f));
        }
        return allDerivations;
    }

    /// <summary>
    /// Returns the set of all possible derivations with the given
    /// conclusion from the set of rules. </summary>
    /// @param <S> the type of rules </param>
    /// <param name="rules"> a set of rules </param>
    /// <param name="conclusion"> the conclusion </param>
    /// <returns> the set of all possible derivations with the given conclusion </returns>
    public static HashSet<Derivation<TS>> AllDerivations<TS>(ICollection<TS> rules, Formula conclusion) where TS : Rule<Formula, Formula>
    {
        var stack = new Stack<Tuple<List<TS>, HashSet<Formula>, RuleSet<TS>>>();

        var initial = Tuple.Create(new List<TS>(), new HashSet<Formula> { conclusion }, new RuleSet<TS>(rules));

        stack.Push(initial);

        var derivations = new HashSet<Derivation<TS>>();

        while (stack.Any())
        {
            var derivation = stack.Pop();
            if (!derivation.Item2.Any())
                derivations.Add(new Derivation<TS>(derivation.Item1));
            else
            {
                foreach (var f in derivation.Item2)
                {
                    foreach (var r in derivation.Item3.GetRulesWithConclusion(f))
                    {
                        var newDerivation = Tuple.Create(new List<TS>(derivation.Item1), new HashSet<Formula>(derivation.Item2), new RuleSet<TS>(derivation.Item3));

                        newDerivation.Item1.Add(r);
                        newDerivation.Item2.Remove(f);
                        newDerivation.Item2.UnionWith(r.Premise);
                        newDerivation.Item3.RemoveAll(newDerivation.Item3.GetRulesWithConclusion(f));
                        bool noder = false;
                        foreach (var g in newDerivation.Item2)
                        {
                            if (newDerivation.Item1.Any(s => s.Conclusion.Equals(g)))
                            {
                                noder = true;
                                break;
                            }
                        }
                        if (!noder)
                            stack.Push(newDerivation);
                    }
                }
            }
        }
        return derivations;
    }


    /// <summary>
    /// Checks whether this derivation is founded, i.e.
    /// whether every formula appearing in the premise of
    /// a rule is also the conclusion of a previous rule. </summary>
    /// <returns> "true" if this derivation is founded. </returns>
    public bool IsFounded()
    {
        var toProve = new HashSet<Formula>();
        var rules = this.GetEnumerator();

        if (rules.MoveNext())
            toProve.UnionWith(rules.Current.Premise);

        while (rules.MoveNext())
        {
            var rule = rules.Current;

            toProve.Remove(rule.Conclusion);

            toProve.UnionWith(rule.Premise);
        }

        return toProve.Count == 0;
    }

    /// <summary>
    /// Checks whether this derivation is minimal with
    /// respect to set inclusion. This is equivalent to checking
    /// whether every conclusion besides the first is used
    /// in a premise and no conclusion appear twice. </summary>
    /// <returns> "true" if this derivation is minimal. </returns>
    public bool IsMinimal()
    {
        var ruleSet = new RuleSet<T>(this);

        foreach (var f in ruleSet.Conclusions)
        {
            if (ruleSet.GetRulesWithConclusion(f).Count > 1)
                return false;
        }

        var conclusions = new HashSet<Formula>(ruleSet.Conclusions);

        var premises = new HashSet<Formula>(ruleSet.Premises);

        conclusions.ExceptWith(premises);

        return conclusions.Count == 1;
    }

    /* (non-Javadoc)
     * @see java.util.AbstractList#hashCode()
     */
    public override int GetHashCode()
    {
        // for comparing a derivation is treated as a set
        ISet<T> ThisSet = new HashSet<T>(this);
        return ThisSet.GetHashCode();
    }

    /* (non-Javadoc)
     * @see java.util.AbstractList#equals(java.lang.Object)
     */
// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @SuppressWarnings("unchecked") @Override public boolean equals(Object obj)
    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
       
        // for comparing a derivation is treated as a set
        ISet<T> thisSet = new HashSet<T>(this);
        return thisSet.SetEquals(new HashSet<T>((Derivation<T>)obj));
    }

}