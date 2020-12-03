using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using KNU.Algorithms.LibrarySort.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNU.Algorithms.LibrarySortBenchmarks.ServiceTests
{
    public class LibrarySortServiceBenchmarks
    {
        private readonly List<string> testSet;

        public LibrarySortServiceBenchmarks()
        {
            var set = new List<string>();
            var random = new Random();

            for (int i = 0; i < 50000; ++i)
            {
                var sb = new StringBuilder();
                for(int j = 0; j < random.Next(1,50); ++j)
                {
                    sb.Append((char)random.Next(1, 255));
                }
                set.Add(sb.ToString());
            }

            testSet = set;
        }

        private class LibrarySortConfig : ManualConfig
        {
            public LibrarySortConfig()
            {
                AddColumn(StatisticColumn.Min);
                AddColumn(StatisticColumn.Max);
                AddColumn(StatisticColumn.OperationsPerSecond);
            }
        }

        [Benchmark]
        public void Sort100()
        {
            var words = testSet.Take(100).ToList();
            LibrarySortService<string>.Comparer = Comparer<string>.Default;
            LibrarySortService<string>.Sort(words);
        }

        [Benchmark]
        public void Sort500()
        {
            var words = testSet.Take(500).ToList();
            LibrarySortService<string>.Comparer = Comparer<string>.Default;
            LibrarySortService<string>.Sort(words);
        }

        [Benchmark]
        public void Sort1000()
        {
            var words = testSet.Take(1000).ToList();
            LibrarySortService<string>.Comparer = Comparer<string>.Default;
            LibrarySortService<string>.Sort(words);
        }

        [Benchmark]
        public void Sort5000()
        {
            var words = testSet.Take(5000).ToList();
            LibrarySortService<string>.Comparer = Comparer<string>.Default;
            LibrarySortService<string>.Sort(words);
        }

        [Benchmark]
        public void Sort10000()
        {
            var words = testSet.Take(10000).ToList();
            LibrarySortService<string>.Comparer = Comparer<string>.Default;
            LibrarySortService<string>.Sort(words);
        }

        [Benchmark]
        public void Sort50000()
        {
            var words = testSet.Take(50000).ToList();
            LibrarySortService<string>.Comparer = Comparer<string>.Default;
            LibrarySortService<string>.Sort(words);
        }
    }
}
