using System;
using System.Collections.Generic;
using System.IO;

namespace day_06
{
  class Program
  {
    class SpaceObject
    {
      public SpaceObject(string id)
      {
        Id = id;
      }
      public readonly string Id;
      public SpaceObject Parent = null;
      public List<SpaceObject> Satellites = new List<SpaceObject>();

      public override string ToString()
      {
        return Id;
      }
    }

    static void Main(string[] args)
    {
      var roots = new List<SpaceObject>();
      var lookup = new Dictionary<string, SpaceObject>();

      var input = File.ReadAllLines("input.txt");
      //input = "COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L".Split('\n');

      foreach (var line in input)
      {
        var ids = line.Split(')');

        if (!lookup.TryGetValue(ids[0], out SpaceObject parent))
        {
          parent = new SpaceObject(ids[0]);
          roots.Add(parent);
          lookup.Add(ids[0], parent);
        }

        if (!lookup.TryGetValue(ids[1], out SpaceObject satellite))
        {
          satellite = new SpaceObject(ids[1]);
          lookup.Add(ids[1], satellite);
        }
        else if (satellite.Parent == null)
        {
          roots.Remove(satellite);
        }

        parent.Satellites.Add(satellite);
        satellite.Parent = parent;
      }

      int count = 0;
      foreach (var obj in lookup.Values)
      {
        SpaceObject o = obj;
        while (o.Parent != null)
        {
          count++;
          o = o.Parent;
        }
      }

      Console.WriteLine(count);
    }

  }
}
