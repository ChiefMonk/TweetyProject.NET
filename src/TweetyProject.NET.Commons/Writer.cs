namespace TweetyProject.NET.Commons;

public abstract class Writer
{
    /// <summary>
    /// The object to be printed.
    /// </summary>
    protected object? Input = null;

    /// <summary>
    /// Creates a new writer for the given object. </summary>
    /// <param name="obj"> an object. </param>
    protected Writer(object? obj)
    {
        Input = obj;
    }

    /// <summary>
    /// Creates a new empty writer.
    /// </summary>
    protected Writer()
    {
    }

    /// <summary>
    /// Sets the object of this writer. </summary>
    /// <param name="obj"> some object </param>
    public virtual object? ObjectToBePrinted
    {
        set => Input = value;
        get => Input;
    }


    /// <summary>
    /// Writes the object into a string. </summary>
    /// <returns> the string representing the object. </returns>
    public abstract string WriteToString();

   
    public virtual void WriteToFile(string filename)
    {
        string s = WriteToString(); // Assuming WriteToString() is a method that returns a string.
        try
        {
            // Write object to file
            File.WriteAllText(filename, s);
            // Console.WriteLine("Success: Wrote object to " + filename);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}