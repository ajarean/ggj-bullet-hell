using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupils : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform

    private void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the eye to the player
            Vector3 directionToPlayer = playerTransform.position - transform.position;

            // Calculate the rotation angle based on the direction
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Set the rotation of the eye sprite to face the player
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
