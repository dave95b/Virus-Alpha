using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Poza procentowym zajęciem każdego terytorium mogą być tutaj pokazywane informacje o ilości generowanych punktów akcji na turę.
public class OccupationDataView : MonoBehaviour {

    [SerializeField]
    private OccupationDataViewItem viewItem;
    [SerializeField]
    private OccupationDataViewItem totalItem;

    [SerializeField]
    private SystemElementList systemElementList;

    [SerializeField]
    private VirusValue virus;

    [SerializeField] 
    private ElementActionEvent hackEvent, destroyEvent, unhackEvent;

    private List<OccupationDataViewItem> viewItems;
    private OccupationCalculator calculator;

    private CanvasGroupFader fader;

    private void Awake()
    {
        calculator = new OccupationCalculator(systemElementList, virus);
        int territoryCount = systemElementList.ControlElementSizes.Count;

        viewItems = new List<OccupationDataViewItem>(territoryCount);

        viewItems.Add(viewItem);
        for (int i = 1; i < territoryCount; i++)
        {
            var newItem = Instantiate(viewItem, viewItem.transform.parent);
            viewItems.Add(newItem);
        }

        hackEvent.AddListener(ElementActionHandle);
        destroyEvent.AddListener(ElementActionHandle);

        fader = GetComponent<CanvasGroupFader>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        fader.Fade(1f);
    }

    public void Hide()
    {
        fader.Fade(0f);
    }

    private void UpdateView()
    {
        var data = calculator.GetOccupationData();
        OccupationData totalData = new OccupationData();

        for (int i = 0; i < data.Count; i++)
        {
            viewItems[i].UpdateView(data[i]);

            totalData.HackedCount += data[i].HackedCount;
            totalData.TotalCount += data[i].TotalCount;
        }

        totalData.PercentageControl += systemElementList.PercentageOccupation(virus.Value);
        totalItem.UpdateView(totalData);
    }

    private void ElementActionHandle(SystemElementController controller, VirusValue virus)
    {
        if (gameObject.activeInHierarchy)
            UpdateView();
    }

    private void OnEnable()
    {
        UpdateView();
    }

    private void OnDestroy()
    {
        hackEvent.RemoveListener(ElementActionHandle);
        destroyEvent.RemoveListener(ElementActionHandle);
    }
}
