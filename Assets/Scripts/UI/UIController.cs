using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    [SerializeField]
    private SystemElementControllerEvent systemElementClick;

    [SerializeField]
    private SystemElementView systemElementView;
    [SerializeField]
    private SystemElementHackedView systemElementHackedView;
    [SerializeField]
    private SystemElementDestroyedView systemElementDestroyedView;

    private ISystemElementView currentView;


    public void ResetView()
    {
        currentView = null;
    }

    private void ShowSystemElementView(SystemElementController elementController)
    {
        SystemElement systemElement = elementController.SystemElement;
        ISystemElementView selectedView;

        if(systemElement.IsDestroyed)
        {
            selectedView = systemElementDestroyedView;
        }
        else if(systemElement.IsHacked)
        {
            selectedView = systemElementHackedView;
        }
        else
        {
            selectedView = systemElementView;
        }

        if (currentView != null && currentView != selectedView)
            currentView.Hide();

        currentView = selectedView;
        currentView.Show(elementController);
    }

    private void OnEnable()
    {
        systemElementClick.AddListener(ShowSystemElementView);
    }

    private void OnDisable()
    {
        systemElementClick.RemoveListener(ShowSystemElementView);
    }
}
