using System;
using System.IO;
using System.Linq;

namespace day_01
{
  class Program
  {
    static long GetFuel(long mass)
    {
      if (mass <= 0) return 0;
      long part = Math.Max(0, (long)Math.Floor(mass / 3.0) - 2);
      return part + GetFuel(part);
    }

    static void Main(string[] args)
    {
      //var input = new[] { "100756" };
      var input = File.ReadAllLines("input.txt");
      var sum = input.Select(f => long.Parse(f)).Sum(f => GetFuel(f));
      Console.WriteLine(sum);
    }
  }
}
