using System;
using System.Collections.Generic;
using System.IO;

namespace KNU.Algorithms.LibrarySort.Services
{
    public class SortManager
    {
        private readonly string input;

        private const int chunk = 1024;

        public SortManager(string filename)
        {
            this.input = filename;
        }

        public void Sort()
        {
            try
            {
                ExecuteDefaultSorting();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ExecuteIOSorting(chunk);
            }
        }

        private void ExecuteDefaultSorting()
        {
            using var reader = new StreamReader(File.OpenRead(input));
            var words = new List<string>();

            while (reader.Peek() > -1)
            {
                words.AddRange(reader.ReadLine().Split(" "));
            }

            reader.Close();

            LibrarySortService<string>.Comparer = Comparer<string>.Default;
            LibrarySortService<string>.Sort(words);

            Console.WriteLine("Regular Success");

            foreach (var word in words)
            {
                Console.Write(word + " ");
            }
        }

        private void ExecuteIOSorting(int fileSize)
        {
            using var reader = new StreamReader(File.OpenRead(input));
            var nums = new List<string>();

            int filesCount = 0;
            while (reader.Peek() > -1)
            {
                var words = new List<string>();

                for (int i = 0; (i < fileSize) && (reader.Peek() > -1); i++)
                {
                    words.AddRange(reader.ReadLine().Split(" "));
                }

                LibrarySortService<string>.Comparer = Comparer<string>.Default;
                LibrarySortService<string>.Sort(words);

                FileManager.WriteToFile(filesCount, words);

                filesCount++;
            }

            reader.Close();
            FileManager.MergeFiles(filesCount);
        }
    }
}
