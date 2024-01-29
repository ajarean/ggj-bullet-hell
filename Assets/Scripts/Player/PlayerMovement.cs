using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private SpriteRenderer _spriteRenderer;
    public int health = 3;
    private bool isInvincible = false;
    public float invincibilityDuration = 1.5f;
    public float flashInterval = 0.1f;


    public Image black;
    public Color fadeColor = new Color(0f, 0f, 0f, 0f);
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("Script called");
        
    }

    private void FixedUpdate() {
        // _rigidbody.velocity = new Vector2(1, 0.5f); // 1 unit per second on the x axis, 0.5 on y
        _rigidbody.velocity = _movementInput * _speed;
    }

    private void OnMove(InputValue inputValue) {
        Vector2 vector = inputValue.Get<Vector2>();
        _movementInput = vector;
        // Debug.Log("input detected. Magnitude(" + vector.x + ", " + vector.y);

    }

    void OnTriggerEnter2D(Collider2D other) {
        // Check if the other collider is an enemy, obstacle, or any other object you want to interact with
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Collision with bullet detected.");
            // Perform actions when colliding with an enemy
            if (!isInvincible) {
                takeDamage();
                StartCoroutine(InvincibilityCoroutine());
            }
            // SceneManager.LoadScene("DeathScreen");

        }
    }

    private void takeDamage() {
        health--;
        if (health <= 0)
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        _spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // Set the sprite color to semi-transparent white
        yield return new WaitForSeconds(flashInterval);
        _spriteRenderer.color = Color.white; // Reset the sprite color to normal
        yield return new WaitForSeconds(flashInterval);
        _spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(flashInterval);
        _spriteRenderer.color = Color.white; // Reset the sprite color to normal
        isInvincible = false;
    }
}

