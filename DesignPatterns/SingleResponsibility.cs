using System.Diagnostics;

namespace DesignPatterns
{
    internal class SingleResponsibility
    {
        public class Journal
        {
            private readonly List<string> entries = new List<string>();

            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{++count}: {text}");
                return count;
            }

            public void RemoveEntry(string text)
            {
                var match = entries.FirstOrDefault(x => x == text);
                if (match != null)
                {
                    entries.Remove(match);
                }
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }


            /*
            * Single responsibility principle
            * better move to another class to for managing the persistence 
            *
            *
            public void Save(string filename)
            {

            }

            public static Journal Load(string filename)
            {

            }

            public static Journal Load(Uri uri)
            {

            }

            */
        }

        public class Persistence
        {
            public void SaveToFile(Journal j, string filename, bool overwrite = false)
            {
                if (overwrite || !File.Exists(filename))
                {
                    File.WriteAllText(filename, j.ToString());
                } else
                {
                    Console.WriteLine("File is not saved, either overwrite is false or file does not exists");
                }
            }
        }


        public static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("First entry of journal");
            j.AddEntry("Second entry of journal");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(j, filename, true);
            // Process.Start(new ProcessStartInfo { FileName = filename, UseShellExecute = true });
        }
    }
}