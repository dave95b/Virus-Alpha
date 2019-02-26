using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveController : MonoBehaviour {

    [SerializeField]
    private SavedState savedState;

    private ISaveable[] saveables;

    [SerializeField]
    private NativeEvent onMapGenerated, onGameSaved;

    private GameStateSerializer serializer;


    private void Awake()
    {
        // Nie śmiejcie się z tego :(
        saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToArray();
        serializer = new GameStateSerializer();

        ApplyStateBeforeMapGeneration();
        onMapGenerated.AddListener(ApplyStateAfterMapGeneration);
    }


    private void ApplyStateBeforeMapGeneration()
    {
        foreach (var saveable in saveables)
            saveable.ApplyStateEarly(savedState);
    }

    private void ApplyStateAfterMapGeneration()
    {
        foreach (var saveable in saveables)
            saveable.ApplyStateLate(savedState);
    }

    public void SaveGameState()
    {
        foreach (var saveable in saveables)
            saveable.SaveState(savedState);

        serializer.SaveToFile(savedState);

        onGameSaved.Invoke();
    }


    private void OnDestroy()
    {
        onMapGenerated.RemoveListener(ApplyStateAfterMapGeneration);
    }
}
