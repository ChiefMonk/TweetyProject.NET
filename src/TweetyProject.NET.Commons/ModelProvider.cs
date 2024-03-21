namespace TweetyProject.NET.Commons;

/// <summary>
/// Instances of this interface model reasoners that determine the (selected) models
/// for a given set of formulas.
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <S> the type of formulas </param>
/// @param <B> the type of belief bases </param>
/// @param <T> the type of interpretations </param>
public interface IModelProvider<TS, TB, TT> where TS : Formula where TB : BeliefBase where TT : Interpretation<TB,TS>
{

    /// <summary>
    /// Returns a characterizing model of the given belief base </summary>
    /// <param name="bbase"> some belief base </param>
    /// <returns> the (selected) models of the belief base </returns>
    ICollection<TT> GetModels(TB bbase);

    /// <summary>
    /// Returns a single (dedicated) model of the given belief base.
    /// If the implemented method allows for more than one dedicated model,
    /// the selection may be non-deterministic. </summary>
    /// <param name="bbase"> some belief base </param>
    /// <returns> a selected model of the belief base. </returns>
    TT GetModel(TB bbase);
}