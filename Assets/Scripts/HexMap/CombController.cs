using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombController : MonoBehaviour, ISaveable {

    [SerializeField]
    private NativeEvent onMapGeneratedEvent;

    [SerializeField]
    private GameConfiguration gameConfiguration;

    public string seed;

    [SerializeField]
    private RandomValue random;


    public int MinHolesSize;
    public int MaxHolesSize;
    [Range(0f, 0.5f)]
    public float HolesPercentage;
    
    public int BigElementModifier;
    public int AvrageElementModifier;
    public int SmallElementModifier;

    public HexComb hexComb;
    public GameObject hexTile;
    public ColorList colorList;

    private List<HexagonTille> hexTileList;
    public List<HexagonTille> TileList { get { return hexTileList; } }


    private List<Tuple<Color, List<HexagonTille>>> listOfColoreArea;


    private void Start()
    {
        if (string.IsNullOrEmpty(seed))
            seed = DateTime.Now.Millisecond.ToString();

        random.Value = new System.Random(seed.GetHashCode());

        List<HexComb> connectedCombList = new List<HexComb>();
        HexMapGenerator mapGenerator = new HexMapGenerator()
        {
            CombPrefab = hexComb,
            Size = gameConfiguration.MapSize,
            Rand = random.Value,
            MinHolesSize = MinHolesSize,
            MaxHolesSize = MaxHolesSize,
            HolesPercentage = HolesPercentage,
            Parent = transform
        };
        mapGenerator.CombPrefab.hexTile = hexTile;

        StartCoroutine(ManageTiles(mapGenerator));
    }

    //TODO: sprawdzić, czy da się wykonać kolorowanie i nadawanie koordynatów jednocześnie
    private IEnumerator ManageTiles(HexMapGenerator mapGenerator)
    {
        yield return null;
        yield return mapGenerator.GenerateMap();
        hexTileList = mapGenerator.TileList;

        ColorTiles();
        GenerateSystemElements();

        var hexagonalCoordinateController = new HexagonalCoordinateController(hexTileList.First());
        hexagonalCoordinateController.ApplyCoordinates();

        yield return new WaitForEndOfFrame();
        onMapGeneratedEvent.Invoke();
    }

    private void ColorTiles()
    {
        int ColorAreasCount = colorList.Value.Count;
        listOfColoreArea = new List<Tuple<Color, List<HexagonTille>>>(ColorAreasCount);
        HashSet<HexagonTille> tmpTileSet = new HashSet<HexagonTille>(hexTileList);

        var rand = random.Value;
        for (int i = 0; i < ColorAreasCount; i++)
        {
            listOfColoreArea.Add(new Tuple<Color, List<HexagonTille>>(colorList.Value[i], new List<HexagonTille>()));

            var tile = hexTileList.GetRandom(rand);
            tile.ColorTile(listOfColoreArea[i].Item1);
            listOfColoreArea[i].Item2.Add(tile);
            tmpTileSet.Remove(tile);
        }

        int[] lastChecked = new int[ColorAreasCount];

        while (tmpTileSet.Count != 0)
        {
            for (int i = 0; i < ColorAreasCount; i++)
            {
                var color = listOfColoreArea[i].Item1;
                var coloredList = listOfColoreArea[i].Item2;
                int coloredCount = coloredList.Count;

                for (int j = lastChecked[i]; j < coloredCount; j++)
                {
                    HexagonTille tile = coloredList[j];
                    for (int k = 0; k < tile.NeighbourList.Count; k++)
                    {
                        var neighbour = tile.NeighbourList[k];

                        if (neighbour.IsColored)
                            continue;

                        neighbour.ColorTile(color);
                        coloredList.Add(neighbour);
                        tmpTileSet.Remove(neighbour);
                    }
                }
                lastChecked[i] = coloredCount;
            }
        }
    }

    private void GenerateSystemElements()
    {
        var generator = GetComponent<SystemElementGenerator>();

        var coloredList = new List<Tuple<Color, HashSet<HexagonTille>>>();
        foreach (var colorTuple in listOfColoreArea)
        {
            var color = colorTuple.Item1;
            var hexSet = new HashSet<HexagonTille>(colorTuple.Item2);
            var tuple = new Tuple<Color, HashSet<HexagonTille>>(color, hexSet);

            coloredList.Add(tuple);
        }

        generator.ColoredList = coloredList;
        generator.BigElementsModifier = BigElementModifier;
        generator.AvrageElementsModifier = AvrageElementModifier;
        generator.SmallElementModifier = SmallElementModifier;
        generator.Generate();
    }


    public void SaveState(SavedState state)
    {
        state.Seed = seed;
    }

    public void ApplyStateEarly(SavedState state)
    {
        if (state.IsLoaded)
            seed = state.Seed;
    }

    public void ApplyStateLate(SavedState state)
    {

    }
}
