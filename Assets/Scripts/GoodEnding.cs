using UnityEngine;
using UnityEngine.UI;

public class GoodEnding : MonoBehaviour
{
    public TMPro.TextMeshProUGUI CoreNeutralEndButton;
    public TMPro.TextMeshProUGUI NeutralEndText;
    public TMPro.TextMeshProUGUI NeutralEndContinueButton;
    public Image GameEndImage;

    [Space(10)]
    [TextArea]
    public string goodCoreNeutralButtonText;
    [TextArea]
    public string goodNeutralEndText;
    [TextArea]
    public string goodNeutralEndContinueButtonText;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>().goodEndUnlocked)
        {
            CoreNeutralEndButton.text = goodCoreNeutralButtonText;
            NeutralEndText.text = goodNeutralEndText;
            NeutralEndContinueButton.text = goodNeutralEndContinueButtonText;
            GameEndImage.color = new Color(1, 1, 1);
        }
    }
}
