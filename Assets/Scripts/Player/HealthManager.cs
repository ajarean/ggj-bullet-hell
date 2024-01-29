using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public PlayerMovement player;
    public SpriteRenderer heart1;
    public SpriteRenderer heart2;
    public SpriteRenderer heart3;
    // Start is called before the first frame update
    void Start()
    {
        heart1.enabled = true;
        heart2.enabled = true;
        heart3.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health < 3) {
            heart3.enabled = false;
        }
        if (player.health < 2) {
            heart2.enabled = false;
        }
        if (player.health < 1) {
            heart1.enabled = false;
        }
    }
}
