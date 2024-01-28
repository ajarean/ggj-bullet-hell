//Bullet.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 99999f; //99999
    public float roation = 0f; //0
    public float speed = 5f; //5

    private Vector2 spawnpoint;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > bulletLife){
            Destroy(this.gameObject);
            timer = 0;
        }
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer){
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x+spawnpoint.x, y+spawnpoint.y);
    }
}
