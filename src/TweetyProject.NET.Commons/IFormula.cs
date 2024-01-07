namespace TweetyProject.NET.Commons;

/**
 * A formula is a basic language construct.
 * @author Matthias Thimm
 */
public interface IFormula
{
	/**
	 * Returns the signature of the language of this formula.
	 * @return the signature of the language of this formula.
	 */
	ISignature Signature
	{
		get;
	}
}