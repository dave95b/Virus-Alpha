using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerClickHandler
{
    public string Name;
    public Color Color;


    [SerializeField]
    private int actionPoints;
    public int ActionPoints {
        get { return actionPoints; }
        set
        {
            actionPoints = Mathf.Min(value, maxActionPoints);
            actionPoints = Mathf.Max(actionPoints, 0);

            onActionPointsChangedEvent.Invoke(actionPoints, maxActionPoints);
            moving = false;
        }
    }

    [SerializeField]
    private int maxActionPoints;
    public int MaxActionPoints {
        get { return maxActionPoints; }
        set {
            maxActionPoints = value;
            onActionPointsChangedEvent.Invoke(actionPoints, maxActionPoints);
        }
    }

    private int instanceId;
    public int InstanceId { get { return instanceId; } }


    [HideInInspector]
    public HexagonTille CurrentTile;

    [Space, SerializeField]
    private HexagonTileEvent hexagonClick;

    [SerializeField]
    private Int2Event onActionPointsChangedEvent;
    [SerializeField]
    private NativeEvent onTurnChanged, onBeforeTurnChange;

    [SerializeField]
    private MovementController movementController;

    private bool moving;


    private void Awake()
    {
        instanceId = GetInstanceID();

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color;
        moving = false;
    }

    public void Initialize(HexagonTille tile)
    {
        gameObject.SetActive(true);
        CurrentTile = tile;

        movementController = Instantiate(movementController);
        movementController.virus = this;
        movementController.SetPlayerPosition(tile);
        movementController.InitPlayerAnimationController();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        //movementController.CreateMoveRange(CurrentTile);
        if (!moving)
            movementController.CreateMoveRange(CurrentTile);
        else
            movementController.ResetMoveList(true);

        moving = !moving;
    }

    public void SetActionPoints(int actionPoints, int maxActionPoints)
    {
        this.actionPoints = actionPoints;
        this.maxActionPoints = maxActionPoints;
        onActionPointsChangedEvent.Invoke(actionPoints, maxActionPoints);
    }


    private void MoveToTile(HexagonTille destination)
    {
        if (destination.Action == HexagonTille.ActionType.Move)
            movementController.MoveToTile(destination);
    }

    private void TurnChanged()
    {
        if (CurrentTile != null)
            movementController.SetPlayerPosition(CurrentTile);
    }

    // Wykonanie tego przed zmianą tury przez aktywnego gracza ze względu na błąd, w którym kolejny wirus nie mógł 
    // zhackować lub zniszczyć elementu, jeśli oba wirusy stały obok niego.
    private void BeforeTurnChange()
    {
        movementController.ResetMoveList(false);
    }


    void OnEnable()
    {
        hexagonClick.AddListener(MoveToTile);
        onTurnChanged.AddListener(TurnChanged);
        onBeforeTurnChange.AddListener(BeforeTurnChange);
    }

    void OnDisable()
    {
        hexagonClick.RemoveListener(MoveToTile);
        onTurnChanged.RemoveListener(TurnChanged);
        onBeforeTurnChange.RemoveListener(BeforeTurnChange);
    }
}
