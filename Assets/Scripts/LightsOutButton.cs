using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsOutButton : MonoBehaviour
{
    public List<LightsOutButton> neighbors;
    public bool isOn;
    public Color offColor = new Color(0.9f, 0, 0);
    public Color onColor = new Color(0, 0, 0.9f);
    public Button b;
    public AudioSource src;

    void Start()
    {
        b = gameObject.GetComponent<Button>();

        b.onClick.AddListener(() => ChangeStatus());

        ChangeColor();
    }

    private void ChangeColor()
    {
        if (isOn)
        {
            ColorBlock colors = b.colors;
            colors.normalColor = onColor;

            Color highlightedcolor = onColor;
            highlightedcolor.g = 0.5f;
            colors.highlightedColor = highlightedcolor;

            Color pressedColor = onColor;
            pressedColor.g = 0.4f;
            colors.pressedColor = pressedColor;

            colors.selectedColor = onColor;

            b.colors = colors;
        }
        else
        {
            ColorBlock colors = b.colors;
            colors.normalColor = offColor;

            colors.highlightedColor = new Color(0.5f, 0.5f, 0.5f);

            colors.pressedColor = new Color(0.4f, 0.4f, 0.4f);

            colors.selectedColor = offColor;

            b.colors = colors;
        }
    }

    public void ChangeStatus()
    {
        isOn = !isOn;
        ChangeColor();

        src.Play();

        foreach (LightsOutButton button in neighbors)
        {
            button.isOn = !button.isOn;

            button.ChangeColor();
        }
    }
}