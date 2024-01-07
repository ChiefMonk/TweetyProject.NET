namespace TweetyProject.NET.Commons.Enums;

/**
 * Enum constants for the two classical inference models of skeptical inference (assess
 * a formula as true iff it is contained in every model) and credulous inference
 * (assess a formula as true iff it is contained in some model).
 *
 * @author Matthias Thimm
 */
public enum InferenceMode
{
	/**
	 * Skeptical inference assesses a formula as true iff it is contained in <strong>every</strong> model
	 */
	Skeptical,
	/**
	 * Credulous inference assesses a formula as true iff it is contained in <strong>some</strong> model
	 */
	Credulous
}