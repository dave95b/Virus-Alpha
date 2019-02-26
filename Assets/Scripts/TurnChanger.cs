using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnChanger : MonoBehaviour, ISaveable {

    [SerializeField]
    private VirusArray players;

    [SerializeField]
    private VirusValue activeVirus;

    [SerializeField]
    private GameConfiguration gameConfiguration;

    [Space, SerializeField]
    private NativeEvent OnBeforeTurnChange;
    [SerializeField]
    private NativeEvent OnTurnChanged;

    [SerializeField]
    private IntValue currentTurn;

    [Space, SerializeField]
    private NativeEvent onMapGenerated, onGameOver;


    private int playerIndex;


    public void EndTurn()
    {
        if (currentTurn.Value == gameConfiguration.TurnCount && playerIndex == (players.Length - 1))
            onGameOver.Invoke();
        else
            NextTurn();
    }


    private void NextTurn()
    {
        OnBeforeTurnChange.Invoke();

        activeVirus.Value.enabled = false;

        playerIndex = (playerIndex + 1) % players.Length;

        activeVirus.Value = players[playerIndex];
        activeVirus.Value.enabled = true;

        if (playerIndex == 0)
            currentTurn.Value++;

        OnTurnChanged.Invoke();
    }


    private void OnEnable()
    {
        //Wywołane przy starcie, by zaktualizowało się UI
        onMapGenerated.AddListener(OnTurnChanged.Invoke);
    }

    private void OnDisable()
    {
        onMapGenerated.RemoveListener(OnTurnChanged.Invoke);
    }

    public void SaveState(SavedState state)
    {
        state.CurrentTurn = currentTurn.Value;
        state.MapSize = gameConfiguration.MapSize;
        state.TurnTime = gameConfiguration.TurnTime;
        state.TurnCount = gameConfiguration.TurnCount;
    }

    public void ApplyStateEarly(SavedState state)
    {
        if (state.IsLoaded)
        {
            currentTurn.Value = state.CurrentTurn;
            playerIndex = state.CurrentPlayer;
        }
    }

    public void ApplyStateLate(SavedState state)
    {

    }
}
