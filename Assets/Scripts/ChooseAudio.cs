using UnityEngine;

public class ChooseAudio : MonoBehaviour
{
    public AudioSource src;
    public AudioClip working;
    public AudioClip faulty;
    public SaveState save;
    public DisplayTrigger DP;
    public Status ST;
    public string key = "engine";

    public bool broken = true;

    void Start()
    {
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        DP = GetComponent<DisplayTrigger>();
        ST = GetComponent<Status>();
        src = GetComponent<AudioSource>();
        Choose();
    }

    public void Choose()
    {
        if (save.flags[key])
        {
            src.clip = working;
            ST.enabled = false;
            DP.enabled = true;
            broken = false;
        }
        else
        {
            broken = true;
            ST.enabled = true;
            DP.enabled = false;
            src.clip = faulty;
        }
        src.Play();

    }

    public void Stop()
    {
        src.Stop();
    }
}
