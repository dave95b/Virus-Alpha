using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupationMonitor : MonoBehaviour {

    [SerializeField]
    private SystemElementList elementList;

    [SerializeField]
    private ElementActionEvent onElementHacked, onElementDestroyed;

    [SerializeField]
    private NativeEvent onGameOver;

    [SerializeField]
    private GameConfiguration gameConfiguration;


    [Space, SerializeField]
    private float[] occupationLimits;

    public float OccupationLimit { get { return occupationLimits[gameConfiguration.PlayerCount - 2]; } }


    private void OnEnable()
    {
        onElementHacked.AddListener(CheckOccupationLimit);
        onElementDestroyed.AddListener(CheckOccupationLimit);
    }

    private void OnDisable()
    {
        onElementHacked.RemoveListener(CheckOccupationLimit);
        onElementDestroyed.RemoveListener(CheckOccupationLimit);
    }


    private void CheckOccupationLimit(SystemElementController hackedElement, VirusValue virus)
    {
        float occupation = elementList.PercentageOccupation(virus.Value);

        if (occupation >= OccupationLimit)
            onGameOver.Invoke();
    }
}
