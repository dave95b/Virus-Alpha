using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private SavedState state;

    [SerializeField]
    private IntValue currentTurn;

    [SerializeField]
    private GameConfiguration gameConfiguration;

    [SerializeField]
    private PlayerDataArray playerDataArray;

    [SerializeField]
    private SceneChanger sceneChanger;

    [SerializeField]
    private Text alertText;
    private Animator alertAnimator;
    private int alertAnimationId;

    [SerializeField]
    private GameObject loadGameButton;


    private GameStateSerializer serializer;


    private void Start()
    {
        serializer = new GameStateSerializer();

        alertAnimator = alertText.GetComponent<Animator>();
        alertAnimationId = Animator.StringToHash("Alert");

        loadGameButton.SetActive(serializer.FileExists());
    }


    public void NewGame()
    {
        state.IsLoaded = false;
    }

    public void LoadGame()
    {
        SavedState loaded = serializer.LoadFromFile();
        if (!loaded.IsLoaded)
        {
            alertText.gameObject.SetActive(true);
            alertText.text = "Save game could not be loaded :(";
            alertAnimator.SetTrigger(alertAnimationId);
            return;
        }

        state.SetState(loaded);

        PlayerInfo[] playerInfoArray = state.PlayerInfoArray;
        currentTurn.Value = state.CurrentTurn;
        gameConfiguration.PlayerCount = playerInfoArray.Length;
        gameConfiguration.MapSize = state.MapSize;
        gameConfiguration.TurnTime = state.TurnTime;
        gameConfiguration.TurnCount = state.TurnCount;

        playerDataArray.Array = new PlayerData[playerInfoArray.Length];

        for (int i = 0; i < playerInfoArray.Length; i++)
        {
            PlayerInfo info = playerInfoArray[i];
            Color color = new Color(info.ColorR, info.ColorG, info.ColorB, 1f);
            playerDataArray[i] = new PlayerData(info.Name, color);
        }

        sceneChanger.OpenGameScene();
    }
}
