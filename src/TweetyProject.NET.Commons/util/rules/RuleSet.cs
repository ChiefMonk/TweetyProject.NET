namespace TweetyProject.NET.Commons.util.rules;

public class RuleSet<TT> : HashSet<TT>
    where TT : Rule<Formula, Formula>
{
    /// <summary>
    /// Creates a new empty rule set.
    /// </summary>
    public RuleSet()
    {
    }

    /// <summary>
    /// Creates a new rule set with the given rules </summary>
    public RuleSet(ICollection<TT> rules) : base(rules)
    {
    }

    /// <summary>
    /// Returns all rules this set with the given conclusion </summary>
    /// <param name="f"> a formula </param>
    /// <returns> all rules this set with the given conclusion </returns>
    public virtual ISet<TT> GetRulesWithConclusion(Formula f)
    {
        var rulesWithConclusion = new HashSet<TT>();

        foreach (var rule in this)
        {
            if (rule.Conclusion.Equals(f))
            {
                rulesWithConclusion.Add(rule);
            }
        }
        return rulesWithConclusion;
    }

    /// <summary>
    /// Returns all conclusions of all rules of this rule set. </summary>
    /// <returns> all conclusions of all rules of this rule set. </returns>
    public virtual ISet<Formula> Conclusions
    {
        get
        {
            var conclusions = new HashSet<Formula>();
            foreach (var rule in this)
            {
                conclusions.Add(rule.Conclusion);
            }
            return conclusions;
        }
    }

    /// <summary>
    /// Returns all premises appearing in this rule set. </summary>
    /// <returns> all premises appearing in this rule set. </returns>
    public virtual ISet<Formula> Premises
    {
        get
        {
            var premises = new HashSet<Formula>();
            foreach (var rule in this)
            {
                premises.UnionWith(rule.Premise);
            }
            return premises;
        }
    }

    /// <summary>
    /// Returns the maximal subset of this rule set that is closed under
    /// "syntactic" neighbourhood relationship for the given formula. A formula/rule has
    /// a "syntactic" neighbourhood relationship with a rule iff they share vocabulary
    /// elements. </summary>
    /// <param name="f"> some formula </param>
    /// <returns> a rule set. </returns>
    public virtual RuleSet<TT> GetSyntacticModule(Formula f)
    {
        var ruleSet = new RuleSet<TT>();
        var sig = f.Signature;
        bool changed;
        do
        {
            changed = false;
            foreach (var rule in this)
            {
                if (!ruleSet.Contains(rule) && rule.Signature.IsOverlappingSignature(sig))
                {
                    ruleSet.Add(rule);
                    changed = true;
                    sig.AddSignature(rule.Signature);
                }
            }
        } while (changed);
        return ruleSet;
    }
}