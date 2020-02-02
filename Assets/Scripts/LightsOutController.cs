using Doozy.Engine;
using System.Collections;
using UnityEngine;

public class LightsOutController : MonoBehaviour
{
    public LightsOutButton[] buttons = new LightsOutButton[9];
    public GameObject player;
    public SaveState save;
    public AudioSource buttonSrc;
    public AudioClip winClip;
    public ChooseAudio chooser;

    void Start()
    {
        buttonSrc = GetComponent<AudioSource>();
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        buttons = GetComponentsInChildren<LightsOutButton>();
        player = GameObject.FindGameObjectWithTag("Player");
        buttonSrc.Play();
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
            buttonSrc.clip = winClip;
            buttonSrc.Play();

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
            chooser.Stop();
            this.enabled = false;
        }
    }

    private IEnumerator GameWon()
    {
        yield return new WaitForSeconds(1f);
        GameEventMessage.SendEvent("LightsOutWon");
        save.changeFlag("engine");
        chooser.Choose();
        player.GetComponent<Player>().isInputAvailable = true;
    }
}
