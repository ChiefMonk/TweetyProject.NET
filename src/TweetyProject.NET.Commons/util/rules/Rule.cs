namespace TweetyProject.NET.Commons.util.rules;

public interface Rule<TC, TP> : Formula 
    where TC : Formula 
    where TP : Formula
{

    /// <summary>
    /// isFact </summary>
    /// <returns>  whether the rule is a fact </returns>
    bool IsFact { get; }
    /// <summary>
    /// isConstraint </summary>
    /// <returns> whether the rule is a constraint </returns>
    bool IsConstraint { get; }

    /// <summary>
    /// Set the conclusion of this rule. </summary>
    /// <param name="conclusion"> some formula </param>
    TC Conclusion { set;get; }

    /// <summary>
    /// Add the given premise to this rule. </summary>
    /// <param name="premise"> some formula </param>
    void AddPremise(TP premise);

    /// <summary>
    /// Add the given premises to this rule. </summary>
    /// <param name="premises"> some formulas </param>
    void AddPremises(ICollection<TP> premises);

    Signature Signature { get; }

    /// <summary>
    /// Returns the premise of this rule. </summary>
    /// <returns> the premise of this rule. </returns>
    ICollection<TP> Premise { get; }


}