using UnityEngine;
using UnityEngine.UI;

public class DominationView : MonoBehaviour {

    [SerializeField]
    private Text dominationText;

    [SerializeField]
    private OccupationMonitor monitor;

    [SerializeField]
    private VirusValue activeVirus;

    [SerializeField]
    private SystemElementList systemElementList;

    [SerializeField]
    private NativeEvent onTurnChanged;
    [SerializeField]
    private ElementActionEvent onHackElement, onDestroyElement;

    private string baseText = "Domination: ";

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        dominationText.text = string.Format("{0} {1:0.00} / {2:0.00} %", baseText, systemElementList.PercentageOccupation(activeVirus.Value) * 100, monitor.OccupationLimit * 100);
    }

    private void UpdateTextElement(SystemElementController systemElementController, VirusValue virus)
    {
        UpdateText();
    }

    private void OnEnable()
    {
        onTurnChanged.AddListener(UpdateText);
        onHackElement.AddListener(UpdateTextElement);
        onDestroyElement.AddListener(UpdateTextElement);
    }

    private void OnDisable()
    {
        onTurnChanged.RemoveListener(UpdateText);
        onHackElement.RemoveListener(UpdateTextElement);
        onDestroyElement.RemoveListener(UpdateTextElement);
    }
}
