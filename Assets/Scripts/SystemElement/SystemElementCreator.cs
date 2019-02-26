using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemElementCreator : MonoBehaviour {

    [SerializeField]
    private SystemElementController systemElementPrefab;

    [SerializeField]
    private SystemElementList systemElementList;

    [SerializeField]
    private Modifiers modifiers;

    [SerializeField]
    private SystemElementType[] types;

    [SerializeField]
    private SystemElementControl[] controls;

    [SerializeField]
    private ColorList colorList;

    [SerializeField]
    private RandomValue random;


    private void Start()
    {
        foreach (var control in controls)
        {
            colorList.Value.Add(control.Color);
        }
    }

    public void OnElementGenerated(GeneratedElementInfo elementInfo)
    {
        SystemElement systemElement = InstantiateSystemElement(ref elementInfo);
        systemElementList.Value.Add(systemElement);

        SystemElementController elementController = Instantiate(systemElementPrefab, transform);
        elementController.transform.position = elementInfo.Position;
        elementController.SystemElement = systemElement;
        elementController.Tiles = elementInfo.Tiles;
        systemElement.Controller = elementController;
    }

    private SystemElement InstantiateSystemElement(ref GeneratedElementInfo elementInfo)
    {
        var control = controls[elementInfo.MapTerritory];
        var type = types.GetRandom(random.Value);
        int size = elementInfo.Tiles.Count;

        return new SystemElement(control, type, size, modifiers);
    }
}
