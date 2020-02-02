using UnityEngine;
using UnityEngine.UI;

public class GoodEnding : MonoBehaviour
{
    public Button CoreGoodEndButton;
    public TMPro.TextMeshProUGUI NeutralEndText;
    public TMPro.TextMeshProUGUI NeutralEndContinueButton;
    public Image GameEndImage;

    [Space(10)]
    [TextArea]
    public string goodNeutralEndText;
    [TextArea]
    public string goodNeutralEndContinueButtonText;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>().goodEndUnlocked)
        {
            CoreGoodEndButton.interactable = true;
            NeutralEndText.text = goodNeutralEndText;
            NeutralEndContinueButton.text = goodNeutralEndContinueButtonText;
            GameEndImage.color = new Color(1, 1, 1);
        }
    }
}
