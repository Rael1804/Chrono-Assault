using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flotar : MonoBehaviour
{
    private float floatSpeed = 1f;
    private float floatHeight = 0.1f;
    private float startY;

    private void Start() {
        startY = transform.position.y;
    }
    
    private void Update() {
        float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
