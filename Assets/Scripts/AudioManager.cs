using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)] public float footstepVolume;

    public AudioMixerGroup output;
    public AudioClip[] stepSounds;
    public List<AudioSource> sources;
    public List<AudioSource> ambientSounds;

    private static bool created;
    private int lastStep;

    void Start() {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;

            ambientSounds = new List<AudioSource>(GetComponents<AudioSource>());
            SceneManager.sceneLoaded += (scene, mode) => {
                foreach (var ambientSound in ambientSounds) {
                    if (!ambientSound.isPlaying) {
                        ambientSound.Play();
                    }
                }
            }; 
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
            Destroy(gameObject);
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