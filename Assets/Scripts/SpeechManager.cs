using System.Collections;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    public SpeechBubble speechBubble;
    public string[] startMessages = {"Let's get started!", "Show me a good time.", "You're up!"}; // Array of messages to display
    public string[] messages = {"This is such fun!", "Again, again!", "Keep it up!", "Don't die!", "Hehehe...", "You can do better."};
    public float showFor = 3f; // Time interval shown
    public float interval1 = 2f;
    public float interval2 = 10f;
    public float letterInterval = .1f;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
        speechBubble.HideSpeechBubble();
        StartCoroutine(ShowSpeechBubbles());
    }

    IEnumerator ShowSpeechBubbles()
    {
        while (true) // Infinite loop to keep showing speech bubbles
        {
            for (int i = 0; i < messages.Length; i++)
            {
                yield return new WaitForSeconds(interval1);
                spriteRenderer.enabled = true;
                yield return StartCoroutine(RevealText(determineMessage(i)));
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(showFor);
                speechBubble.HideSpeechBubble();
                yield return new WaitForSeconds(interval2);
            }
        }
    }

    IEnumerator RevealText(string message)
    {
        speechBubble.ShowSpeechBubble(""); // Show an empty speech bubble initially
    
        for (int i = 0; i <= message.Length; i++)
        {
            string partialText = message.Substring(0, i);
            speechBubble.ShowSpeechBubble(partialText);
            yield return new WaitForSeconds(letterInterval);
        }
    }
    
    private string determineMessage(int i)
    {
        if (i == 0) {
            return startMessages[Random.Range(0, startMessages.Length - 1)];
        }
        else {
            return messages[Random.Range(0, messages.Length - 1)];
        }
    }
}
