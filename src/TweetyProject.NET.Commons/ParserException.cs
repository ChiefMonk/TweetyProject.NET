namespace TweetyProject.NET.Commons;

/// <summary>
/// This class models a general exception for parsing.
/// 
/// @author Matthias Thimm 
/// </summary>
public class ParserException : Exception
{
    /// <summary>
    /// Creates a new parser exception with the given message. </summary>
    /// <param name="message"> a string. </param>
    public ParserException(string message) : base(message)
    {
    }

    /// <summary>
    /// Creates a new parser exception with the given sub exception. </summary>
    /// <param name="e"> an exception. </param>
    public ParserException(Exception e) : base(e?.Message, e)
    {
    }
}