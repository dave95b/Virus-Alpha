using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameButton : MonoBehaviour {

    private Button button;
    private Image[] images;
    private Text[] texts;

    public UnityEvent onClick { get { return button.onClick; } }


    private void Awake()
    {
        button = GetComponent<Button>();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<Text>();
    }

    public void SetEnabled(bool enabled)
    {
        Color color = enabled ? button.colors.highlightedColor : button.colors.disabledColor;
        SetEnabled(enabled, color);
    }

    private void SetEnabled(bool enable, Color color)
    {
        button.interactable = enable;
        foreach (Image img in images)
        {
            img.color = color;
        }
        foreach (Text txt in texts)
        {
            txt.color = color;
        }
    }
}
