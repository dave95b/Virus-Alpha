using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntValueDisplayer : MonoBehaviour {

    [SerializeField]
    private IntValue target;

    [SerializeField]
    private Text uiText;

    [SerializeField, Multiline]
    private string textToDisplay;


    private void DisplayText(int value)
    {
        uiText.text = string.Format(textToDisplay, target.Value);
    }


    private void OnEnable()
    {
        target.OnValueChanged += DisplayText;
        DisplayText(target.Value);
    }

    private void OnDisable()
    {
        target.OnValueChanged -= DisplayText;
    }
}
