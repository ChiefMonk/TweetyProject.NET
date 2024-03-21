namespace TweetyProject.NET.Commons.util;

/// <summary>
/// @author Nils Geilen
/// Provides several ways to run unix commands on different OSes.
/// </summary>
public abstract class Shell
{
    public static Shell NativeShell { get; } = new NativeShell();

    /// <summary>
    /// a wrapper for the os' native shell </summary>
    /// <returns>  a wrapper for the os' native shell </returns>

    /// <summary>
    /// a wrapper around the cygwin shell </summary>
    /// <param name="binary"> path to bash.exe </param>
    /// <returns>  a wrapper for the os' native shell </returns>
    public static Shell GetCygwinShell(string binary)
    {
        return new CygwinShell(binary);
    }

    /// <summary>
    /// runs command </summary>
    /// <param name="cmd"> the command to be run </param>
    /// <returns> the terminal output </returns>
    /// <exception cref="InterruptedException"> if some interruption occurred. </exception>
    /// <exception cref="IOException"> if some IO issue occurred. </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public abstract String run(String cmd) throws InterruptedException, java.io.IOException;
    public abstract string Run(string cmd);
}