using UnityEngine;
using UnityEngine.UI;

public class NewTurnView : MonoBehaviour {

    [SerializeField]
    private Text playerText;

    [SerializeField]
    private Image playerImage;

    [SerializeField]
    private VirusValue currentPlayer;

    [SerializeField]
    private NativeEvent onTurnChanged;

    [SerializeField]
    private Timer timer;

    private CanvasGroupFader fader;
    private CanvasGroup group;

    private void Start()
    {
        fader = GetComponent<CanvasGroupFader>();
        group = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        onTurnChanged.AddListener(Show);
    }

    private void OnDisable()
    {
        onTurnChanged.RemoveListener(Show);
    }

    private void Show()
    {
        playerText.text = currentPlayer.Value.Name;
        playerImage.color = currentPlayer.Value.Color;
        group.interactable = true;
        group.blocksRaycasts = true;
        fader.Fade(1f, 0.1f);
    }

    public void Hide()
    {
        group.interactable = false;
        group.blocksRaycasts = false;
        fader.Fade(0f);
        if (timer.gameObject.activeInHierarchy)
            timer.StartCountdown();
    }
}
