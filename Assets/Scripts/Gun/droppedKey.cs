using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class droppedKey : MonoBehaviour
{
    public Image image;
    public Transform target;
    public Vector3 offset = new Vector3(0,1f,0);
    public GunCollect gunCollect;

    private void LateUpdate() {
         transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        if (gunCollect.IsInside()) {
            image.enabled = true;
        } else {
            image.enabled = false;
        }
    }

}
