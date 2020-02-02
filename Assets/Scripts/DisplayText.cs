using Doozy.Engine.UI;
using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public UIView textView;
    public string[] sentences;
    public int sentence;
    public bool scrolling;
    public TMP_Text text;
    public bool inDialogue = false;

    public AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void StartDisplay()
    {
        this.GetComponent<Player>().isInputAvailable = false;
        inDialogue = true;
        textView.Show();
        sentence = 0;
        StartCoroutine(ScrollText());
    }

    void Update()
    {
        if (inDialogue && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space)))
        {
            NextSentence();
        }
    }

    public void NextSentence()
    {
        if (scrolling)
        {
            scrolling = false;
            return;
        }
        sentence += 1;
        if (sentence < sentences.Length)
        {
            StartCoroutine(ScrollText());
        }
        else
        {
            this.GetComponent<Player>().isInputAvailable = true;
            text.text = "";
            sentence = 0;
            inDialogue = false;
            textView.Hide();
        }
    }

    IEnumerator ScrollText()
    {
        scrolling = true;
        src.Play();
        string displayText = "";
        foreach (char character in sentences[sentence])
        {
            if (!scrolling)
            {
                displayText = sentences[sentence];
                text.text = displayText;
                break;
            }
            displayText += character;
            text.text = displayText;
            yield return new WaitForSeconds(0.05f);
        }
        scrolling = false;
        src.Stop();
    }
}
