using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void Interact()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Equals("Start"))
        {
            save.startNote = true;
        }
        else if (sceneName.Equals("PressureRoom"))
        {
            save.pressureNote = true;
        }
        else if (sceneName.Equals("Base"))
        {
            save.baseNote = true;
        }
        else if (sceneName.Equals("LifeSupport"))
        {
            save.lifeNote = true;
        }

        label.Hide();
        playerDisplay.sentences = sentences;
        playerDisplay.StartDisplay();
    }
}
