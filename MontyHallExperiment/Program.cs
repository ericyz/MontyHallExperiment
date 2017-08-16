using System;
using System.Collections.Generic;
using System.Linq;

namespace MontyHallExperiment
{
    public class Program
    {
        private static Lazy<Random> _random = new Lazy<Random>(() => new Random());
        private static IEnumerable<int> _doorNumbers = new[] { 1, 2, 3 };
        private const int TotalTimes = 10000;
        public static void Main(string[] args)
        {
            var result = new Dictionary<string, int>(){
                {"No Change",0},
                {"Changed", 0}
            };

            for (var i = 0; i < TotalTimes; i++)
            {
                var answer = _doorNumbers.Map(NextInt);
                var firstChoice = _doorNumbers.Map(NextInt);
                var doorToOpen = _doorNumbers.Except(new[] { answer }).Map(NextInt);

                var secondChoice = _doorNumbers.Except(new[] { doorToOpen }).Map(NextInt);

                if (answer == secondChoice)
                {
                    if (firstChoice == secondChoice)
                    {
                        result["No Change"]++;
                    }
                    else
                    {
                        result["Changed"]++;
                    }
                }
            }

            foreach (var keyvalue in result)
            {
                Console.WriteLine($"{keyvalue.Key}, counts: {keyvalue.Value}");
            }
        }

        private static int NextInt(IEnumerable<int> doors) => doors.ElementAt(_random.Value.Next(0, doors.Count()));
    }

    public static class FunctionalExtensions
    {
        public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> func)
        {
            return func(@this);
        }
    }
}
