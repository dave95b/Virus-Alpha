using UnityEngine;
using UnityEngine.UI;

public class ActionPointsView : MonoBehaviour {

    [SerializeField]
    private Int2Event actionPointsEvent;
    [SerializeField]
    private IntValue pointsPerTurn;

    [Space]

    [SerializeField]
    private Text pointsText;
    [SerializeField]
    private Text maxText;
    [SerializeField]
    private Text perTurnText;

    private int actionPoints, maxActionPoints;


    private void Start()
    {
        actionPointsEvent.AddListener(OnActionPointsChanged);
        pointsPerTurn.OnValueChanged += UpdatePerTurnText;
    }

    private void UpdateView()
    {
        pointsText.text = actionPoints.ToString();
        maxText.text = maxActionPoints.ToString();
        UpdatePerTurnText(pointsPerTurn.Value);
    }

    private void UpdatePerTurnText(int points)
    {
        perTurnText.text = "+ " + points + "\nper turn";
    }

    private void OnActionPointsChanged(int actionPoints, int maxActionPoints)
    {
        this.actionPoints = actionPoints;
        this.maxActionPoints = maxActionPoints;
        UpdateView();
    }

    private void OnElementHacked(SystemElementController controller, VirusValue virus)
    {
        UpdateView();
    }
    

    private void OnDestroy()
    {
        actionPointsEvent.RemoveListener(OnActionPointsChanged);
        pointsPerTurn.OnValueChanged -= UpdatePerTurnText;
    }
}
