using System.Diagnostics;
using System.Text;

namespace TweetyProject.NET.Commons.util;

/// <summary>
/// Default shell
/// @author Nils Geilen, Matthias Thimm
/// 
/// </summary>
public class NativeShell : Shell
{

    public NativeShell()
    {
    }

    /* (non-Javadoc)
     * @see org.tweetyproject.commons.util.Shell#run(java.lang.String)
     */
    /// <summary>
    /// return 
    /// </summary>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: @Override public String run(String cmd) throws InterruptedException, java.io.IOException
    public override string Run(string cmd)
    {
        return InvokeExecutable(cmd, -1, true);
    }

    /// <summary>
    /// Executes the given command on the commandline and returns the complete output. </summary>
    /// <param name="commandline"> some command </param>
    /// <returns> the output of the execution </returns>
    /// <exception cref="IOException"> of an error was encountered. </exception>
    /// <exception cref="InterruptedException"> if some interruption occurred. </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public static String invokeExecutable(String commandline) throws IOException, InterruptedException
    public static string InvokeExecutable(string commandline)
    {
        return InvokeExecutable(commandline, -1);
    }

    /// <summary>
    /// invokeExecutable </summary>
    /// <param name="commandline"> String </param>
    /// <param name="maxLines"> String </param>
    /// <returns> NativeShell </returns>
    /// <exception cref="IOException"> throws </exception>
    /// <exception cref="InterruptedException"> throws </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public static String invokeExecutable(String commandline, long maxLines) throws IOException, InterruptedException
    public static string InvokeExecutable(string commandline, long maxLines)
    {
        return InvokeExecutable(commandline, maxLines, false);
    }

    /// <summary>
    /// Executes the given command on the commandline and returns the output up to a given number of lines. </summary>
    /// <param name="commandline"> some command </param>
    /// <param name="maxLines"> the maximum number of lines to be read (the process is killed afterwards) </param>
    /// <param name="suppressErrors"> if set to true, possible errors will not be included in the output </param>
    /// <returns> the output of the execution </returns>
    /// <exception cref="IOException"> of an error was encountered. </exception>
    /// <exception cref="InterruptedException"> if some interruption occurred.  </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public static String invokeExecutable(String commandline, long maxLines, boolean suppressErrors) throws IOException, InterruptedException
    public static string InvokeExecutable(string commandLine, long maxLines, bool suppressErrors)
    {
        return InvokeExecutableAsync(commandLine, maxLines, suppressErrors).GetAwaiter().GetResult();
    }

    public static async Task<string> InvokeExecutableAsync(string commandLine, long maxLines, bool suppressErrors)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c {commandLine}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        var output = new StringBuilder();
        using (var process = Process.Start(processStartInfo))
        {
            if (process == null)
            {
                throw new InvalidOperationException("Process could not be started.");
            }

            using (var reader = new StreamReader(process.StandardOutput.BaseStream))
            {
                string? line;
                long lines = 0;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    output.AppendLine(line);
                    lines++;
                    if (maxLines != -1 && lines >= maxLines)
                        break;
                }
            }

            // Check for errors (only if we did not exhaust max lines)
            if (maxLines == -1 || output.ToString().Split('\n').Length < maxLines)
            {
                using (var reader = new StreamReader(process.StandardError.BaseStream))
                {
                    string error = await reader.ReadToEndAsync();
                    error = error.Trim();
                    if (!suppressErrors && !string.IsNullOrEmpty(error))
                        throw new IOException(error);
                    else if (!string.IsNullOrEmpty(error))
                        output.AppendLine(error);
                }
            }

            await process.WaitForExitAsync();
        }

        return output.ToString();
    }
}