using System;
using System.Collections.Generic;
using System.Linq;

namespace day_07
{
  class Program
  {
    static int[] memory;
    static int pc = 0;

    static void Main(string[] args)
    {
      string input = "3,8,1001,8,10,8,105,1,0,0,21,34,51,68,89,98,179,260,341,422,99999,3,9,1001,9,4,9,102,4,9,9,4,9,99,3,9,1002,9,5,9,1001,9,2,9,1002,9,2,9,4,9,99,3,9,1001,9,3,9,102,3,9,9,101,4,9,9,4,9,99,3,9,102,2,9,9,101,2,9,9,1002,9,5,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,99";
      //input = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
      //input = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";

      long maxOut = 0;

      foreach (var c in GetPermutations(new [] { 0, 1, 2, 3, 4 }, 5))
      {
        var combo = c.ToArray();
        var amp = new Amplifier();
        List<int> outputs = new List<int> { 0 };

        Console.WriteLine(string.Join(",", combo));
        memory = input.Split(',').Select(f => int.Parse(f)).ToArray();
        for (int i=0; i<5; i++)
        {
          int nextInput = outputs[0];
          outputs = new List<int>();
          amp.Run(memory, new List<int> { combo[i], nextInput }, outputs);
        }

        if (outputs[0] > maxOut) maxOut = outputs[0];
      }


      Console.WriteLine("ok. " + maxOut);
    }

    //https://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
    static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
      if (length == 1) return list.Select(t => new T[] { t });

      return GetPermutations(list, length - 1)
          .SelectMany(t => list.Where(e => !t.Contains(e)),
              (t1, t2) => t1.Concat(new T[] { t2 }));
    }
  }

  class Amplifier
  {
    int[] memory;
    int pc = 0;
    List<int> inputs;
    List<int> outputs;
    
    public void Run(int[] memory, List<int> inputs, List<int> outputs)
    {
      this.memory = memory;
      this.inputs = inputs;
      this.outputs = outputs;
      this.pc = 0;
      while (memory[pc] % 100 != 99)
      {
        cycle();
      }
    }

    void cycle()
    {
      int v;
      switch (memory[pc] % 100)
      {
        case 1:
          write(3, read(1) + read(2));
          //memory[memory[pc + 3]] = memory[memory[pc + 1]] + memory[memory[pc + 2]];
          pc += 4;
          break;

        case 2:
          write(3, read(1) * read(2));
          //memory[memory[pc + 3]] = memory[memory[pc + 1]] * memory[memory[pc + 2]];
          pc += 4;
          break;

        case 3:
          Console.Write("Input? ");
          if (inputs.Count > 0)
          {
            v = inputs[0];
            Console.WriteLine(v);
            inputs.RemoveAt(0);
          }
          else
          {
            v = int.Parse(Console.ReadLine());
          }
          write(1, v);
          pc += 2;
          break;

        case 4:
          v = read(1);
          Console.WriteLine($"Output: {v}");
          if (outputs != null) outputs.Add(v);
          pc += 2;
          break;

        case 5: // junp-if-true
          v = read(1);
          if (v != 0) { pc = read(2); } else { pc += 3; }
          break;

        case 6: // jump-if-false
          v = read(1);
          if (v == 0) { pc = read(2); } else { pc += 3; }
          break;

        case 7: // less than
          write(3, (read(1) < read(2)) ? 1 : 0);
          pc += 4;
          break;

        case 8: // equals
          write(3, (read(1) == read(2)) ? 1 : 0);
          pc += 4;
          break;

        default:
          break;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="argIdx">1 based</param>
    /// <returns></returns>
    int read(int argIdx)
    {
      string modeStr = memory[pc].ToString().PadLeft(2 + argIdx, '0');
      char modeChar = modeStr[modeStr.Length - 2 - argIdx];

      int val = memory[pc + argIdx];
      if (modeChar == '0') val = memory[val];
      return val;
    }

    void write(int argIdx, int value)
    {
      string modeStr = memory[pc].ToString().PadLeft(2 + argIdx, '0');
      char modeChar = modeStr[modeStr.Length - 2 - argIdx];

      if (modeChar == '1')
        throw new NotImplementedException();
      memory[memory[pc + argIdx]] = value;
    }
  }
}
