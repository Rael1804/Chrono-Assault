using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollect : MonoBehaviour
{

    public CurrentGun currentGun;
    public CurrentGun currentGun1;
    public CurrentGun currentGun2;
    public int index;
    public AudioSource audioSource;

    public GameObject[] prefabs;
    private bool inside = false;

    private void Start() {
        audioSource =  GameObject.FindGameObjectWithTag("pickGunSound").GetComponent<AudioSource>();
        GameObject gunController1 = GameObject.FindGameObjectWithTag("GunControllerMan");
        GameObject gunController2 = GameObject.FindGameObjectWithTag("GunControllerWoman");
        

        if (gunController1 != null) {
            currentGun1 = gunController1.GetComponent<CurrentGun>();
        }
        if (gunController2 != null) {
            currentGun2 = gunController2.GetComponent<CurrentGun>();
        }
        
        if (currentGun1 != null && currentGun1.enabled) {
            currentGun = currentGun1;
        } else if(currentGun2 != null && currentGun2.enabled){
            currentGun = currentGun2;
        }
        
    }

    private void Update() {
        if (inside) {
            if (Input.GetKeyDown(KeyCode.Q)) {
               ChangeGun();
            }
        }
    }

    private void ChangeGun() {
        if (currentGun != null) {
        audioSource.Play();
        
        int lastIndex = currentGun.getIndex();
        Instantiate(prefabs[lastIndex], transform.position, Quaternion.identity);
       
        currentGun.ChangeGunIndex(index);
        Destroy(gameObject);
        }
    }
        

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !gameObject.CompareTag("DroppedWeapon")) {
            ChangeGun();
        } else if (other.CompareTag("Player") && gameObject.CompareTag("DroppedWeapon")) {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player") && gameObject.CompareTag("DroppedWeapon")) {
            inside = false;
        }
    }

    public bool IsInside() {
        return inside;  
    }
}
