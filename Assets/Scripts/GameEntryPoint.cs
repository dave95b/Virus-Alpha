using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour {

    [Header("Data")]

    [SerializeField]
    private SystemElementList systemElementList;
    [SerializeField]
    private ColorList colorList;
    [SerializeField]
    private TurnActionPointsAdder actionPointsAdder;


    [Header("Events")]

    [SerializeField]
    private NativeEvent onTurnChanged;
    [SerializeField]
    private NativeEvent onMapGenerated;
    [SerializeField]
    private ElementActionEvent onElementHacked, onElementDestroyed;



    void Awake () {
        systemElementList.Value.Clear();
        colorList.Value.Clear();
	}

    private void OnEnable()
    {
        onTurnChanged.AddListener(actionPointsAdder.AddActionPoints);
        onElementHacked.AddListener(actionPointsAdder.OnElementHacked);

        onMapGenerated.AddListener(systemElementList.OnMapGenerated);
        onElementDestroyed.AddListener(systemElementList.OnElementDestroyed);
    }

    private void OnDisable()
    {
        onTurnChanged.RemoveListener(actionPointsAdder.AddActionPoints);
        onElementHacked.RemoveListener(actionPointsAdder.OnElementHacked);

        onMapGenerated.RemoveListener(systemElementList.OnMapGenerated);
        onElementDestroyed.RemoveListener(systemElementList.OnElementDestroyed);
    }
}
