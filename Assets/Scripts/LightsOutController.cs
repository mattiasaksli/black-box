using Doozy.Engine;
using System.Collections;
using UnityEngine;

public class LightsOutController : MonoBehaviour
{
    public LightsOutButton[] buttons = new LightsOutButton[9];
    public GameObject player;
    public SaveState save;
    public AudioSource buttonSrc;
    public ChooseAudio chooser;

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

        //Win
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

        //Lose
        if (currentlyAllOff)
        {
            player.GetComponent<Player>().PowerFailure();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator GameWon()
    {
        save.changeFlag("engine");
        chooser.Choose();
        yield return new WaitForSeconds(1f);
        player.GetComponent<Player>().isInputAvailable = true;
    }
}
