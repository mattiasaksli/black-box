using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;

public class DialogueWin : MonoBehaviour
{
    public GameObject player;
    public SaveState save;
    public ChooseAudio chooser;
    UIView view;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
    }

    public void WinDialogue()
    {
        StartCoroutine(GameWon());
    }

    private IEnumerator GameWon()
    {
        save.changeFlag("pipes");
        chooser.Choose();
        yield return new WaitForSeconds(1f);
        player.GetComponent<Player>().isInputAvailable = true;
    }
}
