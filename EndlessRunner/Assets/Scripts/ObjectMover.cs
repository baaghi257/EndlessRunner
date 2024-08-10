using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float moveSpeed = 10;
    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        // Destroy the object when it moves out of the screen
        if (transform.position.z < -60)
        {
            Destroy(gameObject);
        }
    }
}
