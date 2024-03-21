

/*
 *  This file is part of "TweetyProject", a collection of Java libraries for
 *  logical aspects of artificial intelligence and knowledge representation.
 *
 *  TweetyProject is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License version 3 as
 *  published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 *  Copyright 2020 The TweetyProject Team <http://tweetyproject.org/contact/>
 */
namespace TweetyProject.NET.Commons.util;

/// 
/// <summary>
/// Generates an overview of example classes and resources in the workspace with
/// HTML formatting.
/// 
/// @author Anna Gessler
/// 
/// </summary>
public class ExamplesHTMLGenerator
{

    private static string _GitPath = "https://github.com/TweetyProjectTeam/TweetyProject/tree/main/";

    /// <summary>
    /// Generates SourceForge urls for example classes.
    /// </summary>
    private static string _GenerateSFExamplePath(string Modulename)
    {
        return Modulename + "/src/main/java/" + Modulename.Replace("-", "/");
    }

    /// <summary>
    /// Generates SourceForge urls for resources.
    /// </summary>
    private static string _GenerateSFResourcePath(string Modulename)
    {
        return Modulename + "/src/main/";
    }

    // HTML templates for the table of contents
    private static string _IndexTemplate = "<ul>\n" + "	<li><a href='#sec-general'>General Libraries</a>\n" + "	<ul> $GENERAL_LIBRARIES_LIST </ul>\n" + "	</li>\n" + "	<li><a href='#sec-logic'>Logic Libraries</a>\n" + "	<ul> $LOGIC_LIBRARIES_LIST </ul>\n" + "	</li>\n" + "	<li><a href='#sec-lp'>Logic Programming Libraries</a>\n" + "	<ul> $LP_LIBRARIES_LIST </ul>\n" + "	</li>\n" + "	<li><a href='#sec-arg'>Argumentation Libraries</a>\n" + "	<ul> $ARG_LIBRARIES_LIST </ul>\n" + "	</li>\n" + "	<li><a href='#sec-agent'>Agent Libraries</a>\n" + "	<ul> $AGENT_LIBRARIES_LIST </ul>\n" + "	</li>\n" + "	<li><a href='#sec-other'>Other Libraries</a>\n" + "	<ul> $OTHER_LIBRARIES_LIST </ul>\n" + "	</li>\n" + "</ul>";
    private static string _IndexItemTemplate = "\n<li><a href='#$MODULELINK'>$MODULENAME</a> (<span style='font-family: Courier'>$MODULEPATH</span>)</li>";

    // HTML template for the library sections
    private static string _LibrariesTemplate = "<hr noshade='noshade' size='1' />\n" + "<a name='sec-general'></a><h3>General Libraries</h3>\n" + "$GENERAL_LIBRARIES\n" + "\n" + "<hr noshade='noshade' size='1' />\n" + "<a name='#sec-logic'></a><h3>Logic Libraries</h3>\n" + "$LOGIC_LIBRARIES\n" + "\n" + "<hr noshade='noshade' size='1' />\n" + "<a name='sec-lp'></a><h3>Logic Programming Libraries</h3>\n" + "$LP_LIBRARIES\n" + "\n" + "<hr noshade='noshade' size='1' />\n" + "<a name='sec-arg'></a><h3>Argumentation Libraries</h3>\n" + "$ARG_LIBRARIES\n" + "\n" + "<hr noshade='noshade' size='1' />\n" + "<a name='sec-agent'></a><h3>Agent Libraries</h3>\n" + "$AGENT_LIBRARIES\n" + "\n" + "<hr noshade='noshade' size='1' />\n" + "<a name='sec-other'></a><h3>Other Libraries</h3>\n" + "$OTHER_LIBRARIES";

    // HTML templates for the examples and resources of individual libraries
    private static string _ModuleTemplate = "\n<a name='$MODULELINK'></a><h4>$MODULENAME (<span style='font-family: Courier'>$MODULEPATH</span>)</h4>\n" + "Example code: $EXAMPLES \n Resources: $RESOURCES";
    private static string _ResourcesTemplate = "\n" + "<li><a target='_blank' href='" + _GitPath + "$MODULE_SF_PATH/resources/$EXAMPLENAME'><tt>resources.$EXAMPLENAME</tt></a>$DESCRIPTION</li>";
    private static string _ExamplesTemplate = "\n<li><a target='_blank' href='" + _GitPath + "$MODULE_SF_PATH/examples/$EXAMPLENAME'><tt>examples.$EXAMPLENAME</tt></a>$DESCRIPTION</li>";
    private static string _ResourcesEmpty = "<p> <i>no resources available</i> </p>";
    private static string _ExamplesEmpty = "<p> <i>no example code available</i> </p>";

    /// <summary>
    /// Generates an overview of example classes and resources in the workspace with
    /// HTML formatting.
    /// </summary>
    /// <param name="tweety_libraries_dir"> path of the TweetyProject 'libraries' folder (can be
    ///                             detected automatically if left empty) </param>
    /// <returns> String containing an overview of examples and resources with HTML
    ///         formatting </returns>
    /// <exception cref="IOException"> </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: private static String generateHTMLOverview(String tweety_libraries_dir) throws java.io.IOException
    private static string _GenerateHTMLOverview(string TweetyLibrariesDir)
    {
        if (TweetyLibrariesDir.Trim().Length == 0)
        {
            TweetyLibrariesDir = System.GetProperty("user.dir");
            TweetyLibrariesDir = TweetyLibrariesDir.Substring(0, TweetyLibrariesDir.LastIndexOf("/", StringComparison.Ordinal));
        }

        // variables that will be used to replace the keywords of the same names
        // in the table of contents HTML template
        string Index = _IndexTemplate;
        string GeneralLibrariesList = "";
        string LogicLibrariesList = "";
        string LpLibrariesList = "";
        string ArgLibrariesList = "";
        string AgentLibrariesList = "";
        string OtherLibrariesList = "";

        // variables that will be used to replace the keywords of the same names
        // in the libraries template
        string Listings = _LibrariesTemplate;
        string GeneralLibraries = "";
        string LogicLibraries = "";
        string LpLibraries = "";
        string ArgLibraries = "";
        string AgentLibraries = "";
        string OtherLibraries = "";

        File[] TweetyDirs = (new File(TweetyLibrariesDir)).ListFiles();
        // this map is used to store and sort the libraries and their corresponding
        // examples and resources
        SortedDictionary<string, Pair<string, string>> LibraryItems = new SortedDictionary<string, Pair<string, string>>();
        if (TweetyDirs != null)
        {
            foreach (File Child in TweetyDirs)
            {
                if (Child.IsDirectory() && !Child.GetName().Contains(".settings") && !Child.GetName().Contains(".git") && !Child.GetName().Contains("logo") && !Child.GetName().Equals("org-tweetyproject"))
                {

                    string MODULEPATH = Child.GetName();
                    string MODULENAME = MODULEPATH;

                    File[] Contents = Child.ListFiles();

                    // Variables that will be used to replace the keywords of the same names
                    // in the examples and resources HTML template
                    string EXAMPLES = "";
                    string RESOURCES = "";

                    foreach (File C in Contents)
                    {
                        // Get full library name from POM file
                        if (C.GetName().Contains("pom"))
                        {
                            Scanner Reader = new Scanner(C);
                            while (Reader.HasNextLine())
                            {
                                string Line = Reader.NextLine();
                                if (Line.IndexOf("<name>", StringComparison.Ordinal) != -1)
                                {
                                    var TempVar = Line.IndexOf("(", System.StringComparison.Ordinal) + 1;
                                    MODULENAME = Line.Substring(TempVar, Line.IndexOf(")", StringComparison.Ordinal) - (TempVar));
                                    break;
                                }
                            }
                            Reader.close();
                        }

                        // Collect examples and resources file names recursively
                        IList<Path> Examples = new List<Path>();
                        IList<Path> Resources = new List<Path>();
                        Files.Walk(Paths.Get(C.GetAbsolutePath())).Filter(Files.isRegularFile).ForEach((f) =>
                        {
                            string File = f.ToString();
                            if (File.Contains("resources") && !File.Contains(".prefs") && !File.Contains("javadoc-options-javadoc-resources.xml"))
                            {
                                Resources.Add(f);
                            }
                            else if (File.Contains("examples") && !File.Contains(".class"))
                            {
                                Examples.Add(f);
                            }
                        });

                        // sort file names alphabetically
                        Examples.Sort((a, b) => string.CompareOrdinal(a.GetFileName().ToString(), b.GetFileName().ToString()));
                        Resources.Sort((a, b) => string.CompareOrdinal(a.GetFileName().ToString(), b.GetFileName().ToString()));

                        // Parse example descriptions from javadoc
                        foreach (Path P in Examples)
                        {
                            string Example = _ExamplesTemplate.Replace("$EXAMPLENAME", P.GetFileName().ToString());
                            string Description = "";
// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @SuppressWarnings("resource") String doc = new java.util.Scanner(p).useDelimiter("\\Z").next();
                            string Doc = (new Scanner(P)).UseDelimiter("\\Z").Next();
                            if (Doc.Contains("public class"))
                            {
                                if (Doc.IndexOf("Copyright", StringComparison.Ordinal) == -1)
                                {
                                    throw new System.ArgumentException("The following class file is missing its license comment, please fix this first: " + P);
                                }
                                try
                                {
                                    var TempVar2 = Doc.IndexOf("Copyright", System.StringComparison.Ordinal);
                                    Doc = Doc.Substring(TempVar2, Doc.IndexOf("public class", StringComparison.Ordinal) - TempVar2).Trim();
                                }
                                catch (Exception E)
                                {
                                    Console.WriteLine(P);
                                    throw new Exception(E);
                                }
                                if (Doc.Contains("/**"))
                                {
                                    string Spl = Doc.Substring(Doc.IndexOf("/**", StringComparison.Ordinal) + 1).Trim();
                                    string[] Lines = Spl.Split("\n", true);

                                    // Remove comment characters and ignore lines with javadoc tags
                                    foreach (string S in Lines)
                                    {
                                        if (S.Contains("@") || S.StartsWith("import", StringComparison.Ordinal))
                                        {
                                            continue;
                                        }
                                        string Line = S.ReplaceAll("[\\*]", "");
                                        Line = Line.Replace("\n", " ").Replace("\r", " ").Replace(" /", "");
                                        Description += Line;
                                    }
                                    if (!Description.Trim().Length == 0)
                                    {
                                        Description = ": " + Description;
                                    }
                                }
                            }
                            EXAMPLES += Example.Replace("$DESCRIPTION", Description);
                        }

                        foreach (Path P in Resources)
                        {
                            RESOURCES += _ResourcesTemplate.Replace("$EXAMPLENAME", P.GetFileName().ToString()).Replace("$DESCRIPTION", "");
                        }

                        // replace keywords in HTML template
                        RESOURCES = RESOURCES.Replace("$MODULEPATH", MODULEPATH);
                        EXAMPLES = EXAMPLES.Replace("$MODULEPATH", MODULEPATH);
                        RESOURCES = RESOURCES.Replace("$MODULE_SF_PATH", _GenerateSFResourcePath(MODULEPATH));
                        EXAMPLES = EXAMPLES.Replace("$MODULE_SF_PATH", _GenerateSFExamplePath(MODULEPATH));

                    }

                    if (EXAMPLES.Equals(""))
                    {
                        EXAMPLES = _ExamplesEmpty;
                    }
                    else
                    {
                        EXAMPLES = "<ul>" + EXAMPLES + "</ul>";
                    }
                    if (RESOURCES.Equals(""))
                    {
                        RESOURCES = _ResourcesEmpty;
                    }
                    else
                    {
                        RESOURCES = "<ul>" + RESOURCES + "</ul>";
                    }

                    // replace keywords in HTML template
                    string MODULELINK = "lib-" + MODULENAME.ToLower().Replace(" ", "-");
                    string Item = _IndexItemTemplate;
                    Item = Item.Replace("$MODULENAME", MODULENAME);
                    Item = Item.Replace("$MODULEPATH", MODULEPATH.Replace("-", "."));
                    Item = Item.Replace("$MODULELINK", MODULELINK);
                    string ModuleItem = _ModuleTemplate;
                    ModuleItem = ModuleItem.Replace("$MODULENAME", MODULENAME);
                    ModuleItem = ModuleItem.Replace("$MODULEPATH", MODULEPATH.Replace("-", "."));
                    ModuleItem = ModuleItem.Replace("$MODULELINK", MODULELINK);
                    ModuleItem = ModuleItem.Replace("$EXAMPLES", EXAMPLES);
                    ModuleItem = ModuleItem.Replace("$RESOURCES", RESOURCES);

                    LibraryItems[MODULEPATH] = new Pair<string, string>(Item, ModuleItem);
                }
            }
        }
        else
        {
            throw new System.ArgumentException(TweetyLibrariesDir + " is not a valid TweetyProject directory");
        }

        // Put collected libraries together
        foreach (KeyValuePair<string, Pair<string, string>> I in LibraryItems.SetOfKeyValuePairs())
        {
            string MODULEPATH = I.Key;
            string Item = I.Value.GetFirst();
            string ModuleItem = I.Value.GetSecond();

            // sort modules into categories
            if (MODULEPATH.Contains("-logics-"))
            {
                LogicLibrariesList += Item;
                LogicLibraries += ModuleItem;
            }
            else if (MODULEPATH.Contains("-arg-"))
            {
                ArgLibrariesList += Item;
                ArgLibraries += ModuleItem;
            }
            else if (MODULEPATH.Contains("-lp-"))
            {
                LpLibrariesList += Item;
                LpLibraries += ModuleItem;
            }
            else if (MODULEPATH.Contains("-agents"))
            {
                AgentLibrariesList += Item;
                AgentLibraries += ModuleItem;
            }
            else if (MODULEPATH.Contains("commons") || MODULEPATH.Contains("plugin") || MODULEPATH.Contains("cli") || MODULEPATH.Contains("comparator") || MODULEPATH.Contains("math") || MODULEPATH.Contains("graphs"))
            {
                GeneralLibrariesList += Item;
                GeneralLibraries += ModuleItem;
            }
            else
            {
                OtherLibrariesList += Item;
                OtherLibraries += ModuleItem;
            }
        }

        // replace keywords in HTML template
        Index = Index.Replace("$LOGIC_LIBRARIES_LIST", LogicLibrariesList);
        Index = Index.Replace("$ARG_LIBRARIES_LIST", ArgLibrariesList);
        Index = Index.Replace("$LP_LIBRARIES_LIST", LpLibrariesList);
        Index = Index.Replace("$AGENT_LIBRARIES_LIST", AgentLibrariesList);
        Index = Index.Replace("$GENERAL_LIBRARIES_LIST", GeneralLibrariesList);
        Index = Index.Replace("$OTHER_LIBRARIES_LIST", OtherLibrariesList);
        Listings = Listings.Replace("$LOGIC_LIBRARIES", LogicLibraries);
        Listings = Listings.Replace("$ARG_LIBRARIES", ArgLibraries);
        Listings = Listings.Replace("$LP_LIBRARIES", LpLibraries);
        Listings = Listings.Replace("$AGENT_LIBRARIES", AgentLibraries);
        Listings = Listings.Replace("$GENERAL_LIBRARIES", GeneralLibraries);
        Listings = Listings.Replace("$OTHER_LIBRARIES", OtherLibraries);

        return Index + Listings;
    }

    /// <summary>
    /// Generates an overview of example classes and resources in the workspace with
    /// HTML formatting and writes it to a HTML file.
    /// </summary>
    /// <param name="path"> where the generated file will be saved </param>
    /// <exception cref="IOException"> if an IO error occurs </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public static void printExamplesToHtmlFile(String path) throws java.io.IOException
    public static void PrintExamplesToHtmlFile(string Path)
    {
        StreamWriter Writer = new StreamWriter(Path + "examples.html");
        // the path parameter can be left empty if this class is part of the same local
        // TweetyProject repository that the generator is supposed to generate the overview from
        // otherwise, use the path to a TweetyProject 'libraries' folder
        Writer.Write(_GenerateHTMLOverview(""));
        Writer.Close();
    }

    /// <param name="args"> String </param>
    /// <exception cref="IOException"> throws </exception>
// JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
// ORIGINAL LINE: public static void main(String[] args) throws java.io.IOException
    public static void Main(string[] Args)
    {
        PrintExamplesToHtmlFile("/Users/mthimm/Downloads/");
    }

}