namespace TweetyProject.NET.Commons;

/**
 * An interpretation for some logical language.
 * @author Matthias Thimm
 * @param <TB> the type of belief bases
 * @param <TS> the type of formulas
 */
public interface IInterpretation<TB, TS> where TB : IBeliefBase where TS : IFormula
{
	/**
	 * Checks whether this interpretation satisfies the given formula.
	 * @param formula a formula .
	 * @return "true" if this interpretation satisfies the given formula.
	 * @throws IllegalArgumentException if the formula does not correspond
	 * 		to the expected language.
	 */
	bool Satisfies(TS formula);

	/**
	 * Checks whether this interpretation satisfies all given formulas.
	 * @param formulas a collection of formulas.
	 * @return "true" if this interpretation satisfies all given formulas.
	 * @throws IllegalArgumentException if at least one formula does not correspond
	 * 		to the expected language.
	 */
	bool Satisfies(ICollection<TS> formulas);

	/**
	 * Checks whether this interpretation satisfies the given knowledge base.
	 * @param beliefBase a knowledge base.
	 * @return "true" if this interpretation satisfies the given knowledge base.
	 * @throws IllegalArgumentException IllegalArgumentException if the knowledgebase does not correspond
	 * 		to the expected language.
	 */
	bool Satisfies(TB beliefBase);
}
