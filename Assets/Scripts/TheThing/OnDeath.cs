using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDeath: MonoBehaviour
{
    public float speed = 2f; // Adjust the speed as needed
    public float targetY = 0f; // Y-coordinate where you want the sprite to stop
    public SpriteRenderer hehe;
    public SpriteRenderer haha;
    public SpriteRenderer laugh;
    // public SpriteRenderer laugh;

    public float showFor = 2f; // Time interval shown
    public float interval1 = 1f;

    public SpeechBubble speechBubble;

    public string[] messages = {"hehe.", "haha.", "HAHAHA!"};
    public string hahamsg = "HAHAHAHAHAHAHAHAHAHAHHAHAHAHAHAHAH";
    public float letterInterval = .1f;

    void Awake() {
        speechBubble.HideSpeechBubble();
        hehe.enabled = true;
        haha.enabled = false;
        laugh.enabled = false;
        // laugh.enabled = false;
    }
    void Start()
    {
        StartCoroutine(MoveToMiddle());
        StartCoroutine(Laugh1());
        StartCoroutine(ShowSpeechBubbles());
        
    }

    IEnumerator MoveToMiddle()
    {
        while (transform.position.y > targetY)
        {
            float step = speed * Time.deltaTime;
            transform.Translate(Vector3.down * step);
            yield return null;
        }
    }

    IEnumerator Laugh1()
    {
        yield return new WaitForSeconds(1f);
        hehe.enabled = true;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator ShowSpeechBubbles()
    {
            for (int i = 0; i < messages.Length; i++)
            {
                yield return new WaitForSeconds(interval1);
                haha.enabled = true;
                yield return StartCoroutine(RevealText(messages[i]));
                haha.enabled = false;
                yield return new WaitForSeconds(showFor);
                speechBubble.HideSpeechBubble();
            }

            laugh.enabled = true;
        StartCoroutine(RevealText(hahamsg));
        yield return new WaitForSeconds(showFor);
        yield return new WaitForSeconds(interval1);
        SceneManager.LoadScene("TitleScreen");
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
}
