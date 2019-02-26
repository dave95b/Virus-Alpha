using UnityEngine;
using UnityEngine.UI;

public class TurnPlayerView : MonoBehaviour {

    [SerializeField]
    private Text nameText, numberText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private VirusValue currentPlayer;
    [SerializeField]
    private VirusArray players;

    [SerializeField]
    private NativeEvent onTurnChanged;

    private void OnEnable()
    {
        onTurnChanged.AddListener(UpdateTextAndIcon);
    }

    private void OnDisable()
    {
        onTurnChanged.RemoveListener(UpdateTextAndIcon);
    }

    private void UpdateTextAndIcon()
    {
        nameText.text = currentPlayer.Value.Name;
        numberText.text = PlayerNumber(currentPlayer).ToString();
        iconImage.color = currentPlayer.Value.Color;
    }

    private int PlayerNumber(VirusValue player)
    {
        int i = 0;
        for(i = 0; i < players.Length; i++)
        {
            if(players[i] == player.Value)
                return i + 1;
        }
        return i;
    }

}
