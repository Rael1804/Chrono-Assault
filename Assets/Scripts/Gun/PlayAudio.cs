using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource reloadSound;

    public void PlayReloadSound() {
        reloadSound.Play();
    } 
    
}
