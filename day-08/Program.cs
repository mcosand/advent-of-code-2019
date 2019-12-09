using System;
using System.IO;
using System.Linq;

namespace day_08
{
  class Program
  {
    static void Main(string[] args)
    {
      int width = 25; int height = 6; string input = File.ReadAllText("input.txt");

      //width = 2; height = 2; input = "0222112222120000";

      var layers = Enumerable.Range(0, input.Length / (width * height))
        .Select(f => input.Substring(f * width * height, width * height));

      var merged = layers
        .Aggregate((a, b) => new string(a.Zip(b, (x, y) => x == '2' ? y : x).ToArray()));

      var image = string.Join('\n', Enumerable.Range(0, height)
        .Select(f => merged.Substring(f * width, width)))
        .Replace("0", " ").Replace("1", "#");

      Console.WriteLine(image);
    }
  }
}
