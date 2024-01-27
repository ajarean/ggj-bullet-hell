using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        Debug.Log("Script called");
    }

    private void FixedUpdate() {
        // _rigidbody.velocity = new Vector2(1, 0.5f); // 1 unit per second on the x axis, 0.5 on y
        _rigidbody.velocity = _movementInput * _speed;
    }

    private void OnMove(InputValue inputValue) {
        Vector2 vector = inputValue.Get<Vector2>();
        _movementInput = vector;
        Debug.Log("input detected. Magnitude(" + vector.x + ", " + vector.y);

    }
}

