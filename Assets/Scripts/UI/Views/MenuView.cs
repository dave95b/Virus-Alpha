using UnityEngine;

public class MenuView : MonoBehaviour {

    [SerializeField]
    private GameObject nextView, prevView;

    public float prevTransitionTime = 1f;
    public float nextTransitionTime = 1f;
    public bool fadeOnStart = false;
    public float startTransitionTime = 1f;

    private CanvasGroup group;
    private CanvasGroupFader fader;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        fader = GetComponent<CanvasGroupFader>();
        if(fadeOnStart)
        {
            fader.Fade(1f, startTransitionTime);
        }
    }

    public void NextView()
    {
        if(nextView != null)
        {
            SwitchView(nextView, nextTransitionTime);
        }
    }

    public void PrevView()
    {
        if(prevView != null)
        {
            SwitchView(prevView, prevTransitionTime);
        }
    }

    private void SwitchView(GameObject nextView, float transitionTime)
    {
        fader.Fade(0f, transitionTime);
        group.interactable = false;
        group.blocksRaycasts = false;
        
        nextView.SetActive(true);
        CanvasGroup nextGroup = nextView.GetComponent<CanvasGroup>();
        nextGroup.alpha = 0f;
        fader.Fade(nextGroup, 1f, transitionTime);

        nextGroup.interactable = true;
        nextGroup.blocksRaycasts = true;
    }
}
