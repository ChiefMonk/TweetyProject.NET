using System.Diagnostics;
using System.Text;

namespace TweetyProject.NET.Commons.util;

public class CygwinShell : Shell 
{
    private readonly string _binaryLocation;

    public CygwinShell(string binaryLocation)
    {
        _binaryLocation = binaryLocation;
    }

    public override string Run(string cmd)
    {
        var result = new StringBuilder();

        try
        {
            var procStartInfo = new ProcessStartInfo
            {
                FileName = _binaryLocation,
                Arguments = "-c \"" + cmd + "\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.Start();

            while (!proc.StandardOutput.EndOfStream)
            {
                var line = proc.StandardOutput.ReadLine();
                result.AppendLine(line);
            }
            proc.WaitForExit(); // You could add a timeout for extra safety
        }
        catch (Exception ex)
        {
            // Handle or throw the exception depending on your use case.
            // In a real application, you might want to log this exception or communicate the failure to the user.
            throw new InvalidOperationException("Failed to run the Cygwin command.", ex);
        }

        return result.ToString();
    }
}
