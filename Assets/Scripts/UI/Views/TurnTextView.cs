using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TurnTextView : MonoBehaviour {

    [SerializeField]
    private GameConfiguration gameConfiguration;

    [SerializeField]
    private IntValue currentTurn;

    [SerializeField]
    private NativeEvent onTurnChanged;

    private Text turnText;


    private void Start()
    {
        turnText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        onTurnChanged.AddListener(UpdateTurnText);
    }

    private void OnDisable()
    {
        onTurnChanged.RemoveListener(UpdateTurnText);
    }

    void UpdateTurnText()
    {
        turnText.text = "Turn\n" + currentTurn.Value + " / " + gameConfiguration.TurnCount;
    }
}
