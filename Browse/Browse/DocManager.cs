using System;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Browse
{
    class DocManager
    {
        public List<String> GetData(String path)
        {
            Microsoft.Office.Interop.Word.Application word_app = new Microsoft.Office.Interop.Word.Application();
            {
                // Make Word visible (optional).
                word_app.Visible = false;
                // Open the Word document.
                object missing = Type.Missing;
                object filename = path;
                object confirm_conversions = false;
                object read_only = false;
                object add_to_recent_files = false;
                object format = 0;
                Document word_doc =
                    word_app.Documents.Open(ref filename,
                        ref confirm_conversions,
                        ref read_only, ref add_to_recent_files,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref format, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing);
                List<String> data = new List<string>();

                // 1 case can't - can not
                // 2 case missing dots so i don't know the sentence location
                // 3 '#$ any symbols deleted

                int count = word_doc.Words.Count;
                for (int i = 1; i <= count; i++)
                {
                    // Write the word.
                    string text = word_doc.Words[i].Text.ToLower();
                    string fix = Regex.Replace(text, @"^\s*$\n", string.Empty, RegexOptions.Multiline).TrimEnd();

                    if (Regex.IsMatch(fix, "^[a-z0-9а-я]*$"))
                    { /* your code */
                      //Console.WriteLine("Word {0} = {1}", i, fix);
                        if (!string.IsNullOrWhiteSpace(fix)) data.Add(fix);
                    } /*else
                Console.WriteLine("Word {0} = {1}", i, text); */
                }

                // Remove the hyperlinks.
                object index = 1;
                while (word_doc.Hyperlinks.Count > 0)
                {
                    word_doc.Hyperlinks.get_Item(ref index).Delete();
                }

                // Save and close the document without prompting.
                object save_changes = true;
                word_doc.Close(ref save_changes, ref missing, ref missing);

                // Close the word application.
                word_app.Quit();

                // Apply transformation
                data = TransformData(data);
                for (int i = 0; i < data.Count; i++)
                {
                    Console.WriteLine(data[i] + " ");
                }
                MessageBox.Show("Done");
                return data;

            }
        }

        public List<String> TransformData(List<String> data)
        {
            // 1 remove stopwords
            // 2 use stemmer
            string[] stopwords = System.IO.File.ReadAllLines("stopwords.txt");
            if (stopwords.Length == 0) MessageBox.Show("Can't find stopwords");
            Stemmer stemmer = new Stemmer();
            //EnglishStemmer estemmer = new EnglishStemmer();
            List<String> trueData = new List<String>();

            Array.Sort(stopwords, StringComparer.InvariantCulture);

            foreach (String st in data)
            {
                if (Array.BinarySearch(stopwords, st) < 0)
                {
                    stemmer = new Stemmer();
                    for (int i = 0; i < st.Length; i++) stemmer.add(st[i]);
                    stemmer.stem();
                    String u = stemmer.ToString();
                    //String u = estemmer.Stem(st);
                    Console.WriteLine("Stemmed " + u + "\n");
                    trueData.Add(u);
                }
            }
            for(int i = 0; i < trueData.Count; i++)
            {
                Console.Write(trueData[i] + " ");
            }
            return trueData;
        }

    }

}

