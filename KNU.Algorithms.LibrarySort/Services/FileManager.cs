using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KNU.Algorithms.LibrarySort.Services
{
    public static class FileManager
    {
        public static void WriteToFile(int fileNum, List<string> words)
        {
            using var writer = new StreamWriter(File.OpenWrite($"output{fileNum}.txt"));

            foreach (var word in words)
            {
                writer.Write(word + "\n");
            }
        }

        public static void MergeFiles(int filesTotal)
        {
            using var writer = new StreamWriter(File.OpenWrite("output.txt"));
            var readers = new List<StreamReader>();
            var words = new List<string>();

            for (int i = 0; i < filesTotal; i++)
            {
                using var currentWriter = new StreamReader(File.OpenRead($"output{i}.txt"));
                readers.Add(currentWriter);
                words.Add(currentWriter.ReadLine());
            }

            while (words.Any(w => w != null))
            {
                int minIndex = FindMinIndex(words);

                writer.Write(words[minIndex] + " ");
                var reader = readers[minIndex];

                if (reader.Peek() > -1)
                {
                    words.RemoveAt(minIndex);
                    words[minIndex] = reader.ReadLine().Trim();
                }
                else
                {
                    words.RemoveAt(minIndex);
                    words[minIndex] = null;
                }
            }
        }

        private static int FindMinIndex(List<string> words)
        {
            var firstWord = words[0];
            int index = 0;
            int i = 0;

            while (firstWord == null)
            {
                firstWord = words[++i];
                index++;
            }

            for (int j = i + 1; j < words.Count; j++)
            {
                var newWord = words[j];

                if (newWord == null)
                {
                    continue;
                }

                if (!newWord.Equals(firstWord))
                {
                    firstWord = newWord;
                    index = j;
                }
            }
            return index;
        }
    }
}
