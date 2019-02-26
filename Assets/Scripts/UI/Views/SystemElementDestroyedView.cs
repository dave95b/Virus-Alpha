using UnityEngine;
using UnityEngine.UI;

public class SystemElementDestroyedView : MonoBehaviour, ISystemElementView
{

    [SerializeField]
    private Text nameText, playerText;

    [SerializeField]
    private Image playerImage;
    
    private VirusValue destroyer;
    private SystemElementController elementController;
    private SystemElement systemElement;

    private CanvasGroupFader fader;

    private void Start()
    {
        fader = GetComponent<CanvasGroupFader>();
    }

    public void Show(SystemElementController elementController)
    {
        this.elementController = elementController;
        systemElement = elementController.SystemElement;

        nameText.text = systemElement.Name;

        playerImage.color = systemElement.OwnerVirus.Color;
        playerText.text = systemElement.OwnerVirus.Name;

        gameObject.SetActive(true);
        fader.Fade(1f);
    }

    public void Hide()
    {
        if(gameObject.activeInHierarchy)
            fader.Fade(0f);
        //gameObject.SetActive(false);
        elementController = null;
        systemElement = null;
    }
}
