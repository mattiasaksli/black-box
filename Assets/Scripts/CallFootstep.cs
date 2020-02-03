using UnityEngine;

public class CallFootstep : MonoBehaviour
{
    AudioManager AM;
    public AudioClip flashlightClip;

    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void PlayStep()
    {
        AM.PlayStep();
    }
    public void PlayFlash()
    {
        AM.PlaySound(flashlightClip, 1f);
    }
}
