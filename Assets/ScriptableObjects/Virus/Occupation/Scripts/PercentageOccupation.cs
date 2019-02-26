using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Occupation", menuName = "Gameplay/Virus/Occupation", order = 1)]
public class PercentageOccupation : ScriptableObject {

    [SerializeField]
    private SystemElementList systemElementList;

    [SerializeField]
    private VirusValue virus;



    public float GetPercentageOccupation(SystemElementControl control)
    {
        return systemElementList.PercentageOccupation(virus.Value, control);
    }
}
