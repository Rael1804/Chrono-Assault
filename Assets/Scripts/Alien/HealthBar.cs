using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Image foreground;
    public Image background;
    private bool isAlive = true;

    private void LateUpdate() {
        if (target != null && isAlive) {
            Vector3 direction = (target.position - Camera.main.transform.position).normalized;
            bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0;
            foreground.enabled = !isBehind;
            background.enabled = !isBehind;
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
    }

    public void SetHealthBar(float healthBarValue) {
        foreground.fillAmount = healthBarValue;
    }

    public void TargetDied() {
        foreground.enabled = false;
        background.enabled = false;
        isAlive = false;
    }
}
