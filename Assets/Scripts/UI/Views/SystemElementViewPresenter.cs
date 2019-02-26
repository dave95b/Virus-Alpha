using System;
using UnityEngine;
using UnityEngine.UI;


class SystemElementViewPresenter
{
    private GameButton hackButton, destroyButton;

    private Image[] hackImages, destroyImages;
    private Text[] hackTexts, destroyTexts;
    

    public SystemElementViewPresenter(GameButton hackButton, GameButton destroyButton)
    {
        this.hackButton = hackButton;
        this.destroyButton = destroyButton;

        hackImages = hackButton.GetComponentsInChildren<Image>();
        destroyImages = destroyButton.GetComponentsInChildren<Image>();

        hackTexts = hackButton.GetComponentsInChildren<Text>();
        destroyTexts = destroyButton.GetComponentsInChildren<Text>();
    }



    public void UpdateView(SystemElementController elementController, int actionPoints)
    {
        var systemElement = elementController.SystemElement;

        bool buttonState = actionPoints >= systemElement.HackCost && elementController.CanPerformAction;
        hackButton.SetEnabled(buttonState);
        buttonState = actionPoints >= systemElement.DestroyCost && elementController.CanPerformAction;
        destroyButton.SetEnabled(buttonState);
    }

    private void ManageButton(Button button, SystemElementController elementController, int actionPoints, int cost)
    {
        button.interactable = actionPoints >= cost && elementController.CanPerformAction;
    }

    private void UpdateButtonChildren(Button button, Image[] images, Text[] texts)
    {
        Color color = button.IsInteractable() ? button.colors.highlightedColor : button.colors.disabledColor;

        foreach (Image img in images)
        {
            img.color = color;
        }
        foreach (Text txt in texts)
        {
            txt.color = color;
        }
    }
}
