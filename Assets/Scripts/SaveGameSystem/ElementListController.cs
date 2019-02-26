using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementListController : MonoBehaviour, ISaveable {

    [SerializeField]
    private SystemElementList elementList;

    [SerializeField]
    private VirusArray players;

    [SerializeField]
    private ComponentController componentController;

    [SerializeField]
    private VirusValue currentVirus;


    public void ApplyStateEarly(SavedState state)
    {

    }

    public void ApplyStateLate(SavedState state)
    {
        if (state.IsLoaded)
        {
            Player initialPlayer = currentVirus.Value;

            List<SystemElement> _elementList = elementList.Value;
            int elementCount = _elementList.Count;
            SystemElementInfo[] elementInfoArray = state.SystemElementInfoArray;
            Player[] playersArray = players.Array;

            for (int i = 0; i < elementCount; i++)
            {
                var systemElement = _elementList[i];
                var elementInfo = elementInfoArray[i];

                if (elementInfo.IsHacked)
                {
                    Player hacker = playersArray[elementInfo.OwnerVirus];
                    systemElement.OwnerVirus = hacker;
                    systemElement.IsHacked = true;

                    currentVirus.Value = hacker;
                    componentController.HackElement(systemElement.Controller);
                }
                else if (elementInfo.IsDestroyed)
                {
                    Player destroyer = playersArray[elementInfo.OwnerVirus];
                    systemElement.OwnerVirus = destroyer;
                    systemElement.IsDestroyed = true;

                    currentVirus.Value = destroyer;
                    componentController.DestroyElement(systemElement.Controller);
                }
            }

            currentVirus.Value = initialPlayer;
        }
    }

    public void SaveState(SavedState state)
    {
        List<SystemElement> _elementList = elementList.Value;
        int elementCount = _elementList.Count;
        var elementInfoArray = new SystemElementInfo[elementCount];
        Player[] playersArray = players.Array;

        for (int i = 0; i < elementCount; i++)
        {
            var systemElement = _elementList[i];
            int playerIndex;
            if (systemElement.OwnerVirus == null)
                playerIndex = -1;
            else
                playerIndex = Array.IndexOf(playersArray, systemElement.OwnerVirus);

            var elementInfo = new SystemElementInfo()
            {
                OwnerVirus = playerIndex,
                IsHacked = systemElement.IsHacked,
                IsDestroyed = systemElement.IsDestroyed
            };

            elementInfoArray[i] = elementInfo;
        }

        state.SystemElementInfoArray = elementInfoArray;
    }
}
