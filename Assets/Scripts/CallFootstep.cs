using UnityEngine;

public class CallFootstep : MonoBehaviour
{
    AudioManager AM;
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void PlayStep()
    {
        AM.PlayStep();
    }
}
