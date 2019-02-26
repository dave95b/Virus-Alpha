using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SystemElementController : MonoBehaviour {

    //Fizyczne dane dotyczące elementu systemu
    [SerializeField]
    private SystemElement systemElement;

    public SystemElement SystemElement
    {
        get { return systemElement; }
        set
        {
            systemElement = value;
            SetCentralHexagon();
        }
    }

    public HashSet<HexagonTille> Tiles;

    private Func<HexagonTille, bool> tileWithAction = tile => tile.Action == HexagonTille.ActionType.ChooseAction;
    public bool CanPerformAction
    {
        get { return Tiles.Any(tileWithAction); }
    }

    [SerializeField]
    private HexagonTileEvent hexagonClick;
    [SerializeField]
    private SystemElementControllerEvent systemElementClick;

    [SerializeField]
    private SpriteRenderer hexSpriteRenderer;

    [SerializeField]
    private SpriteRenderer iconSpriteRenderer;
    
    private Color destroyedColor = new Color(0.45f, 0.45f, 0.45f);


    private void HandleHexagonClick(HexagonTille tile)
    {
        //if (Tiles.Contains(tile) && tile.Action == HexagonTille.ActionType.ChooseAction)
        if (Tiles.Contains(tile))
            systemElementClick.Invoke(this);
    }

    void OnEnable()
    {
        hexagonClick.AddListener(HandleHexagonClick);
    }

    void OnDisable()
    {
        hexagonClick.RemoveListener(HandleHexagonClick);
    }

    private void SetCentralHexagon()
    {
        iconSpriteRenderer.sprite = systemElement.Type.Sprite;
        hexSpriteRenderer.color = systemElement.Control.Color;

        Vector3 iconScale = iconSpriteRenderer.transform.localScale;
        float t = 0.35f + 0.12f * systemElement.Size;
        hexSpriteRenderer.transform.localScale = new Vector3(t, t, 1);
        iconSpriteRenderer.transform.localScale = new Vector3(iconScale.x * t, iconScale.y * t, 1);
    }

    public void DestroyView()
    {
        iconSpriteRenderer.sprite = systemElement.Type.DestroyedSprite;

        Vector3 iconScale = iconSpriteRenderer.transform.localScale;
        iconSpriteRenderer.transform.localScale = new Vector3(iconScale.x * 2f, iconScale.y * 2f, 1);
        Vector3 iconPosition = iconSpriteRenderer.transform.localPosition;
        iconSpriteRenderer.transform.localPosition = new Vector3(
            iconPosition.x + 0.005f * systemElement.Size, 
            iconPosition.y - 0.02f * systemElement.Size + 0.01f * (systemElement.Size - 1), 
            iconPosition.z);

        hexSpriteRenderer.color = destroyedColor;
    }

    public void HackView(Color playerColor)
    {
        hexSpriteRenderer.color = playerColor;
    }

    public void UnhackView()
    {
        hexSpriteRenderer.color = systemElement.Control.Color;
    }
}
