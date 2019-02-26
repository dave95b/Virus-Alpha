using System;
using System.Collections.Generic;
using System.Linq;

public class MoveRangeCreator {

    private HashSet<HexagonTille> moveSet = new HashSet<HexagonTille>();
    private Queue<HexagonTille> queue = new Queue<HexagonTille>();


    public HashSet<HexagonTille> CreateMoveRange(int range, HexagonTille origin) {
        moveSet.Clear();
        queue.Clear();

        moveSet.Add(origin);
        queue.Enqueue(origin);

        for (int i = 0; i < range; i++) {
            int queueLength = queue.Count;
            for (int j = 0; j < queueLength; j++) {
                HexagonTille next = queue.Dequeue();
                AddTiles(next);
            }
        }

        return moveSet;
    }

    private void AddTiles(HexagonTille origin) {
        foreach (var tile in origin.NeighbourList) {
            if (moveSet.Add(tile))
                queue.Enqueue(tile);
        }
    }
}
