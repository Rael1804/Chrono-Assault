using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidCollect : MonoBehaviour
{
    public AudioSource audioSource;
    public PlayerMove playerMove;
    private int positionIndex;
    private ItemsSpawn itemsSpawn;

    private void Start() {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        itemsSpawn = GameObject.FindGameObjectWithTag("ItemsSpawn").GetComponent<ItemsSpawn>();
        audioSource = GameObject.FindGameObjectWithTag("FirstAid").GetComponent<AudioSource>();
    }

    public void SetIndex(int i) {
        positionIndex = i;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (playerMove.vida < playerMove.maxvida) {
                audioSource.Play();
                playerMove.MaxHealth();
                itemsSpawn.SetIndexFalseAidKit(positionIndex);
                Destroy(gameObject);
            }
        }
    }
}
