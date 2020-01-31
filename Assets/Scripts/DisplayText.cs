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

    public void StartDisplay()
    {
        inDialogue = true;
        textView.Show();
        sentence = 0;
        StartCoroutine(ScrollText());
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
            text.text = "";
            sentence = 0;
            inDialogue = false;
            textView.Hide();
        }
    }

    IEnumerator ScrollText()
    {
        scrolling = true;
        string displayText = "";
        foreach (char character in sentences[sentence])
        {
            if (!scrolling)
            {
                displayText = sentences[sentence];
                break;
            }
            displayText += character;
            text.text = displayText;
            yield return new WaitForSeconds(0.05f);
        }
        scrolling = false;
    }
}
