using Doozy.Engine.UI;
using UnityEngine;

public class DisplayTrigger : MonoBehaviour
{
    public DisplayText playerDisplay;
    public UIView label;
    public GameObject player;
    public SaveState save;
    public float activationDistance;
    public string key;
    public bool status = false;

    [TextArea(3, 10)]
    public string[] sentences;

    void Start()
    {
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerDisplay = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<DisplayText>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance && player.GetComponent<Player>().isInputAvailable)
        {
            if (!key.Equals(""))
            {
                if (save.flags[key] && !status)
                {
                    this.enabled = false;
                }
                else if (!save.flags[key] && status)
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
