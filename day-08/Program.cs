using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_08
{
  class Program
  {
    static void Main(string[] args)
    {
      int width = 25; int height = 6; string input = File.ReadAllText("input.txt");

      //width = 3; height = 2; input = "123456789012";
      
      var outp = Enumerable.Range(0, input.Length / (width * height))
        .Select(f => input.Substring(f * width * height, width * height))
        .OrderBy(f => f.Count(c => c == '0'))
        .Select(f => f.Count(c => c == '1') * f.Count(c => c == '2'))
        .First();
    }
  }
}
