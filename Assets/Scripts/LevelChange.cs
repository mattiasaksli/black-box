using Doozy.Engine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public UIView transitionView;
    public UIView label;
    public GameObject player;
    public SaveState save;
    public AudioManager AM;
    public string sceneToLoad;
    public float activationDistance;
    public string key;

    [Range(0f, 1f)] public float useVolume;
    public AudioClip useSound;

    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        transitionView = GameObject.Find("View - Transition").GetComponent<UIView>();
        player = GameObject.FindGameObjectWithTag("Player");
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < activationDistance && player.GetComponent<Player>().isInputAvailable)
        {
            if (!key.Equals(""))
            {
                if (!save.flags[key])
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
        if (sceneToLoad.Equals("PressureRoom"))
        {
            save.startRoomSpawnPos = new Vector3(transform.position.x, 0, 0);
        }
        else if (sceneToLoad.Equals("PowerRoom") || sceneToLoad.Equals("LifeSupport"))
        {
            save.coreRoomSpawnPos = new Vector3(transform.position.x, 0, 0);
        }
        label.Hide();
        StartCoroutine(LevelTransition());
    }

    IEnumerator LevelTransition()
    {
        player.GetComponent<Player>().isInputAvailable = false;
        player.GetComponent<Player>().levelChanging = true;
        AM.PlaySound(useSound, useVolume);
        transitionView.Show();
        yield return new WaitForSeconds(useSound.length - 1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
