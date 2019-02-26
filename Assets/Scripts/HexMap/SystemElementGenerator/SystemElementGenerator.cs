using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

public class SystemElementGenerator : MonoBehaviour
{
    public List<Tuple<Color, HashSet<HexagonTille>>> ColoredList { get; set; }
    public int BigElementsModifier { get; set; }
    public int AvrageElementsModifier { get; set; }
    public int SmallElementModifier { get; set; }

    [SerializeField]
    private ColorValue componentHexColor;

    [SerializeField]
    private List<SystemElementGeneratorItem> GeneratorItems;

    private List<HexagonTille> elementTiles = new List<HexagonTille>();
    private int currentMapTerritory;

    public SystemElementGeneratorEvent OnSystemElementGenerated;

    
    public void Generate()
    {
        CalculateCountOfSystemElements();

        foreach (var generatorItem in GeneratorItems)
        {
            currentMapTerritory = 0;
            foreach (var coloredTuple in ColoredList)
            {
                GenerateSystemElements(generatorItem, coloredTuple.Item2, coloredTuple.Item1);
                currentMapTerritory++;
            }
        }
    }

    private void GenerateSystemElements(SystemElementGeneratorItem generatorItem, HashSet<HexagonTille> tileList, Color elementColor)
    {
        for (int i = 0; i < generatorItem.CountPerMapModule; i++)
        {
            elementTiles.Clear();
            FindTilesForSystemElement(tileList, generatorItem.Size);

            if (elementTiles.Count == 0)
                return;

            GenerateSystemElement(generatorItem, tileList, ref elementColor);
        }
    }

    private void GenerateSystemElement(SystemElementGeneratorItem generatorItem, HashSet<HexagonTille> source, ref Color elementColor)
    {
        foreach(var tile in elementTiles)
        {
            tile.IsComponent = true;
            tile.SetMainColor(componentHexColor, ref elementColor);
            source.Remove(tile);

            foreach(var neighbour in tile.NeighbourList)
            {
                source.Remove(neighbour);
            }
        }

        Vector3 position = elementTiles[0].transform.position;

        if(elementTiles.Count == 3)
        {
            float minX, maxX, posY;

            minX = maxX = position.x;
            posY = position.y;

            for(int i = 1; i < elementTiles.Count; i++)
            {
                position = elementTiles[i].transform.position;
                float x = position.x;
                float y = position.y;

                if (x < minX)
                    minX = x;
                else if (x > maxX)
                    maxX = x;

                posY += y;
            }

            position = new Vector3((minX + maxX) / 2, posY / 3, 0);
        } 

        var generatedElementInfo = new GeneratedElementInfo()
        {
            MapTerritory = currentMapTerritory,
            Position = position,
            Tiles = new HashSet<HexagonTille>(elementTiles)
        };
        OnSystemElementGenerated.Invoke(generatedElementInfo);
    }

    private void FindTilesForSystemElement(HashSet<HexagonTille> source, int elementSize)
    {
        if (elementSize == 7)
            GetBigElement(source);
        else if (elementSize == 3)
            GetMediumElement(source);
        else
            GetSmallElement(source);
    }

    private void GetBigElement(HashSet<HexagonTille> source)
    {
        int clearRange = 2;

        foreach (var tile in source)
        {
            if (tile.IsClearRange(clearRange))
            {
                elementTiles.Add(tile);
                elementTiles.AddRange(tile.NeighbourList);
                return;
            }
        }
    }

    Predicate<HexagonTille> neighbourInClearRange = neighbour => neighbour.IsClearRange(1);
    private void GetMediumElement(HashSet<HexagonTille> source)
    {
        int clearRange = 1;

        foreach (var tile in source)
        {
            if (tile.IsClearRange(clearRange))
            {
                HexagonTille second = tile.NeighbourList.Find(neighbourInClearRange);

                if (second == null)
                    continue;

                HexagonTille third = null;

                int neighbourCount = tile.NeighbourList.Count;
                for (int i = 0; i < neighbourCount; i++)
                {
                    var neighbour = tile.NeighbourList[i];
                    if (second.NeighbourList.Contains(neighbour) && neighbour.IsClearRange(clearRange))
                    {
                        third = neighbour;
                        break;
                    }
                }

                if (third == null)
                    continue;

                elementTiles.Add(tile);
                elementTiles.Add(second);
                elementTiles.Add(third);
                return;
            }
        }
    }

    private void GetSmallElement(HashSet<HexagonTille> source)
    {
        int clearRange = 1;

        foreach (var tile in source)
        {
            if (tile.IsClearRange(clearRange))
            {
                elementTiles.Add(tile);
                return;
            }
        }
    }

    private void CalculateCountOfSystemElements()
    {
        int tilesCount = 0;
        foreach(var t in ColoredList)
        {
            tilesCount += t.Item2.Count;
        }
        int oneTileElementsCount = tilesCount / 3;
        int BigElementCount = oneTileElementsCount / BigElementsModifier;
        int AvrageElementCount = oneTileElementsCount / AvrageElementsModifier;
        int SmallElementCount = oneTileElementsCount / SmallElementModifier;
        GeneratorItems[0] = new SystemElementGeneratorItem() { CountPerMapModule = BigElementCount, Size = 7 };
        GeneratorItems[1] = new SystemElementGeneratorItem() { CountPerMapModule = AvrageElementCount, Size = 3 };
        GeneratorItems[2] = new SystemElementGeneratorItem() { CountPerMapModule = SmallElementCount, Size = 1 };
    }
}

[System.Serializable]
struct SystemElementGeneratorItem
{
    public int Size, CountPerMapModule;
}
