using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawn : MonoBehaviour
{
    private int oleada = 0;
    private int lastOleada = 0;
    public GameObject munitionPrefab;
    public GameObject kitPrefab;
    public Transform[] spawnPointsMunition;
    private bool[] spawnPointsOccupiedMunition;

    public Transform[] spawnPointsAid;
    private bool[] spawnPointsOccupiedAid;

    private void Start() {
        spawnPointsOccupiedMunition = new bool[spawnPointsMunition.Length];
        spawnPointsOccupiedAid = new bool[spawnPointsAid.Length];
        SpawnMunition();
        SpawnFirstAidKit();
    }

    private void Update() {
        if (oleada > 0 && oleada % 2 == 0 && oleada != lastOleada) {
            lastOleada = oleada;
            SpawnMunition();
            SpawnFirstAidKit();
        }
    }

    public void SetOleada(int oleadaCurrent) {
        oleada = oleadaCurrent;
    }

    public void SpawnMunition() {
        for (int i = 0; i < spawnPointsMunition.Length; i++) {
            if (!spawnPointsOccupiedMunition[i]) {
                GameObject munition = Instantiate(munitionPrefab, spawnPointsMunition[i].position, spawnPointsMunition[i].rotation);
                spawnPointsOccupiedMunition[i] = true;
                AmmonationCollect ammonationCollect = munition.GetComponent<AmmonationCollect>();   
                if (ammonationCollect != null) {
                    ammonationCollect.SetIndex(i);
                }
            }
        }
    }

    public void SetIndexFalseMunition(int i) {
        spawnPointsOccupiedMunition[i] = false;
    }
    
    /*
    public void SetSpawnPointOccupiedMunition(int index, bool isOccupied) {
        if (index >= 0 && index < spawnPointsOccupiedMunition.Length) {
            spawnPointsOccupiedMunition[index] = isOccupied;
        }
    }*/

    public void SpawnFirstAidKit() {
        for (int i = 0; i < spawnPointsAid.Length; i++) {
            if (!spawnPointsOccupiedAid[i]) {
                GameObject kit = Instantiate(kitPrefab, spawnPointsAid[i].position, spawnPointsAid[i].rotation);
                spawnPointsOccupiedAid[i] = true;
                FirstAidCollect firstAidCollect = kit.GetComponent<FirstAidCollect>();   
                if ( firstAidCollect != null) {
                    firstAidCollect.SetIndex(i);
                }
            }
        }
    }

    public void SetIndexFalseAidKit(int i) {
        spawnPointsOccupiedAid[i] = false;
    }
}
