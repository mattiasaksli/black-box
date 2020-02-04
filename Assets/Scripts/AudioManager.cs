using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)] public float footstepVolume;

    public AudioMixerGroup output;
    public AudioClip[] stepSounds;
    public List<AudioSource> sources;

    public static bool created;
    int lastStep;

    void Start()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;

            for (int i = 0; i < 7; i++)
            {
                AudioSource s = gameObject.AddComponent<AudioSource>();
                s.playOnAwake = false;
                s.loop = false;
                s.spatialBlend = 0f;
                s.outputAudioMixerGroup = output;
                sources.Add(s);
            }
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        AudioSource ambient = GetComponents<AudioSource>()[0];
        AudioSource music = GetComponents<AudioSource>()[1];

        if (!ambient.isPlaying)
        {
            ambient.Play();
        }
        if (!music.isPlaying)
        {
            music.Play();
        }
    }

    public void PlayStep()
    {
        foreach (AudioSource sc in sources)
        {
            if (!sc.isPlaying)
            {
                int step = 0;
                while (true)
                {
                    step = Random.Range(0, 4);
                    if (step != lastStep)
                    {
                        lastStep = step;
                        break;
                    }
                }
                sc.volume = footstepVolume;
                sc.clip = stepSounds[step];
                sc.Play();
                return;
            }
        }
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        foreach (AudioSource sc in sources)
        {
            if (!sc.isPlaying || sc.clip == null)
            {
                sc.volume = volume;
                sc.clip = clip;
                sc.Play();
                return;
            }
        }
    }
}
