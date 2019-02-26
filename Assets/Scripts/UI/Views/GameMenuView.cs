using UnityEngine;
using UnityEngine.UI;

public class GameMenuView : MonoBehaviour {

    [SerializeField]
    private NativeEvent onGameSaved;

    [SerializeField]
    private Text savedText;
    private Animator savedAnimator;
    private int savedAnimationId;

    [SerializeField]
    private Timer timer;

    CanvasGroup group;
    CanvasGroupFader fader;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        fader = GetComponent<CanvasGroupFader>();
        savedAnimator = savedText.gameObject.GetComponent<Animator>();
        savedAnimationId = Animator.StringToHash("Saved");

        onGameSaved.AddListener(OnGameSaved);
    }

    public void ShowMenu()
    {
        timer.isPaused = true;
        fader.Fade(1f);
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public void CloseMenu()
    {
        timer.isPaused = false;
        fader.Fade(0f);
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    private void OnGameSaved()
    {
        savedAnimator.SetTrigger(savedAnimationId);
    }

    private void OnDestroy()
    {
        onGameSaved.RemoveListener(OnGameSaved);
    }
}
