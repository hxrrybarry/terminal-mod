using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace terminal_mod;

internal class Terrain(int xSize, int ySize, int noPoints, float threshold, int seed)
{
    public int XSize { get; } = xSize;
    public int YSize { get; } = ySize;
    public float NumberOfPoints { get; } = noPoints;
    public float Threshold { get; } = threshold;
    private int[,] PointsOnGrid { get; } = new int[noPoints, 2];
    public int Seed { get; } = seed;

    public char[,] TerrainGrid { get; set; } = new char[xSize, ySize];
    public const char TEXTURE = '#';

    private float GetDistanceToNearestPoint(int x, int y)
    {
        // get distance to nearest point by looping and overriding-
        //- the previously recorded nearestDistance if necessary
        float nearestDistance = 1000;
        for (int i = 0; i < NumberOfPoints; i++)
        {
            float distance = MathF.Sqrt(MathF.Pow(x - PointsOnGrid[i, 0], 2) + MathF.Pow(y - PointsOnGrid[i, 1], 2));
            if (distance < nearestDistance)
                nearestDistance = distance;
        }

        return nearestDistance;
    }

    public void GenerateTerrainGrid()
    {
        Random r = new(Seed);

        // choose NumberOfPoints amount of random coordinates to-
        //- sit on the grid for later point-distance calculations
        for (int i = 0; i < NumberOfPoints; i++)
        {
            PointsOnGrid[i, 0] = r.Next(XSize);
            PointsOnGrid[i, 1] = r.Next(YSize);
        }

        // loop through each point (very slow in 3D!) and perform-
        // - a distance calculation which is then evaluated at a threshold-
        // - to determine whether to fill that point with a value
        for (int x = 0; x < XSize; x++)
        {
            for (int y = 0; y < YSize; y++)
            {
                float distance = GetDistanceToNearestPoint(x, y);

                if (x > 15)
                    distance += (50 / x);
                else if (x < 15)
                    distance -= (50 / (x + 1));

                if (distance > Threshold)
                    TerrainGrid[x, y] = TEXTURE;
                else
                    TerrainGrid[x, y] = ' ';
            }
        }
    }

    public override string ToString()
    {
        string result = "";
        for (int x = 0; x < XSize; x++)
        {
            result += '\n';
            for (int y = 0; y < YSize; y++)
            {
                result += TerrainGrid[x, y];
            }
        }

        return result;
    }
}
