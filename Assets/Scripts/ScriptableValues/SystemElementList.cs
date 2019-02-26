using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Values/System Element List", order = 152)]
public class SystemElementList : ScriptableValue<List<SystemElement>>
{
    public Dictionary<SystemElementControl, List<SystemElement>> ElementsByControl;
    public Dictionary<SystemElementControl, int> ControlElementSizes;

    private int totalElementSize;
    public int TotalElementSize { get { return totalElementSize; } }


    public int HackedCount(Player virus)
    {
        return HackedCount(virus, Value);
    }

    public int HackedCount(Player virus, SystemElementControl control)
    {
        var controlElements = ElementsByControl[control];
        return HackedCount(virus, controlElements);
    }

    private int HackedCount(Player virus, List<SystemElement> elements)
    {
        int elementCount = elements.Count;
        int hackedCount = 0;

        for (int i = 0; i < elementCount; i++)
        {
            var element = elements[i];
            if (element.IsHackedBy(virus))
                hackedCount++;
        }

        return hackedCount;
    }


    public float PercentageOccupation(Player virus)
    {
        int hackedSize = GetHackedElementsSize(virus, Value);

        return (float)hackedSize / (float)totalElementSize;
    }

    public float PercentageOccupation(Player virus, SystemElementControl control)
    {
        var controlElements = ElementsByControl[control];
        int hackedSize = GetHackedElementsSize(virus, controlElements);
        int controlElementsSize = ControlElementSizes[control];

        return (float)hackedSize / (float)controlElementsSize;
    }

    private int GetHackedElementsSize(Player virus, List<SystemElement> elements)
    {
        int elementCount = elements.Count;
        int hackedSize = 0;

        for (int i = 0; i < elementCount; i++)
        {
            var element = elements[i];
            if (element.IsHackedBy(virus))
                hackedSize += element.Size;
        }

        return hackedSize;
    }

    

    public void OnMapGenerated()
    {
        var controls = Value.Select(element => element.Control).Distinct();

        ElementsByControl = new Dictionary<SystemElementControl, List<SystemElement>>();
        foreach (var control in controls)
            ElementsByControl.Add(control, new List<SystemElement>());

        foreach (var element in Value)
            ElementsByControl[element.Control].Add(element);

        totalElementSize = 0;
        ControlElementSizes = new Dictionary<SystemElementControl, int>();
        foreach (var elementsEntry in ElementsByControl)
        {
            var control = elementsEntry.Key;
            var elements = elementsEntry.Value;
            int sizeCount = 0;

            foreach (var element in elements)
                sizeCount += element.Size;

            ControlElementSizes[control] = sizeCount;
            totalElementSize += sizeCount;
        }
    }

    public void OnElementDestroyed(SystemElementController elementController, VirusValue virus)
    {
        var element = elementController.SystemElement;
        var control = element.Control;
        int elementSize = element.Size;

        ElementsByControl[control].Remove(element);
        ControlElementSizes[control] -= elementSize;
        totalElementSize -= elementSize;
    }
}
