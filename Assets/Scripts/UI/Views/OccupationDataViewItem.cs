using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Może jakaś ikonka przy discountText?
public class OccupationDataViewItem : MonoBehaviour {

    [SerializeField]
    private Text territoryNameText, elementCountsText, discountText;

    [SerializeField]
    private Image territoryImage;

    public void UpdateView(OccupationData occupationData)
    {
        if(occupationData.Control != null)
        {
            territoryNameText.text = occupationData.Control.Name;
        }

        string percentageOccupation = GetPercentageText(occupationData.PercentageControl);
        elementCountsText.text = string.Format("{0} / {1} ({2})", occupationData.HackedCount, occupationData.TotalCount, percentageOccupation);

        if(occupationData.Control != null)
        {
            string discount = GetPercentageText(occupationData.Control.OccupationDiscount);
            discountText.text = discount;

            territoryImage.sprite = occupationData.Control.Sprite;
        }
    }

    private string GetPercentageText(float value)
    {
        return (value * 100).Round(2) + "%";
    }
}
