using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementController", menuName = "Gameplay/MovementController", order = 1)]
public class MovementController : ScriptableObject
{
    [SerializeField]
    public Player virus;

    [SerializeField]
    private Color moveRangeColor;

    [SerializeField]
    private int moveCost = 3;

    private List<HexagonTille> tilesToReset = new List<HexagonTille>();
    private MoveRangeCreator rangeCreator = new MoveRangeCreator();

    private VirusAnimationController playerAnimationController;


    public void MoveToTile(HexagonTille tile)
    {
        if(playerAnimationController != null)
        {
            playerAnimationController.Move(tile);
        }
        else
        {
            SetPlayerPosition(tile);
        }
    }

    public void SetPlayerPosition(HexagonTille tile)
    {
        virus.transform.position = tile.transform.position;

        virus.CurrentTile.HasPlayer = false;
        int moveDistance = virus.CurrentTile.HexagonCoordinates.DistanceTo(tile.HexagonCoordinates);
        virus.CurrentTile = tile;
        virus.CurrentTile.HasPlayer = true;
        ResetMoveList(true);
        virus.ActionPoints -= (moveDistance * moveCost);
    }

    public void CreateMoveRange(HexagonTille sourceTile)
    {
        int moveRange = virus.ActionPoints / moveCost;
        var moveList = rangeCreator.CreateMoveRange(moveRange, sourceTile);

        foreach (var tile in moveList)
        {
            ApplyRange(tile);
            tilesToReset.Add(tile);
        }
    }

    public void InitPlayerAnimationController()
    {
        playerAnimationController = virus.GetComponent<VirusAnimationController>();
        playerAnimationController.SetMoveAction(SetPlayerPosition);
    }

    public void ResetMoveList(bool selectNeighbours)
    {
        int tileCount = tilesToReset.Count;
        for (int i = 0; i < tileCount; i++)
        {
            var tile = tilesToReset[i];
            tile.Action = HexagonTille.ActionType.None;
            tile.ColorMiddleTile(tile.DefaultColor.Value);
        }
        tilesToReset.Clear();

        if (selectNeighbours)
        {
            virus.CurrentTile.SelectNeighbourComponents();
            tilesToReset.AddRange(virus.CurrentTile.NeighbourList);
        }
    }

    private void ApplyRange(HexagonTille tile)
    {
        if (tile.IsComponent || tile.HasPlayer)
            return;

        tile.ColorMiddleTile(moveRangeColor);
        tile.Action = HexagonTille.ActionType.Move;
    }

    private void OnEnable()
    {
        tilesToReset.Clear();
    }
}
