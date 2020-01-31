using Doozy.Engine.UI;
using UnityEngine;

public class DisplayTrigger : MonoBehaviour
{
    public DisplayText playerDisplay;
    public UIView label;
    public GameObject player;
    public float activationDistance;

    [TextArea(3, 10)]
    public string[] sentences;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance && !playerDisplay.inDialogue)
        {
            label.Show();
            if (Input.GetKeyDown(KeyCode.F))
            {
                label.Hide();
                playerDisplay.sentences = sentences;
                playerDisplay.StartDisplay();
            }
        }
        else
        {
            label.Hide();
        }
    }
}
