

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
 *  Copyright 2016 The TweetyProject Team <http://tweetyproject.org/contact/>
 */
namespace TweetyProject.NET.Commons;

/// <summary>
/// This interface contains some configuration options for
/// Tweety.
/// 
/// @author Matthias Thimm
/// </summary>
public interface TweetyConfiguration
{

    //TODO: Add more configuration options:
    //- Set default reasoners globally
    //- Set precision globally
    //- commons.BeliefSet: toggle EQUALS_USES_SIGNATURE
    //- logics.fol.syntax.FolSignature: toggle whether EqualityPredicate and InequalityPredicate are included in all signatures
    //- Improve/use logging levels

    /// <summary>
    /// The possible log levels.
    /// 
    /// @author Matthias Thimm
    /// </summary>
    public sealed class LogLevel
    {
        /// <summary>
        /// TRACE
        /// </summary>
        public static readonly LogLevel TRACE = new LogLevel("TRACE", InnerEnum.TRACE, 5, "trace");
        /// <summary>
        /// DEBUG
        /// </summary>
        public static readonly LogLevel DEBUG = new LogLevel("DEBUG", InnerEnum.DEBUG, 4, "debug");
        /// <summary>
        /// INFO
        /// </summary>
        public static readonly LogLevel INFO = new LogLevel("INFO", InnerEnum.INFO, 3, "info");
        /// <summary>
        /// WARN
        /// </summary>
        public static readonly LogLevel WARN = new LogLevel("WARN", InnerEnum.WARN, 2, "warn");
        /// <summary>
        /// ERROR
        /// </summary>
        public static readonly LogLevel ERROR = new LogLevel("ERROR", InnerEnum.ERROR, 1, "error");
        /// <summary>
        /// FATAL
        /// </summary>
        public static readonly LogLevel FATAL = new LogLevel("FATAL", InnerEnum.FATAL, 0, "fatal");

        private static readonly List<LogLevel> valueList = new List<LogLevel>();

        static LogLevel()
        {
            valueList.Add(TRACE);
            valueList.Add(DEBUG);
            valueList.Add(INFO);
            valueList.Add(WARN);
            valueList.Add(ERROR);
            valueList.Add(FATAL);
        }

        public enum InnerEnum
        {
            TRACE,
            DEBUG,
            INFO,
            WARN,
            ERROR,
            FATAL
        }

        public readonly InnerEnum innerEnumValue;
        private readonly string nameValue;
        private readonly int ordinalValue;
        private static int nextOrdinal = 0;

        /// <summary>
        /// The log level as integer </summary>
        internal Private readonly;
        /// <summary>
        /// The log level as string </summary>
        internal Private readonly;

        /// <summary>
        /// Creates a new LogLevel </summary>
        internal LogLevel(string name, InnerEnum innerEnum, int LevelAsInt, string LevelAsString)
        {
            /// <summary>
            /// levelAsInt </summary>
            this._LevelAsInt = LevelAsInt;
            /// <summary>
            /// levelAsString </summary>
            this._LevelAsString = LevelAsString;

            nameValue = name;
            ordinalValue = nextOrdinal++;
            innerEnumValue = innerEnum;
        }

        /// <summary>
        /// Returns the log level as integer </summary>
        /// <returns> levelAsInt </returns>

        public int levelAsInt()
        {
            return this._LevelAsInt;
        }

        /// <summary>
        /// Returns the log level as string </summary>
        /// <returns> log level as string </returns>
        public string levelAsString()
        {
            return this._LevelAsString;
        }

        /// <summary>
        /// Returns the log level described by the given string </summary>
        /// <param name="s"> string describing log level </param>
        /// <returns> the log level described by the given string </returns>
        public static LogLevel getLogLevel(string S)
        {
            foreach (LogLevel L in LogLevel.Values())
            {
                if (L._LevelAsString.Equals(S.ToLower()))
                {
                    return L;
                }
                try
                {
                    if (L._LevelAsInt == int.Parse(S))
                    {
                        return L;
                    }
                }
                catch (Exception)
                {
                }
            }
            throw new System.ArgumentException("The given string does not represent a log level.");
        }

        public static LogLevel[] Values()
        {
            return valueList.ToArray();
        }

        public int Ordinal()
        {
            return ordinalValue;
        }

        public override string ToString()
        {
            return nameValue;
        }

        public static LogLevel ValueOf(string name)
        {
            foreach (LogLevel enumInstance in LogLevel.valueList)
            {
                if (enumInstance.nameValue == name)
                {
                    return enumInstance;
                }
            }
            throw new System.ArgumentException(name);
        }
    }
}