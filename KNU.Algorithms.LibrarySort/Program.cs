using KNU.Algorithms.LibrarySort.Services;
using System;
using System.IO;

namespace KNU.Algorithms.LibrarySort
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = args[0];

            if(!File.Exists(fileName))
            {
                throw new ArgumentException("Input file does not exist");
            }

            var manager = new SortManager(fileName);
            manager.Sort();
        }
    }
}
