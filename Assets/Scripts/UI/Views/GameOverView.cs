using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverView : MonoBehaviour {

    [SerializeField]
    private SystemElementList elementList;
    [SerializeField]
    private VirusArray players;
    [SerializeField]
    private PlayerSummaryViewItem summaryViewItem;

    [SerializeField]
    private Text winnerText;
    [SerializeField]
    private Image crownImage;

    private OccupationSummaryCalculator calculator;

    private CanvasGroupFader fader;


    private void Start()
    {
        calculator = new OccupationSummaryCalculator(elementList, players);
        fader = GetComponent<CanvasGroupFader>();
        fader.Fade(1f);
        ShowData();
    }

    private void ShowData()
    {
        OccupationSummary[] summaryData = calculator.Calculate();

        int winnerIndex = 0;
        float winnerPercentage = 0f;
        for (int i = 0; i < summaryData.Length; i++)
        {
            var data = summaryData[i];
            PlayerSummaryViewItem summary = Instantiate(summaryViewItem, summaryViewItem.transform.parent);
            summary.SetViewData(data.Virus, data.HackedCount, elementList.Value.Count, data.PercentageControl);

            if(winnerPercentage < data.PercentageControl)
            {
                winnerPercentage = data.PercentageControl;
                winnerIndex = i;
            }
        }

        Player winner = summaryData[winnerIndex].Virus;
        if(winner.Name == null || winner.Name.IsEmpty())
        {
            winnerText.text = "Noname";
        }
        else
        {
            winnerText.text = winner.Name;
        }
        crownImage.color = winner.Color;

        Destroy(summaryViewItem.gameObject);        
    }
}
