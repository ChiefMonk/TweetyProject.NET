namespace TweetyProject.NET.Commons.analysis;

using BeliefBase = BeliefBase;
using Formula = Formula;

public interface InterpretationDistance<TT, TB, TS> 
    where TT : Interpretation<TB,TS> 
    where TB : BeliefBase 
    where TS : Formula
{

    /// <summary>
    /// Measures the distance between the two given interpretations. </summary>
    /// <param name="a"> some interpretation </param>
    /// <param name="b"> some interpretation </param>
    /// <returns> the distance between the two given interpretations. </returns>
    double Distance(TT a, TT b);

    /// <summary>
    /// Measures the distance between a formula and some
    /// interpretation by taking the minimal distance from all models
    /// of the formula to the given interpretation. </summary>
    /// <param name="f"> some formula </param>
    /// <param name="b"> some interpretation. </param>
    /// <returns> the distance between the set of models of the formula to the
    /// 	given interpretation. </returns>
    double Distance(TS f, TT b);
}