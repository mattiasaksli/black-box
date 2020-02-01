using Doozy.Engine.UI;
using UnityEngine;

public class DisplayTrigger : MonoBehaviour
{
    public DisplayText playerDisplay;
    public UIView label;
    public GameObject player;
    public float activationDistance;
    public string key;
    public SaveState save;

    [TextArea(3, 10)]
    public string[] sentences;

    void Start()
    {
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance && !playerDisplay.inDialogue)
        {
            if (!key.Equals(""))
            {
                if (save.flags[key])
                {
                    this.enabled = false;
                }
            }

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
