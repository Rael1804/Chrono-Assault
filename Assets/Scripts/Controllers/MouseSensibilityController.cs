using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseSensibilityController : MonoBehaviour
{   public Slider mouseSlider;
    public float currentValue;
    [SerializeField] private Text mouseText = null;

    private void Start() {
        float saveMouseSensibility = PlayerPrefs.GetFloat("mouseSensibility", 50f);
        mouseSlider.value = saveMouseSensibility;
        float mousePercentage = Mathf.InverseLerp(10f, 200f, saveMouseSensibility) * 100.0f;
        mouseText.text = ((int)mousePercentage).ToString();
        currentValue = saveMouseSensibility;
    }

    public void ChangeMouseSensibility(float mouseSensibility) {
        PlayerPrefs.SetFloat("mouseSensibility", mouseSensibility);
        float mousePercentage = Mathf.InverseLerp(10f, 200f, mouseSensibility) * 100.0f;
        mouseText.text = ((int)mousePercentage).ToString();
        currentValue = mouseSensibility;
    }
}
