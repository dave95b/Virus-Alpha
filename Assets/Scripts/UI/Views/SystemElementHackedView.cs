using UnityEngine;
using UnityEngine.UI;

public class SystemElementHackedView : MonoBehaviour, ISystemElementView
{

    [SerializeField]
    private Text nameText, playerText, unhackCostText;

    [SerializeField]
    private GameObject unhackTextObject, unhackCostObject;

    [SerializeField]
    private Image playerImage;

    [SerializeField]
    private GameButton unhackButton;

    [SerializeField]
    private VirusValue virus;

    [SerializeField]
    private ComponentController componentController;

    [SerializeField]
    private Int2Event actionPointsEvent;


    private SystemElementController elementController;
    private SystemElement systemElement;

    private CanvasGroupFader fader;

    private void Start()
    {
        unhackButton.onClick.AddListener(Unhack);
        actionPointsEvent.AddListener(OnActionPointsChanged);

        fader = GetComponent<CanvasGroupFader>();
    }

    public void Show(SystemElementController elementController)
    {
        this.elementController = elementController;
        systemElement = elementController.SystemElement;

        UpdateView();

        gameObject.SetActive(true);
        fader.Fade(1f);
    }

    public void Hide()
    {
        if(gameObject.activeInHierarchy)
            fader.Fade(0f);

        elementController = null;
        systemElement = null;
    }

    private void UpdateView()
    {
        nameText.text = systemElement.Name;

        playerImage.color = systemElement.OwnerVirus.Color;
        playerText.text = systemElement.OwnerVirus.Name;
        unhackCostText.text = systemElement.UnhackCost.ToString();

        if (virus.Value == systemElement.OwnerVirus)
        {
            DisableUnhacking();
        }
        else
        {
            bool state = virus.Value.ActionPoints >= systemElement.UnhackCost && elementController.CanPerformAction;
            unhackButton.SetEnabled(state);
            EnableUnhacking();
        }
    }

    private void Unhack()
    {
        componentController.UnhackElement(elementController);
        Hide();
    }

    public void EnableUnhacking()
    {
        unhackButton.gameObject.SetActive(true);
        unhackTextObject.SetActive(true);
        unhackCostObject.SetActive(true);
    }

    private void DisableUnhacking()
    {
        unhackButton.gameObject.SetActive(false);
        unhackTextObject.SetActive(false);
        unhackCostObject.SetActive(false);
    }

    private void OnActionPointsChanged(int actionPoints, int maxActionPoints)
    {
        if (gameObject.activeInHierarchy && systemElement != null)
            UpdateView();
    }

    private void OnDestroy()
    {
        unhackButton.onClick.RemoveListener(Unhack);
        actionPointsEvent.RemoveListener(OnActionPointsChanged);
    }
}
