using UnityEngine;

[CreateAssetMenu(fileName = "Modifiers", menuName = "Gameplay/Elements/Modifiers")]
public class Modifiers : ScriptableObject
{
    public float HackCost;
    public float DestroyCost;
    public float UnhackCost;

    public float HackReward;
    public float DestroyReward;
}

