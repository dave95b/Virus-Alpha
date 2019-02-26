using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataItem : MonoBehaviour {

    [SerializeField]
    private Text numberText;

    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private Image colorSwitcherImage;

    private Button colorSwitcherButton;


    [Space, SerializeField]
    private PlayerColorArray colors;


    public string PlayerName { get { return inputField.text; } }

    public Color Color { get { return colors[Number].Color; } }

    [HideInInspector]
    public int Number;


    private void Start()
    {
        colorSwitcherButton = colorSwitcherImage.GetComponent<Button>();
        colorSwitcherButton.onClick.AddListener(ChangeColor);

        string numberString = Number.ToString();

        numberText.text = numberString;
        inputField.text = "Player " + numberString;
        colorSwitcherImage.color = Color;
        colors[Number].IsTaken = true;
    }


    public void ChangeColor()
    {
        colors[Number].IsTaken = false;
        
        do
        {
            Number = (++Number) % colors.Length;
        } while (colors[Number].IsTaken);

        colorSwitcherImage.color = Color;
        colors[Number].IsTaken = true;
    }
}
