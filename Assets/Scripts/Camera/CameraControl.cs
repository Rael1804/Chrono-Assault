using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CameraControl : MonoBehaviour
{
    public float mouseSensibility;
    private float rotation_x = 0;
    public Transform playerTransform;

    void Start()
    {
        //Para no poder usar el raton fuera de la escena
        mouseSensibility = PlayerPrefs.GetFloat("mouseSensibility", 50f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseSensibility =  PlayerPrefs.GetFloat("mouseSensibility", 50f);

        float mouse_x = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        rotation_x -= mouse_y;
        //No permitir girar 360 grados
        rotation_x = Mathf.Clamp(rotation_x, -30f, 30f);
      
        transform.localRotation = Quaternion.Euler(rotation_x, 0, 0);

        playerTransform.Rotate(Vector3.up * mouse_x);
}
}

