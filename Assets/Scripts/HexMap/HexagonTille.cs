using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexagonTille : MonoBehaviour, IEqualityComparer<HexagonTille>, IComparable<HexagonTille>, IPointerClickHandler {

    public enum ActionType { Move, ChooseAction, None };

    [SerializeField]
    private HexagonTileEvent onClick;

    public HexagonTileEvent OnClick { get { return onClick; } }

    public int HexX;
    public int HexY;
    public int HexZ;

    private HexagonCoordinates hexagonCoordinates;
    public HexagonCoordinates HexagonCoordinates
    {
        get { return hexagonCoordinates; }
        set
        {
            hexagonCoordinates = value;
            HexX = value.X;
            HexY = value.Y;
            HexZ = value.Z;
        }
    }

    public List<HexagonTille> NeighbourList;
    public bool IsColored;
    public bool IsComponent;
    public bool HasPlayer;

    private ActionType nextAction = ActionType.None;
    public ActionType Action {
        get { return nextAction; }
        set { nextAction = value; }
    }

    public ColorValue DefaultColor;

    private SpriteRenderer tileMiddleSprite;
    private SpriteRenderer borderOfTile;


    private void Awake() {
        NeighbourList = new List<HexagonTille>(6);
        tileMiddleSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        borderOfTile = GetComponent<SpriteRenderer>();

        tileMiddleSprite.color = DefaultColor.Value;
    }


    public void ColorTile(Color color)
    {
        IsColored = true;
        borderOfTile.color = color;
    }

    public void ColorMiddleTile(Color color)
    {
        tileMiddleSprite.color = color;
    }

    public void SelectNeighbourComponents()
    {
        foreach (var neighbour in NeighbourList)
        {
            if (!neighbour.IsComponent)
                continue;

            neighbour.nextAction = ActionType.ChooseAction;
        }
    }

    #region Equality

    public bool Equals(HexagonTille x, HexagonTille y) {
        return x.GetInstanceID() == y.GetInstanceID();
    }

    public int GetHashCode(HexagonTille obj) {
        return obj.GetInstanceID();
    }

    public override bool Equals(object other)
    {
        if (other == null || !(other is HexagonTille))
            return false;

        HexagonTille otherTile = other as HexagonTille;
        return GetInstanceID() == otherTile.GetInstanceID();
    }

    public override int GetHashCode()
    {
        return GetInstanceID();
    }

    public int CompareTo(HexagonTille other)
    {
        return other.GetInstanceID() - GetInstanceID();
    }

    #endregion Equality

    public bool IsClearRange(int range)
    {
        if (range == 0 || IsComponent)
        {
            return !IsComponent;
        }
        if (NeighbourList.Count != 6)
        {
            return false;
        }

        int neighbourCount = NeighbourList.Count;
        for (int i = 0; i < neighbourCount; i++)
        {
            var t = NeighbourList[i];
            if (!t.IsClearRange(range - 1))
            {
                return false;
            }
        }

        return true;
    }

    public void SetMainColor(ColorValue mainColor, ref Color componentColor) {
        ColorTile(componentColor);
        ColorMiddleTile(mainColor.Value);
        DefaultColor = mainColor;

        borderOfTile.sortingOrder += 1;
        tileMiddleSprite.sortingOrder += 1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("HexagonTile"))
            return;

        HexagonTille otherTile = collision.GetComponent<HexagonTille>();
        if (!NeighbourList.Contains(otherTile))
            NeighbourList.Add(otherTile);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke(this);
    }

    public void SortNeighbours()
    {
        NeighbourList.Sort((p, q) => p.GetInstanceID().CompareTo(q.GetInstanceID()));
    }
}

[System.Serializable]
public struct HexagonCoordinates : IEquatable<HexagonCoordinates>
{
    public int X;
    public int Z;
    public int Y
    {
        get { return -X - Z; }
    }
    public bool MadeCoordinates;
    public bool HasCoordinates;


    public HexagonCoordinates(int x, int z)
    {
        X = x;
        Z = z;
        MadeCoordinates = false;
        HasCoordinates = false;
    }


    public int DistanceTo(HexagonCoordinates other)
    {
        int xDistance = Mathf.Abs(X - other.X);
        int yDistance = Mathf.Abs(Y - other.Y);
        int zDistance = Mathf.Abs(Z - other.Z);

        return (xDistance + yDistance + zDistance) / 2;
    }


    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is HexagonCoordinates))
            return false;

        HexagonCoordinates other = (HexagonCoordinates) obj;

        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        int hash = 127 + X;
        hash = 127 * hash + Y;
        return 127 * hash + Z;
    }

    public bool Equals(HexagonCoordinates other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }
}