using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WordPredictionEngine.Domain
{
    public class DomainServices
    {
        private static Dictionary< string, Dictionary< string, int > > wordPredictionDictionary;

        private Dictionary< string, Dictionary< string, int > > WordDictInstance
        {
            get
            {
                return wordPredictionDictionary ?? new Dictionary< string, Dictionary< string, int > >( );
            }
        }
        
        public void LearnFromFiles( string directoryPath )
        {
            IEnumerable< string > allLines = null;
            try
            {
                var files = new DirectoryInfo( directoryPath ).EnumerateFiles( "*.txt" );

                allLines = files.SelectMany( x => File.ReadLines( x.FullName ) );
            }
            catch ( Exception e )
            {
                File.AppendAllText( "~/ErrorLog.txt" ,e.Message +"\nIt Occured On " +DateTime.Now);
                return;
            }
            foreach ( var line in allLines )
            {
                this.LearnFromSentence(line);
            }
        }

        public void LearnFromFile( string filePath )
        {
            IEnumerable<string> allLines = null;
            try
            {
                allLines = File.ReadLines( filePath );
            }
            catch (Exception e)
            {
                File.AppendAllText("~/ErrorLog.txt", e.Message + "\nIt Occured On " + DateTime.Now);
                return;
            }
            foreach (var line in allLines)
            {
                this.LearnFromSentence(line);
            }
        }

        public void LearnFromSentence( string sentence )
        {
            if (!string.IsNullOrEmpty(sentence))
            {
                var words = sentence.Split(' ');
                int cnt = words.Count();
                for (int i = 0; i < cnt; i++)
                {
                    if (!string.IsNullOrEmpty(words[i]))
                    {
                        if (WordDictInstance.ContainsKey(words[i]))
                        {
                            if (i != cnt - 1)
                            {
                                if (WordDictInstance[words[i]].ContainsKey(words[i + 1]))
                                {
                                    WordDictInstance[words[i]][words[i + 1]]++;
                                }
                                else
                                {
                                    WordDictInstance[words[i]].Add(words[i + 1], 0);
                                }
                            }
                            else
                            {
                                WordDictInstance[words[i]].Add(string.Empty, -1);
                            }
                        }
                        else
                        {
                            WordDictInstance.Add(words[i], new Dictionary<string, int>());
                        }
                    }
                }
            }
        }

        public string PredictNextWord( string word )
        {
            return string.Empty;
        }
    }
}