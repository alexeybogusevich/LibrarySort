using BenchmarkDotNet.Running;
using KNU.Algorithms.LibrarySortBenchmarks.ServiceTests;

namespace KNU.Algorithms.LibrarySortBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<LibrarySortServiceBenchmarks>();
        }
    }
}
