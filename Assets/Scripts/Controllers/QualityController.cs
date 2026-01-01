using UnityEngine;
using UnityEngine.UI;

public class QualityController : MonoBehaviour
{
    public Dropdown dropdown;

    private void Start()
    {
        int quality = PlayerPrefs.GetInt("quality", 2);
        dropdown.value = quality;
        QualitySettings.SetQualityLevel(quality);
        dropdown.RefreshShownValue();
    }

    public void SetQuality(int quality)
    {
        PlayerPrefs.SetInt("quality", quality);
        QualitySettings.SetQualityLevel(quality);
    }
}
  
