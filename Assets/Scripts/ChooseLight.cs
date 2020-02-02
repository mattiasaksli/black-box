using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChooseLight : MonoBehaviour
{
    Light2D light;
    SpriteRenderer renderer;
    ChooseAudio CA;

    public Color workingColor = Color.green;
    public Color brokenColor = Color.red;
    public Sprite green;
    public Sprite red;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        light = GetComponentInChildren<Light2D>();
        CA = GetComponentInParent<ChooseAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CA.broken)
        {
            light.color = brokenColor;
            renderer.sprite = red;
        }
        else
        {
            light.color = workingColor;
            renderer.sprite = green;
        }
    }
}
