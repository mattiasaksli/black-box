using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChooseLight : MonoBehaviour
{
    Light2D lightSource;
    SpriteRenderer render;
    ChooseAudio CA;

    public Color workingColor = Color.green;
    public Color brokenColor = Color.red;
    public Sprite workingSprite;
    public Sprite brokenSprite;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        lightSource = GetComponentInChildren<Light2D>();
        CA = GetComponentInParent<ChooseAudio>();
    }


    void Update()
    {
        if (CA.broken)
        {
            lightSource.color = brokenColor;
            render.sprite = brokenSprite;
        }
        else
        {
            lightSource.color = workingColor;
            render.sprite = workingSprite;
        }
    }
}
