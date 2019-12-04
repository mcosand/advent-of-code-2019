using System;

namespace day_04
{
  class Program
  {
    static void Main(string[] args)
    {
      int min = 156218;
      int max = 652527;

      //int min = 223450;
      //int max = min;

      int count = 0;
      for (int i=min; i<=max; i++)
      {
        string s = i.ToString();
        char last = ' ';

        bool doubled = false;
        bool asc = true;
        for (int c = 0; c<s.Length; c++)
        {
          doubled |= (s[c] == last);
          asc &= s[c] >= last;
          last = s[c];
        }

        if (doubled && asc) count++;
      }

      Console.WriteLine(count);
    }
  }
}
