using Doozy.Engine;
using Doozy.Engine.UI;
using UnityEngine;

public class Status : MonoBehaviour
{
    public UIView label;
    public UIView status;
    public GameObject player;
    public SaveState save;
    public float activationDistance;
    public string key;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance && player.GetComponent<Player>().isInputAvailable)
        {
            if (!key.Equals(""))
            {
                if (save.flags[key])
                {
                    this.enabled = false;
                }
            }
            label.Show();
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x > Screen.currentResolution.width * 0.4f && touch.position.x < Screen.currentResolution.width * 0.6f && touch.phase == TouchPhase.Began)
                {
                    Interact();
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }
        else
        {
            label.Hide();
        }
    }
    void Interact()
    {
        GameEventMessage.SendEvent("Interacting");
        player.GetComponent<Player>().isInputAvailable = false;
        label.Hide();
    }
}
