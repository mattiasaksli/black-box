using Doozy.Engine;
using Doozy.Engine.UI;
using UnityEngine;

public class Status : MonoBehaviour
{
    public UIView label;
    public UIView status;
    public GameObject player;
    public float activationDistance;
    public SaveState save;
    public string key;
    DisplayTrigger DP;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance)
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
                GameEventMessage.SendEvent("Interacting");
                label.Hide();
            }
        }
        else
        {
            label.Hide();
        }
    }
}
