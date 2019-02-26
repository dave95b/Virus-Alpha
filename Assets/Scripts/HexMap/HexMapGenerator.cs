using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexMapGenerator {
    
    public int MinHolesSize { get; set; }
    public int MaxHolesSize { get; set; }
    public float HolesPercentage { get; set; }
    public HexComb CombPrefab { get; set; }
    public int Size { get; set; }
    public System.Random Rand { get; set; }
    public List<HexagonTille> TileList { get; set; }
    public Transform Parent { get; set; }

    private HexComb comb;
    private int holesCount;

    public IEnumerator GenerateMap()
    {
        comb = Object.Instantiate(CombPrefab, new Vector3(0, 0, 0), Quaternion.identity, Parent);
        comb.layersOfComb = Size;
        comb.Generate();
        var tmp = comb.HexTiles;
        TileList = new List<HexagonTille>();
        foreach(var t in tmp)
        {
            TileList.Add(t.GetComponent<HexagonTille>());
        }
        holesCount = (int)(HolesPercentage * TileList.Count / ((MinHolesSize + MaxHolesSize) / 2));

        yield return new WaitForFixedUpdate();

        foreach(var v in TileList)
        {
            v.SortNeighbours();
        }

        MakeHoles();
        RemoveIslands();
    }

    private void MakeHoles()
    {
        //TileList.Sort(); //okazało się, że przy aktualnej implementacji komparatora nie trzeba sortować tej listy, by korzystać z wyszukiwania binarnego

        List<HexagonTille> holeTileList;
        for(int i = 0; i < holesCount; i++)
        {
            int thisHoleSize = Rand.Next(MinHolesSize, MaxHolesSize + 1);

            holeTileList = new List<HexagonTille>()
            {
                TileList.GetRandom(Rand)
            };
            
            for(int j = 0;j<thisHoleSize - 1; j++)
            {
                var toSearchForNeighbours = holeTileList.GetRandom(Rand).NeighbourList;
                for(int k = 0; k < toSearchForNeighbours.Count;k++)
                {
                    if (!holeTileList.Contains(toSearchForNeighbours[k]))
                    {
                        holeTileList.Add(toSearchForNeighbours[k]);
                        break;
                    }
                }
            }

            foreach(var holeTile in holeTileList)
            {
                RemoveTile(holeTile);
                foreach (var holeNeighbour in holeTile.NeighbourList)
                {
                    holeNeighbour.NeighbourList.Remove(holeTile);
                }
                Object.Destroy(holeTile.gameObject);
            }
        }
    }

    private void RemoveIslands()
    {
        var nonCheckedTiles = new HashSet<HexagonTille>(TileList);
        var islandTiles = new HashSet<HexagonTille>(TileList);
        islandTiles.Clear();

        do
        {
            var tile = nonCheckedTiles.First();
            nonCheckedTiles.Remove(tile);

            GenerateIsland(tile, islandTiles, nonCheckedTiles);

            if (islandTiles.Count >= TileList.Count / 2)
            {
                DeleteTiles(nonCheckedTiles);
                break;
            } else
                DeleteTiles(islandTiles);

        } while (islandTiles.Count != TileList.Count);
    }

    private void GenerateIsland(HexagonTille source, HashSet<HexagonTille> islandTiles, HashSet<HexagonTille> nonCheckedTiles)
    {
        islandTiles.Clear();
        islandTiles.Add(source);
        var islandCopy = new List<HexagonTille>(islandTiles);

        bool added = true;

        while (added)
        {
            added = false;
            for (int i = 0; i < islandCopy.Count; i++)
            {
                var islandTile = islandCopy[i];

                var neighbourList = islandTile.NeighbourList;
                int neighbourCount = neighbourList.Count;

                for (int j = 0; j < neighbourCount; j++)
                {
                    var neighbour = neighbourList[j];
                    if (islandTiles.Add(neighbour))
                    {
                        added = true;
                        islandCopy.Add(neighbour);
                        nonCheckedTiles.Remove(neighbour);
                    }
                }
            }
        }
    }

    private void DeleteTiles(HashSet<HexagonTille> tiles)
    {
        foreach (var tile in tiles)
        {
            RemoveTile(tile);
            Object.Destroy(tile.gameObject);
        }
    }

    private void RemoveTile(HexagonTille tile)
    {
        int tileIndex = TileList.BinarySearch(tile);
        TileList.RemoveAt(tileIndex);
    }
}
