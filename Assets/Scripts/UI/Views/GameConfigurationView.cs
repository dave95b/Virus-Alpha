using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigurationView : MonoBehaviour {

    [SerializeField]
    private GameConfigurationData configurationData;

    [SerializeField]
    private GameConfiguration gameConfiguration;

    [Space, SerializeField]
    private ButtonGroup playerGroup;
    [SerializeField]
    private ButtonGroup mapGroup, turnTimeGroup, turnCountGroup;


    private void Start()
    {
        playerGroup.OnButtonClick += SetPlayerCount;
        mapGroup.OnButtonClick += SetMapSize;
        turnTimeGroup.OnButtonClick += SetTurnTime;
        turnCountGroup.OnButtonClick += SetTurnCount;

        SetButtons(playerGroup.ConfigButtons, configurationData.PlayerCounts);
        SetButtons(turnCountGroup.ConfigButtons, configurationData.TurnCounts);

        SetPlayerCount(0);
        SetMapSize(0);
        SetTurnTime(0);
        SetTurnCount(0);
    }

    private void SetPlayerButtons()
    {
        var playerButtons = playerGroup.ConfigButtons;
        int count = playerButtons.Length;

        for (int i = 0; i < count; i++)
        {
            var button = playerButtons[i];
            button.Text.text = configurationData.PlayerCounts[i].ToString();
        }
    }

    private void SetButtons(ConfigButton[] buttons, int[] configData)
    {
        int count = buttons.Length;
        for (int i = 0; i < count; i++)
        {
            var button = buttons[i];
            button.Text.text = configData[i].ToString();
        }
    }


    private void SetPlayerCount(int index)
    {
        gameConfiguration.PlayerCount = configurationData.PlayerCounts[index];
    }

    private void SetMapSize(int index)
    {
        gameConfiguration.MapSize = configurationData.MapSizes[index];
    }

    private void SetTurnTime(int index)
    {
        gameConfiguration.TurnTime = configurationData.TurnTimes[index];
    }

    private void SetTurnCount(int index)
    {
        gameConfiguration.TurnCount = configurationData.TurnCounts[index];
    }
}
