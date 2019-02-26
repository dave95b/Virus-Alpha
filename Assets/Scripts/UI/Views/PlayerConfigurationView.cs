using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConfigurationView : MonoBehaviour {

    [SerializeField]
    private GameConfiguration gameConfiguration;

    [SerializeField]
    private PlayerDataArray playerData;

    [Space, SerializeField]
    private Transform itemContainer;

    [SerializeField]
    private PlayerDataItem playerDataItem;


    private PlayerDataItem[] dataItems;


    //private void Awake()
    //{
    //    InstantiateDataItems();
    //}


    public void CreatePlayerData()
    {
        int playerCount = gameConfiguration.PlayerCount;
        playerData.Array = new PlayerData[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            var dataItem = dataItems[i]; 
            string name = dataItem.PlayerName;
            Color color = dataItem.Color;

            playerData[i] = new PlayerData(name, color);
        }
    }

    public void InstantiateDataItems()
    {
        if(itemContainer.childCount > 1)
        {
            DestroyDataItems();
        }

        dataItems = new PlayerDataItem[gameConfiguration.PlayerCount];
        dataItems[0] = playerDataItem;
        playerDataItem.Number = 1;

        for (int i = 1; i < gameConfiguration.PlayerCount; i++)
        {
            PlayerDataItem item = Instantiate(playerDataItem, itemContainer);
            item.Number = i + 1;
            dataItems[i] = item;
        }
    }

    private void DestroyDataItems()
    {
        if(dataItems != null)
        {
            for(int i = 1; i < dataItems.Length; i++)
            {
                Destroy(dataItems[i].gameObject);
            }
        }
    }
}
