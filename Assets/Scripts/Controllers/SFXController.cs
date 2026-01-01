using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SFXController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Text volumeText = null;
    private GameObject AudiosObject;
    private AudioSource[] audioSources;

    public void Start () {
        float savedVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        AudiosObject = GameObject.FindGameObjectWithTag("Audios");
        if (AudiosObject != null)
        {
            audioSources = AudiosObject.GetComponentsInChildren<AudioSource>();

            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource != null)
                {
                    audioSource.volume = savedVolume;
                }
            }
            volumeText.text = ((int)(savedVolume*100)).ToString();
            volumeSlider.value = savedVolume;
        }
    }

    public void ChangeVolume(float volume) {
        volumeText.text = ((int)(volume*100)).ToString();
        PlayerPrefs.SetFloat("SFXVolume", volume);
         if (audioSources != null)
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                AudioSource aud = audioSources[i];
                if (aud != null)
                {
                    aud.volume = volume;
                }
                else
                {
                    Debug.LogError("AudioSource no esta asignado en SFXController");
                }
            }
            
        }
        
    }

}
