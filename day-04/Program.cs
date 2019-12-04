using System;
using System.Diagnostics;

namespace day_04
{
  class Program
  {
    static void Main(string[] args)
    {
      int min = 156218;
      int max = 652527;


      Assert(IsValid(112233));
      Assert(!IsValid(123444));
      Assert(IsValid(111122));
      //min = max = 223450;

      // min = max = 123444;
      // min = max = 112222;

      //min = max = 123333;
      //min = max = 588889;

      int count = 0;
      for (int i=min; i<=max; i++)
      {
        if (IsValid(i))
        {
          Console.WriteLine(i);
          count++;
        }
      }

      Console.WriteLine(count);
    }

    private static void Assert(bool v)
    {
      if (!v)
        throw new InvalidOperationException();
    }

    private static bool IsValid(int i)
    {
      string s = i.ToString();
      char last = ' ';

      int run = 1;
      bool doubled = false;
      bool asc = true;
      for (int c = 0; c < s.Length; c++)
      {
        if (s[c] == last)
        {
          run++;
        }
        else
        {
          doubled |= run == 2;
          run = 1;
        }
        //doubled |= (s[c] == last && (c + 1 < s.Length && s[c + 1] != last));

        asc &= s[c] >= last;

        last = s[c];
      }

      return (run == 2 || doubled) && asc;
    }
  }
}
