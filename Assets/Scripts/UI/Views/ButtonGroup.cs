using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour {

    public event Action<int> OnButtonClick;

    [SerializeField]
    private Color selectedColor;

    [HideInInspector]
    public ConfigButton[] ConfigButtons;

    private ConfigButton selectedButton;


    private void Start()
    {
        var buttons = GetComponentsInChildren<Button>();
        int buttonCount = buttons.Length;
        ConfigButtons = new ConfigButton[buttonCount];

        for (int i = 0; i < buttonCount; i++)
        {
            var button = buttons[i];
            var image = button.GetComponent<Image>();
            var text = button.GetComponentInChildren<Text>();

            var configButton = new ConfigButton(i, button, image, text);
            ConfigButtons[i] = configButton;
            button.onClick.AddListener(() => ButtonClick(configButton.Index));
        }

        selectedButton = ConfigButtons[0];
        ButtonClick(0);
    }


    private void ButtonClick(int position)
    {
        selectedButton.Image.color = Color.white;
        selectedButton = ConfigButtons[position];
        selectedButton.Image.color = selectedColor;

        if (OnButtonClick != null)
            OnButtonClick.Invoke(position);
    }
}

public class ConfigButton
{
    public int Index;
    public Button Button;
    public Image Image;
    public Text Text;


    public ConfigButton(int index, Button button, Image image, Text text)
    {
        Index = index;
        Button = button;
        Image = image;
        Text = text;
    }
}
