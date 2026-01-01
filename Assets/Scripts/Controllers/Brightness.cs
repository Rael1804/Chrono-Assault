using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessProfile postProcessProfile;
    public PostProcessLayer postProcessLayer;
    private AutoExposure autoExposure;
    [SerializeField] private Text brightText = null;

    private void Start() {
        float brightValue = PlayerPrefs.GetFloat("brightness", 1.0f);
        postProcessProfile.TryGetSettings(out autoExposure);
        ChangeBrightness(brightValue);
        brightnessSlider.value = brightValue;
        float brightPercentage = Mathf.InverseLerp(0.1f, 3.0f, brightValue) * 100.0f;

        brightText.text = ((int)brightPercentage).ToString();
    }

    public void ChangeBrightness(float brightness) {
        PlayerPrefs.SetFloat("brightness", brightness);
        if (brightness != 0) {
            autoExposure.keyValue.value = brightness;
        } else {
            autoExposure.keyValue.value = 0.05f;
        }
        float brightPercentage = Mathf.InverseLerp(0.1f, 3.0f, brightness) * 100.0f;
        brightText.text = ((int)brightPercentage).ToString();
    }
}
