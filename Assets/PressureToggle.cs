using UnityEngine;
using UnityEngine.UI;

public class PressureToggle : MonoBehaviour
{
    [SerializeField]
    private float pressureValue;
    public float currentValue;
    public Toggle toggle;
    public PressureGameController controller;
    public Color onColor = new Color(1, 0.5f, 0.15f);
    public Color offColor = new Color(0.8f, 0.8f, 0.8f);

    void Start()
    {
        controller = GetComponentInParent<PressureGameController>();
        toggle = gameObject.GetComponent<Toggle>();
        SetColorAndValue();
    }

    public void ChangeValue(bool value)
    {
        toggle.isOn = value;

        SetColorAndValue();

        controller.ChangeTargetPressureValue();
    }

    private void SetColorAndValue()
    {
        if (toggle.isOn)
        {
            ColorBlock cb = toggle.colors;
            cb.normalColor = onColor;
            cb.selectedColor = onColor;
            cb.highlightedColor = new Color(onColor.r - 0.05f, onColor.g - 0.05f, onColor.b - 0.05f);
            cb.pressedColor = new Color(onColor.r - 0.1f, onColor.g - 0.1f, onColor.b - 0.1f);
            toggle.colors = cb;

            currentValue = pressureValue;
        }
        else
        {
            ColorBlock cb = toggle.colors;
            cb.normalColor = offColor;
            cb.selectedColor = offColor;
            cb.highlightedColor = new Color(offColor.r - 0.05f, offColor.g - 0.05f, offColor.b - 0.05f);
            cb.pressedColor = new Color(offColor.r - 0.1f, offColor.g - 0.1f, offColor.b - 0.1f);
            toggle.colors = cb;

            currentValue = 0;
        }
    }
}
