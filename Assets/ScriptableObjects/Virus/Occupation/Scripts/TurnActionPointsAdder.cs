using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurnActionPointsAdder", menuName = "Gameplay/Virus/TurnActionPointsAdder", order = 2)]
public class TurnActionPointsAdder : ScriptableObject {

    [SerializeField]
    private int baseActionPoints;

    [SerializeField]
    private SystemElementList systemElementList;

    [SerializeField]
    private VirusValue activeVirus;

    [SerializeField]
    private IntValue pointsPerTurn;

    public int TurnsToSkip;
    

    public void AddActionPoints()
    {
        pointsPerTurn.Value = baseActionPoints + CalculateActionPoints();
        if (TurnsToSkip > 0)
            TurnsToSkip--;
        else
            activeVirus.Value.ActionPoints += pointsPerTurn.Value;
    }
    
    public void OnElementHacked(SystemElementController controller, VirusValue virus)
    {
        pointsPerTurn.Value += controller.SystemElement.HackReward;
    }


    private int CalculateActionPoints()
    {
        int points = 0;
        int elementCount = systemElementList.Value.Count;

        for (int i = 0; i < elementCount; i++)
        {
            var element = systemElementList.Value[i];
            if (element.IsHackedBy(activeVirus))
                points += element.HackReward;
        }

        return points;
    }
}
