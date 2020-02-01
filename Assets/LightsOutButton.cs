using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsOutButton : MonoBehaviour
{
    public List<LightsOutButton> neighbors;
    public bool isOn = false;
    public Color offColor = new Color(0.9f, 0, 0);
    public Color onColor = new Color(0, 0, 0.9f);
    public Button b;

    void Start()
    {
        b = gameObject.GetComponent<Button>();

        b.onClick.AddListener(() => ChangeStatus());

        if (isOn)
        {
            ColorBlock colors = b.colors;
            colors.normalColor = onColor;

            Color highlightedcolor = onColor;
            highlightedcolor.b = 0.8f;
            colors.highlightedColor = highlightedcolor;

            Color pressedColor = onColor;
            pressedColor.b = 0.7f;
            colors.pressedColor = pressedColor;

            colors.selectedColor = onColor;

            b.colors = colors;
        }
        else
        {
            ColorBlock colors = b.colors;
            colors.normalColor = offColor;

            Color highlightedcolor = offColor;
            highlightedcolor.r = 0.8f;
            colors.highlightedColor = highlightedcolor;

            Color pressedColor = offColor;
            pressedColor.r = 0.7f;
            colors.pressedColor = pressedColor;

            colors.selectedColor = offColor;

            b.colors = colors;
        }
    }

    public void ChangeStatus()
    {
        isOn = !isOn;
        if (isOn)
        {
            ColorBlock colors = b.colors;
            colors.normalColor = onColor;

            Color highlightedcolor = onColor;
            highlightedcolor.b = 0.8f;
            colors.highlightedColor = highlightedcolor;

            Color pressedColor = onColor;
            pressedColor.b = 0.7f;
            colors.pressedColor = pressedColor;

            colors.selectedColor = onColor;

            b.colors = colors;
        }
        else
        {
            ColorBlock colors = b.colors;
            colors.normalColor = offColor;

            Color highlightedcolor = offColor;
            highlightedcolor.r = 0.8f;
            colors.highlightedColor = highlightedcolor;

            Color pressedColor = offColor;
            pressedColor.r = 0.7f;
            colors.pressedColor = pressedColor;

            colors.selectedColor = offColor;

            b.colors = colors;
        }

        foreach (LightsOutButton button in neighbors)
        {
            button.isOn = !button.isOn;

            if (button.isOn)
            {
                ColorBlock colors = button.b.colors;
                colors.normalColor = onColor;

                Color highlightedcolor = onColor;
                highlightedcolor.b = 0.8f;
                colors.highlightedColor = highlightedcolor;

                Color pressedColor = onColor;
                pressedColor.b = 0.7f;
                colors.pressedColor = pressedColor;

                colors.selectedColor = onColor;

                button.b.colors = colors;
            }
            else
            {
                ColorBlock colors = button.b.colors;
                colors.normalColor = offColor;

                Color highlightedcolor = offColor;
                highlightedcolor.r = 0.8f;
                colors.highlightedColor = highlightedcolor;

                Color pressedColor = offColor;
                pressedColor.r = 0.7f;
                colors.pressedColor = pressedColor;

                colors.selectedColor = offColor;

                button.b.colors = colors;
            }
        }
    }
}
