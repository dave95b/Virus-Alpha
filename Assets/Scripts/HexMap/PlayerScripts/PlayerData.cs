using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct PlayerData {

    public string Name;
    public Color Color;


    public PlayerData(string name, Color color)
    {
        Name = name;
        Color = color;
    }
}
