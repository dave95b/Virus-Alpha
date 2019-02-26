using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 190)]
public class GameConfiguration : ScriptableObject {

    public int PlayerCount, MapSize, TurnTime, TurnCount;
}
