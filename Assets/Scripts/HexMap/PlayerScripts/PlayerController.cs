using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISaveable {

    [SerializeField]
    private PlayerDataArray dataArray;

    [SerializeField]
    private Player virusPrefab;

    [SerializeField]
    private VirusValue currentVirus;

    [SerializeField]
    private VirusArray players;

    [SerializeField]
    private CombController combController;

    [SerializeField]
    private RandomValue random;

    [SerializeField]
    private TurnActionPointsAdder actionPointsAdder;


    private void Awake()
    {
        int playerCount = dataArray.Length;
        players.Array = new Player[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            var data = dataArray[i];
            Player virus = Instantiate(virusPrefab, transform);
            virus.Name = data.Name;
            virus.Color = data.Color;

            virus.gameObject.SetActive(false);
            virus.enabled = false;

            players[i] = virus;
        }
    }


    private void InitializePlayers()
    {
        var validTiles = combController.TileList.FindAll(tile => !tile.IsComponent);
        foreach (var player in players)
        {
            var tile = validTiles.GetRandom(random.Value);
            player.Initialize(tile);
            validTiles.Remove(tile);
        }
        players[0].enabled = true;
        currentVirus.Value = players[0];
    }


    public void SaveState(SavedState state)
    {
        state.CurrentPlayer = System.Array.IndexOf(players.Array, currentVirus.Value);

        int playerCount = players.Length;
        PlayerInfo[] playerInfoArray = new PlayerInfo[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            Player virus = players[i];
            Color color = virus.Color;
            var playerInfo = new PlayerInfo()
            {
                Name = virus.Name,
                ColorR = color.r,
                ColorG = color.g,
                ColorB = color.b,
                ActionPoints = virus.ActionPoints,
                MaxActionPoints = virus.MaxActionPoints,
                Coordinates = virus.CurrentTile.HexagonCoordinates
            };

            playerInfoArray[i] = playerInfo;
        }

        state.PlayerInfoArray = playerInfoArray;
    }

    public void ApplyStateEarly(SavedState state)
    {
        if (state.IsLoaded)
            actionPointsAdder.TurnsToSkip = 1;
        else
            actionPointsAdder.TurnsToSkip = players.Length;
    }

    public void ApplyStateLate(SavedState state)
    {
        if (state.IsLoaded)
            LoadPlayersState(state);
        else
            InitializePlayers();
    }

    private void LoadPlayersState(SavedState state)
    {
        var tileList = combController.TileList;
        int tileCount = tileList.Count;
        var tileDictionary = new Dictionary<HexagonCoordinates, HexagonTille>(tileCount);

        for (int i = 0; i < tileCount; i++)
        {
            var tile = tileList[i];
            tileDictionary.Add(tile.HexagonCoordinates, tile);
        }

        for (int i = 0; i < players.Length; i++)
        {
            var playerInfo = state.PlayerInfoArray[i];
            var tile = tileDictionary[playerInfo.Coordinates];
            players[i].Initialize(tile);
            players[i].SetActionPoints(playerInfo.ActionPoints, playerInfo.MaxActionPoints);
        }

        players[state.CurrentPlayer].enabled = true;
        currentVirus.Value = players[state.CurrentPlayer];
    }
}
