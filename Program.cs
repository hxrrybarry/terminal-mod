using System;

namespace terminal_mod;

internal class Program
{
    public static void Main()
    {
        Terrain terrain = new(25, 120, 75, 5f, 1);
        terrain.GenerateTerrainGrid();

        Console.WriteLine(terrain.ToString());
    }
}