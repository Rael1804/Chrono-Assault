using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CurrentGun : MonoBehaviour
{

    public GameObject[] Guns;
    public GameObject[] GunsFirstPerson;
    public int currentGunIndex = 0;
    public int lastIndex = 0;
    public GameObject currentGun1Person;
    public GameObject currentGun;
    
    public PlayerMove playerMove;

    public GameObject RightHandIK;
    public GameObject LeftHandIK;

    private GunController gunController;

    TwoBoneIKConstraint twoBoneIKConstraintLeft;
    TwoBoneIKConstraint twoBoneIKConstraintRight;

    public float[] timeToShoot;
    public float[] damage;
    public float[] range;
    public int[] ammo;


    private void Start() {
        gunController = GetComponent<GunController>();

        twoBoneIKConstraintLeft = LeftHandIK.GetComponent<TwoBoneIKConstraint>();
        twoBoneIKConstraintRight = RightHandIK.GetComponent<TwoBoneIKConstraint>();
         
    }

    private void UpdateWeapon() {
        currentGun.SetActive(false);
        currentGun1Person.SetActive(false);
        currentGun = Guns[currentGunIndex]; 
        currentGun1Person = GunsFirstPerson[currentGunIndex];
        if(playerMove.IsFirstPerson()) {
            currentGun1Person.SetActive(true);
        } else {
            currentGun.SetActive(true);
        }
        //actualizar arma en GunController
        Transform[] children = currentGun.GetComponentsInChildren<Transform>();
        
        twoBoneIKConstraintLeft.data.target = children[0];
        twoBoneIKConstraintRight.data.target = children[1];



        gunController.timeToShoot = timeToShoot[currentGunIndex];
        gunController.damage = damage[currentGunIndex];
        gunController.range = range[currentGunIndex];
        gunController.animator = currentGun1Person.GetComponent<Animator>();
        gunController.partSystem = currentGun1Person.GetComponentInChildren<ParticleSystem>();    
        gunController.SetAmmonationNew(ammo[currentGunIndex]);
        playerMove.gunNonAim = currentGun;
        playerMove.gunFirstPerson = currentGun1Person;
    }

    private void Update() {
        if (currentGunIndex != lastIndex) {
            UpdateWeapon();
            lastIndex = currentGunIndex;
        }

    }

    public void ChangeGunIndex(int index) {
        currentGunIndex = index;
    }

    public int getIndex() {
        return currentGunIndex;
    }
}