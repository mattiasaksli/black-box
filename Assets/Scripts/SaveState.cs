using System.Collections.Generic;
using UnityEngine;

public class SaveState : MonoBehaviour
{
    public Dictionary<string, bool> flags = new Dictionary<string, bool>();

    public bool valve;
    public bool engine;
    public bool pipes;

    public static bool created;

    void Start()
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
        foreach (string key in flags.Keys)
        {
            flags[key] = false;
        }
    }

    public void changeFlag(string key)
    {
        flags[key] = true;
    }
}
