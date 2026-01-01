using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioAlien : MonoBehaviour
{
    public AudioSource hitAudio;

    private void Awake() {
        hitAudio = GameObject.FindWithTag("hitSound").GetComponent<AudioSource>();
    }

    public void PlayHitSound() {
        hitAudio.Play();
    } 
    
}
