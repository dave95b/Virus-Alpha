using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexagonalCoordinateController {

    enum Size { Bigger, Smaller, Equal}

    private HexagonTille tile;

	public HexagonalCoordinateController(HexagonTille tille)
    {
        tile = tille;
    }

    public void ApplyCoordinates()
    {
        tile.HexagonCoordinates = new HexagonCoordinates(0, 0)
        {
            HasCoordinates = true,
            MadeCoordinates = true
        };
        MakeCoordinates(tile);
        Queue<HexagonTille> queue = new Queue<HexagonTille>(tile.NeighbourList);
        HashSet<HexagonTille> hashSet = new HashSet<HexagonTille>(tile.NeighbourList);

        while(queue.Count != 0)
        {
            HexagonTille next = queue.Dequeue();
            MakeCoordinates(next);
            next.HexagonCoordinates = new HexagonCoordinates(next.HexagonCoordinates.X, next.HexagonCoordinates.Z)
            {
                MadeCoordinates = true,
                HasCoordinates = true
            };

            int neighbourCount = next.NeighbourList.Count;
            for (int i = 0; i < neighbourCount; i++)
            {
                var neighbour = next.NeighbourList[i];
                if (neighbour.HexagonCoordinates.MadeCoordinates)
                    continue;

                if (hashSet.Add(neighbour))
                    queue.Enqueue(neighbour);
            }
        }
        hashSet.Clear();
    }

    private void MakeCoordinates(HexagonTille centerTile)
    {
        Vector3 centerPosition = centerTile.transform.position;


        int neighbourCount = centerTile.NeighbourList.Count;
        for (int i = 0; i < neighbourCount; i++)
        {
            var neighbour = centerTile.NeighbourList[i];
            if (neighbour.HexagonCoordinates.HasCoordinates)
                continue;

            Vector3 neighbourPosition = neighbour.transform.position;
            Size ySize = Size.Equal;
            if (neighbourPosition.y > centerPosition.y)
            {
                ySize = Size.Bigger;
            }
            else if (neighbourPosition.y < centerPosition.y)
            {
                ySize = Size.Smaller;
            }

            Size xSize = Size.Smaller;
            if (neighbourPosition.x > centerPosition.x)
            {
                xSize = Size.Bigger;
            }

            int tileX = centerTile.HexagonCoordinates.X;
            int tileZ = centerTile.HexagonCoordinates.Z;
            HexagonCoordinates coordinates;

            if (xSize == Size.Bigger)
            {
                if (ySize == Size.Equal)
                    coordinates = new HexagonCoordinates(tileX + 1, tileZ);
                else if (ySize == Size.Bigger)
                    coordinates = new HexagonCoordinates(tileX, tileZ + 1);
                else
                    coordinates = new HexagonCoordinates(tileX + 1, tileZ - 1);
            }
            else
            {
                if (ySize == Size.Equal)
                    coordinates = new HexagonCoordinates(tileX - 1, tileZ);
                else if (ySize == Size.Bigger)
                    coordinates = new HexagonCoordinates(tileX - 1, tileZ + 1);
                else
                    coordinates = new HexagonCoordinates(tileX, tileZ - 1);
            }

            coordinates.HasCoordinates = true;
            neighbour.HexagonCoordinates = coordinates;
        }
    }
}
