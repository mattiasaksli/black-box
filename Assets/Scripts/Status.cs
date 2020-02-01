using Doozy.Engine;
using Doozy.Engine.UI;
using UnityEngine;

public class Status : MonoBehaviour
{
    public UIView label;
    public UIView status;
    public GameObject player;
    public float activationDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance)
        {
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
