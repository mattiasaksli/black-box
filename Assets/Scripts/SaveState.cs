using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveState : MonoBehaviour
{
    public Dictionary<string, bool> flags = new Dictionary<string, bool>();

    public bool valve;
    public bool engine;
    public bool pipes;
    public bool goodEndUnlocked = false;
    public Vector3 startRoomSpawnPos;
    public Vector3 coreRoomSpawnPos;
    public GameObject player;

    public bool startNote = false;
    public bool pressureNote = false;
    public bool baseNote = false;
    public bool lifeNote = false;

    public static bool created;

    void Awake()
    {
        if (created)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        created = true;

        flags.Add("valve", valve);
        flags.Add("engine", engine);
        flags.Add("pipes", pipes);
    }

    public void Clear()
    {
        created = false;
        Destroy(this.gameObject);
    }

    public void changeFlag(string key)
    {
        flags[key] = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Clear();
        }
        if (s.buildIndex == 1 && startRoomSpawnPos != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = startRoomSpawnPos;
        }
        else if (s.buildIndex == 3 && coreRoomSpawnPos != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = coreRoomSpawnPos;
        }
        else if (s.buildIndex == 6)
        {
            goodEndUnlocked = startNote && pressureNote && baseNote && lifeNote;
        }
    }
}
