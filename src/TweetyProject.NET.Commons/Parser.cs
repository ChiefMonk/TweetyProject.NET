namespace TweetyProject.NET.Commons;

/// <summary>
/// This class models an abstract parser for belief bases and formulas.
/// </summary>
/// @param <T> the type of belief bases </param>
/// @param <S> the type of formulas
/// 
/// @author Matthias Thimm
/// @author Anna Gessler </param>
public abstract class Parser<T, S> where T : BeliefBase where S : Formula
{

    /// <summary>
    /// Regular expression that contains symbols that appear in formulas and cannot be used as separators
    /// for belief bases.
    /// </summary>
    protected internal string IllegalDelimitors = "[\\^\\|\\&!\\(\\)\\<\\>\\=\\^\\[\\]]|forall|exists ";

    /// <summary>
    /// Parses the file of the given filename into a belief base of the given type.
    /// </summary>
    /// <param name="filename"> the name of a file </param>
    /// <returns> a belief base </returns>
    /// <exception cref="FileNotFoundException"> if the file is not found </exception>
    /// <exception cref="IOException">           if some IO issue occurred. </exception>
    /// <exception cref="ParserException">       some parsing exceptions may be added here. </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public T parseBeliefBaseFromFile(String filename) throws FileNotFoundException, IOException, ParserException
    public virtual T ParseBeliefBaseFromFile(string filename)
    {
        StreamReader reader = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read));
        T Bs = this.ParseBeliefBase(reader);
        reader.Close();
        return Bs;
    }

    /// <summary>
    /// Parses the given text into a belief base of the given type.
    /// </summary>
    /// <param name="text"> a string </param>
    /// <returns> a belief base. </returns>
    /// <exception cref="IOException">     if some IO issue occurred. </exception>
    /// <exception cref="ParserException"> some parsing exceptions may be added here. </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public T parseBeliefBase(String text) throws IOException, ParserException
    public virtual T ParseBeliefBase(string text)
    {
        return this.ParseBeliefBase(new StringReader(text));
    }

    /// <summary>
    /// Parses the given reader into a belief base of the given type.
    /// </summary>
    /// <param name="reader"> a reader </param>
    /// <returns> a belief base </returns>
    /// <exception cref="IOException">     if some IO issue occurred. </exception>
    /// <exception cref="ParserException"> some parsing exceptions may be added here. </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public abstract T parseBeliefBase(Reader reader) throws IOException, ParserException;
    public abstract T ParseBeliefBase(Reader reader);

    /// <summary>
    /// Parses the file of the given filename into a list of belief bases of the given type. Belief
    /// bases are separated by three consecutive newline characters ("\n\n\n").
    /// </summary>
    /// <param name="filename"> a string </param>
    /// <returns> a list of belief bases in the order in which they appear in the input
    ///         string. </returns>
    /// <exception cref="IOException"> if an IO error occurs </exception>
    /// <exception cref="ParserException"> some parsing exception </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public java.util.List<T> parseListOfBeliefBasesFromFile(String filename) throws ParserException, IOException
    public virtual IList<T> ParseListOfBeliefBasesFromFile(string Filename)
    {
        string Text = File.ReadAllText(Path.Of(Filename));
        IList<T> Bs = this.ParseListOfBeliefBases(Text);
        return Bs;
    }

    /// <summary>
    /// Parses the file of the given filename into a list of belief bases of the given type. Belief
    /// bases are separated by the given delimiter.
    /// </summary>
    /// <param name="filename"> a string </param>
    /// <param name="delimiter"> for separating belief bases </param>
    /// <returns> a list of belief bases in the order in which they appear in the input
    ///         string. </returns>
    /// <exception cref="IOException"> if an IO error occurs </exception>
    /// <exception cref="ParserException"> some parsing exception </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public java.util.List<T> parseListOfBeliefBasesFromFile(String filename, String delimiter) throws ParserException, IOException
    public virtual IList<T> ParseListOfBeliefBasesFromFile(string Filename, string Delimiter)
    {
        string Text = File.ReadAllText(Path.Of(Filename));
        IList<T> Bs = this.ParseListOfBeliefBases(Text, Delimiter);
        return Bs;
    }


    /// <summary>
    /// Parses the given text into a list of belief bases of the given type. Belief
    /// bases are separated by three consecutive newline characters ("\n\n\n").
    /// </summary>
    /// <param name="text"> a string </param>
    /// <returns> a list of belief bases in the order in which they appear in the input
    ///         string. </returns>
    /// <exception cref="IOException"> if an IO error occurs </exception>
    /// <exception cref="ParserException"> some parsing exception </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public java.util.List<T> parseListOfBeliefBases(String text) throws ParserException, IOException
    public virtual IList<T> ParseListOfBeliefBases(string Text)
    {
        string[] KbsString = Text.Split("\n\n\n", true);
        List<T> Kbs = new List<T>();
        foreach (string KbString in KbsString)
        {
            if (!KbString.Trim().Length == 0)
            {
                Kbs.Add(this.ParseBeliefBase(new StringReader(KbString)));
            }
        }
        return Kbs;
    }

    /// <summary>
    /// Parses the given text into a list of belief bases of the given type. Belief
    /// bases are separated by the given delimiter.
    /// </summary>
    /// <param name="text"> a string </param>
    /// <param name="delimiter"> for separating belief bases </param>
    /// <returns> a list of belief bases in the order in which they appear in the input
    ///         string. </returns>
    /// <exception cref="IOException"> if an IO error occurs </exception>
    /// <exception cref="ParserException"> some parsing exception </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public java.util.List<T> parseListOfBeliefBases(String text, String delimiter) throws ParserException, IOException
    public virtual IList<T> ParseListOfBeliefBases(string Text, string Delimiter)
    {
        if (Delimiter.Matches(".*" + IllegalDelimitors + ".*"))
        {
            throw new System.ArgumentException("The given delimiter is similar to characters that are likely to appear in formulas. Try using a more unique delimiter.");
        }
        string[] KbsString = Text.Split(Delimiter, true);
        List<T> Kbs = new List<T>();
        foreach (string KbString in KbsString)
        {
            if (!KbString.Trim().Length == 0)
            {
                Kbs.Add(this.ParseBeliefBase(new StringReader(KbString)));
            }
        }
        return Kbs;
    }

    /// <summary>
    /// Parses the file of the given filename into a formula of the given type.
    /// </summary>
    /// <param name="filename"> the name of a file </param>
    /// <returns> a formula </returns>
    /// <exception cref="FileNotFoundException"> if the file is not found </exception>
    /// <exception cref="IOException">           if some IO issue occurred. </exception>
    /// <exception cref="ParserException">       some parsing exceptions may be added here. </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public S parseFormulaFromFile(String filename) throws FileNotFoundException, IOException, ParserException
    public virtual S ParseFormulaFromFile(string Filename)
    {
        StreamReader Reader = new StreamReader(new FileStream(Filename, FileMode.Open, FileAccess.Read));
        S F = this.ParseFormula(Reader);
        Reader.Close();
        return F;
    }

    /// <summary>
    /// Parses the given text into a formula of the given type.
    /// </summary>
    /// <param name="text"> a string </param>
    /// <returns> a formula </returns>
    /// <exception cref="IOException">     if some IO issue occurred. </exception>
    /// <exception cref="ParserException"> some parsing exceptions may be added here. </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public S parseFormula(String text) throws IOException, ParserException
    public virtual S ParseFormula(string Text)
    {
        return this.ParseFormula(new StringReader(Text));
    }

    /// <summary>
    /// Parses the given reader into a formula of the given type.
    /// </summary>
    /// <param name="reader"> a reader </param>
    /// <returns> a formula </returns>
    /// <exception cref="IOException">     if some IO issue occurred. </exception>
    /// <exception cref="ParserException"> some parsing exceptions may be added here. </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public abstract S parseFormula(Reader reader) throws IOException, ParserException;
    public abstract S ParseFormula(Reader reader);

    /// <summary>
    /// Checks whether the given string is a number.
    /// </summary>
    /// <param name="str"> some string </param>
    /// <returns> "true" if the given string can be parsed as a number </returns>
    public static bool IsNumeric(string Str)
    {
        try
        {
            double.Parse(Str);
        }
        catch (System.FormatException)
        {
            return false;
        }
        return true;
    }

}