using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu]
public class SavedState : ScriptableObject {

    // Dane gracza
    public PlayerInfo[] PlayerInfoArray;

    // Dane system elementów
    public SystemElementInfo[] SystemElementInfoArray;

    public int CurrentPlayer, CurrentTurn, MapSize, TurnTime, TurnCount, RemainingTurnTime;
    public string Seed;

    public bool IsLoaded;


    public void SetState(SavedState other)
    {
        PlayerInfoArray = other.PlayerInfoArray;
        SystemElementInfoArray = other.SystemElementInfoArray;
        CurrentPlayer = other.CurrentPlayer;
        CurrentTurn = other.CurrentTurn;
        MapSize = other.MapSize;
        TurnTime = other.TurnTime;
        TurnCount = other.TurnCount;
        RemainingTurnTime = other.RemainingTurnTime;
        Seed = other.Seed;
        IsLoaded = other.IsLoaded;
    }
}

[Serializable]
public class PlayerInfo
{
    public string Name;
    public float ColorR, ColorG, ColorB;
    public int ActionPoints, MaxActionPoints;
    public HexagonCoordinates Coordinates;
}

[Serializable]
public class SystemElementInfo
{
    public int OwnerVirus;
    public bool IsHacked, IsDestroyed;
}
