using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RenamePacktFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            TextInfo textInfo = new CultureInfo("en-US").TextInfo;

            string thePath = "../../../Downloads/Packt";
            Console.WriteLine(thePath);
            dynamic fileList = System.IO.Directory.GetFiles(thePath, "*.pdf");
            foreach (dynamic file_loopVariable in fileList)
            {
                dynamic file = file_loopVariable;
                if (!file.EndsWith(".pdf")) continue; // Slightly redundant
                if (!string.IsNullOrWhiteSpace(file))
                {
                    string fileName = System.IO.Path.GetFileName(file);
                    string filePrefix = System.IO.Path.GetFileNameWithoutExtension(file);
                    string filePath = System.IO.Path.GetDirectoryName(file);
                    string[] splits = filePrefix.Split('-');

                    string fileToTest = String.Format("{0}/{1}_code.zip", filePath, splits[0]);

                    if (System.IO.File.Exists(fileToTest) && splits.Length == 2 & Regex.IsMatch(splits[0], @"^\d+$") & splits[0].Length == 13)
                    {
                        dynamic fileToRenameTo = System.IO.Path.Combine(filePath, splits[1] + "_code.zip");
                        try
                        {
                            System.IO.File.Move(System.IO.Path.Combine(filePath, filePrefix + ".pdf"), System.IO.Path.Combine(filePath, ToProperCase(splits[1].Replace("_", " ")) + ".pdf"));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        System.IO.File.Move(System.IO.Path.Combine(filePath, splits[0] + "_code.zip"), System.IO.Path.Combine(filePath, ToProperCase(splits[1].Replace("_", " ")) + "_code.zip"));
                    }
                    else if (splits.Length == 2 & Regex.IsMatch(splits[0], @"^\d+$") & splits[0].Length == 13)
                    {
                        //'Console.Write("REN " + filePrefix + ".pdf """ + StrConv(splits[1], VbStrConv.ProperCase).Replace("_", " ") +".pdf""")
                        try
                        {
                            System.IO.File.Move(System.IO.Path.Combine(filePath, filePrefix + ".pdf"), System.IO.Path.Combine(filePath, ToProperCase(splits[1]).Replace("_", " ")) + ".pdf");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    } // elseif
                }
            } // foreach
        } // main

        public static String ToProperCase(String s)
        {
            if (s == null) return s;

            String[] words = s.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]);
                String rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return String.Join(" ", words);
        }
    } // class

}; // namespace