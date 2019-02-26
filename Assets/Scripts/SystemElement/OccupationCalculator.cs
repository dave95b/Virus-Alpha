using System;
using System.Collections.Generic;
using UnityEngine;

// Klasa wyznaczająca dla każdego terytorium na mapie liczbę posiadanych komponentów oraz liczbę komponentów zhackowanych przez wirusa. 
// Na tej podstawie można wyznaczyć procentowe zajęcie każdego terytorium przez aktywnego wirusa.
// W zwracanych danych jest także referencja do SystemElementControl, aby mieć dostęp do OccupationDiscount.
// Klasa aktualnie wykorzystywana do wyświetlania wyżej wymienionych informacji. Jeśli okaże się użyteczna w innym miejscu, warto zamienić ją na ScriptableObjecta.
public class OccupationCalculator {

    private SystemElementList systemElementList;
    private VirusValue virus;

    private List<OccupationData> occupationDataList;


    public OccupationCalculator(SystemElementList systemElementList, VirusValue virus)
    {
        this.systemElementList = systemElementList;
        this.virus = virus;

        var controlSizes = systemElementList.ControlElementSizes;

        occupationDataList = new List<OccupationData>(controlSizes.Count);
        foreach (var control in controlSizes)
        {
            var occupationData = new OccupationData()
            {
                Control = control.Key
            };
            occupationDataList.Add(occupationData);
        }
    }


    public List<OccupationData> GetOccupationData()
    {
        foreach (var data in occupationDataList)
        {
            var control = data.Control;
            data.HackedCount = systemElementList.HackedCount(virus.Value, control);
            data.TotalCount = systemElementList.ElementsByControl[control].Count;
            data.PercentageControl = systemElementList.PercentageOccupation(virus.Value, control);
        }

        return occupationDataList;
    }
}
