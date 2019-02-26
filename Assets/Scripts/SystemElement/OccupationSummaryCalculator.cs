using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasa służąca do obliczania ilości przejętych elementów przez każdego wirusa. Dane te będą wyświetlane po zakończeniu gry.
public class OccupationSummaryCalculator {

    private SystemElementList elementList;
    private VirusArray players;


    public OccupationSummaryCalculator(SystemElementList elementList, VirusArray players)
    {
        this.elementList = elementList;
        this.players = players;
    }

    public OccupationSummary[] Calculate()
    {
        int playerCount = players.Length;
        OccupationSummary[] result = new OccupationSummary[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            var summary = new OccupationSummary();
            var virus = players[i];

            summary.Virus = virus;
            summary.HackedCount = elementList.HackedCount(virus);
            summary.PercentageControl = elementList.PercentageOccupation(virus);

            result[i] = summary;
        }

        return result;
    }
}
