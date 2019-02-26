using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 191)]
public class GameConfigurationData : ScriptableObject {

    public int[] PlayerCounts, MapSizes, TurnTimes, TurnCounts;
}
