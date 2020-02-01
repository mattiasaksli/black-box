using UnityEngine;

public class ChooseAudio : MonoBehaviour
{
    public AudioSource src;
    public AudioClip working;
    public AudioClip faulty;
    public SaveState save;

    void Start()
    {
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        src = GetComponent<AudioSource>();
        if (save.flags["engine"])
        {
            src.clip = working;
        }
        else
        {
            src.clip = faulty;
        }
        src.Play();
    }
}
