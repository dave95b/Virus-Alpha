using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemElementView : MonoBehaviour, ISystemElementView {

    [SerializeField]
    private Text nameText, hackCostText, destroyCostText, hackBonusText, destroyBonusText;

    [SerializeField]
    private GameButton hackButton;
    [SerializeField]
    private GameButton destroyButton;

    [SerializeField]
    private Image image;

    [Space]

    [SerializeField]
    private VirusValue virus;

    [SerializeField]
    private ComponentController componentController;

    [SerializeField]
    private Int2Event actionPointsEvent;


    private SystemElementViewPresenter presenter;
    private SystemElementController elementController;
    private SystemElement systemElement;

    private CanvasGroupFader fader;

    private void Start()
    {
        presenter = new SystemElementViewPresenter(hackButton, destroyButton);
        hackButton.onClick.AddListener(Hack);
        destroyButton.onClick.AddListener(Destroy);
        actionPointsEvent.AddListener(OnActionPointsChanged);

        fader = GetComponent<CanvasGroupFader>();
    }
    
    public void Show(SystemElementController elementController)
    {
        this.elementController = elementController;
        systemElement = elementController.SystemElement;
        image.sprite = systemElement.Type.Sprite;
        
        UpdateView();

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

    private void UpdateView()
    {
        nameText.text = systemElement.Name;
        hackCostText.text = systemElement.HackCost.ToString();
        destroyCostText.text = systemElement.DestroyCost.ToString();
        hackBonusText.text = systemElement.HackReward.ToString();
        destroyBonusText.text = systemElement.DestroyReward.ToString();

        presenter.UpdateView(elementController, virus.Value.ActionPoints);
    }

    private void Hack()
    {
        componentController.HackElement(elementController);
        Hide();
    }

    private void Destroy()
    {
        componentController.DestroyElement(elementController);
        Hide();
    }

    private void OnActionPointsChanged(int actionPoints, int maxActionPoints)
    {
        if (gameObject.activeInHierarchy && systemElement != null)
            UpdateView();
    }

    private void OnDestroy()
    {
        hackButton.onClick.RemoveListener(Hack);
        destroyButton.onClick.RemoveListener(Destroy);
        actionPointsEvent.RemoveListener(OnActionPointsChanged);
    }
}
