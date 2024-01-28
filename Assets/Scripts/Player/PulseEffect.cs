using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    public float pulseSpeed = 1.5f; // Adjust the pulse speed as needed
    public float pulseAmount = 0.2f; // Adjust the pulse amount as needed

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Calculate the new scale based on the pulse effect
        float newScale = 1 + Mathf.PingPong(Time.time * pulseSpeed, pulseAmount);

        // Apply the new scale
        transform.localScale = originalScale * newScale;
    }
}