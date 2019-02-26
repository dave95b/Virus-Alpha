using System.IO;
using UnityEngine;
using Newtonsoft.Json;

//TODO: zrobić asynchroniczny zapis/odczyt
public class GameStateSerializer
{
    private string fileName = "save";
    private string filePath;


    public GameStateSerializer()
    {
#if UNITY_EDITOR
        filePath = Path.Combine(Application.streamingAssetsPath, fileName);
#else
        filePath = Path.Combine(Application.persistentDataPath, fileName);
#endif
    }


    public void SaveToFile(SavedState state)
    {
        string serialized = JsonConvert.SerializeObject(state);

        File.WriteAllText(filePath, serialized);
    }

    public bool FileExists()
    {
        return File.Exists(filePath);
    }

    public SavedState LoadFromFile()
    {
        SavedState state;

        if (!File.Exists(filePath))
        {
            state = ScriptableObject.CreateInstance<SavedState>();
            state.IsLoaded = false;
            return state;
        }

        string content = File.ReadAllText(filePath);
        state = JsonConvert.DeserializeObject<SavedState>(content);
        state.IsLoaded = true;

        return state;
    }
}
