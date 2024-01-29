using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    public TMP_Text textComponent;
    public Image bubbleBackground;

    public void ShowSpeechBubble(string message)
    {
        textComponent.text = message;
        gameObject.SetActive(true);
    }

    public void HideSpeechBubble()
    {
        gameObject.SetActive(false);
    }
}
