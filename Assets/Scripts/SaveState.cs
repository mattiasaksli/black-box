using System.Collections.Generic;
using UnityEngine;

public class SaveState : MonoBehaviour
{
    public Dictionary<string, bool> flags;
    private static bool created;

    void Start()
    {
        if (created)
        {
            Destroy(this.gameObject);
        }
        else
        {
            flags.Add("valve", false);
            flags.Add("engine", false);
            flags.Add("pipes", false);
        }
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
        if (flags[key])
        {
            flags[key] = false;
            return;
        }
        flags[key] = true;
    }
}
