using Doozy.Engine;
using System.Collections;
using UnityEngine;

public class LightsOutController : MonoBehaviour
{
    public LightsOutButton[] buttons = new LightsOutButton[9];
    public GameObject player;
    public SaveState save;
    public AudioSource src;
    public AudioClip working;
    public AudioClip faulty;
    public DisplayTrigger DP;
    public Status ST;

    void Start()
    {
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        buttons = GetComponentsInChildren<LightsOutButton>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        buttons = GetComponentsInChildren<LightsOutButton>();

        bool currentlyAllOn = true;
        foreach (LightsOutButton b in buttons)
        {
            currentlyAllOn = currentlyAllOn && b.isOn;
        }

        if (currentlyAllOn)
        {
            GameEventMessage.SendEvent("LightsOutWon");
            StartCoroutine(GameWon());
        }

        bool currentlyAllOff = true;
        foreach (LightsOutButton b in buttons)
        {
            currentlyAllOff = currentlyAllOff && !b.isOn;
        }

        if (currentlyAllOff)
        {
            player.GetComponent<Player>().PowerFailure();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator GameWon()
    {
        yield return new WaitForSeconds(1f);
        save.changeFlag("engine");
        src.clip = working;
        src.Play();
        ST.enabled = false;
        DP.enabled = true;
        // Save state and play sounds
    }
}
