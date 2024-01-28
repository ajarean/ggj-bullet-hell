using System.Collections;
using UnityEngine;

public class OnDeath: MonoBehaviour
{
    public float speed = 2f; // Adjust the speed as needed
    public float targetY = 0f; // Y-coordinate where you want the sprite to stop
    public SpriteRenderer hehe;
    public SpriteRenderer laugh;

    void Awake() {
        hehe.enabled = false;
        laugh.enabled = false;
    }
    void Start()
    {
        StartCoroutine(MoveToMiddle());
        StartCoroutine(Laugh1());
        
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
    }
}
