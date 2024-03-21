namespace TweetyProject.NET.Commons.Enums;

public enum InferenceMode
{
    /// <summary>
    /// Skeptical inference assesses a formula as true iff it is contained in <strong>every</strong> model
    /// </summary>
    SKEPTICAL,
    /// <summary>
    /// Credulous inference assesses a formula as true iff it is contained in <strong>some</strong> model
    /// </summary>
    CREDULOUS
}