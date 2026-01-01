using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmonationCollect : MonoBehaviour
{
    private AudioSource audioSource;
    private GunController gunController1;
    private GunController gunController2;
    private GunController gunController;
    private int quantity;
    public int positionIndex;
    public ItemsSpawn itemsSpawn;

   private void Start() {
        GameObject gunControllerMan = GameObject.FindGameObjectWithTag("GunControllerMan");
        GameObject gunControllerWoman = GameObject.FindGameObjectWithTag("GunControllerWoman");

        if (gunControllerMan != null) {
            gunController1 = gunControllerMan.GetComponent<GunController>();
        }
        if (gunControllerWoman != null) {
            gunController2 = gunControllerWoman.GetComponent<GunController>();
        }
        if (gunController1 != null && gunController1.enabled) {
            gunController = gunController1;
        } else {
            gunController = gunController2;
        }
        itemsSpawn = GameObject.FindGameObjectWithTag("ItemsSpawn").GetComponent<ItemsSpawn>();
        audioSource = GameObject.FindGameObjectWithTag("ReloadSciFiSound").GetComponent<AudioSource>();
        quantity = Random.Range(10, 50);
    }

    public void SetIndex(int i) {
        positionIndex = i;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            audioSource.Play();
            gunController.SetAmmonation(quantity);
            itemsSpawn.SetIndexFalseMunition(positionIndex);
            Destroy(gameObject);
        }
    }
}
