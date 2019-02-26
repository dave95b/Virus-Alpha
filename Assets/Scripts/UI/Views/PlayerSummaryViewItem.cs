using UnityEngine;
using UnityEngine.UI;

public class PlayerSummaryViewItem : MonoBehaviour {

    [SerializeField]
    private Image playerHexImage;

    [SerializeField]
    private Text pointsText, percentageText;

    public void SetViewData(Player player, int hackedCount, int maxCount, float percentage)
    {
        playerHexImage.color = player.Color;
        pointsText.text = hackedCount + "/" + maxCount;
        percentageText.text = (percentage * 100).Round(2) + "%";
    }

}
