using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controller", menuName = "Gameplay/Virus/Controller", order = 4)]
public class ComponentController : ScriptableObject {

    [SerializeField]
    private ElementActionEvent hackElement, destroyElement, unhackElement;

    [SerializeField]
    private VirusValue virus;
    

    public void HackElement(SystemElementController elementController)
    {
        var element = elementController.SystemElement;
        var player = virus.Value;

        player.ActionPoints -= element.HackCost;
        element.IsHacked = true;
        element.OwnerVirus = player;

        elementController.HackView(player.Color);

        hackElement.Invoke(elementController, virus);
    }

    public void DestroyElement(SystemElementController elementController)
    {
        var element = elementController.SystemElement;
        var player = virus.Value;

        player.ActionPoints -= element.DestroyCost;
        player.MaxActionPoints += element.DestroyReward;

        element.IsDestroyed = true;
        element.IsHacked = false;
        element.OwnerVirus = player;

        elementController.DestroyView();

        destroyElement.Invoke(elementController, virus);
    }

    public void UnhackElement(SystemElementController elementController)
    {
        var element = elementController.SystemElement;
        var player = virus.Value;
        
        player.ActionPoints -= element.UnhackCost;

        unhackElement.Invoke(elementController, virus);

        element.IsHacked = false;
        element.OwnerVirus = null;

        elementController.UnhackView();
    }
}
