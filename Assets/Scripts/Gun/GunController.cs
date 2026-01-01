using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class GunController : MonoBehaviour
{
    public PlayerMove playerMove;
    public CameraSwitch cameraSwitch;
    public ParticleSystem partSystem;
    public GameObject impactEffectTerrain;
    public AudioSource audioSource;
    public AudioSource audioSourceReload;
    public AudioSource audioSourceNoAmmo;
    public float damage = 50;
    public float range = 100f;
    public float impactForce = 30f;
    public CurrentGun currentGun;
    

    public float timeToShoot=1f;
    private float lastShot;
    public EnemiesCounter enemiesCounter;
    

    //Ammonation
    public float reloadTime = 1f;
    public int currentAmmonation;
    public int ammonationForReload;
    public int maxAmmonation = 10;
    public bool isReloading = false;
    public Animator animator;
    
    public Camera firstPersonCamera;

    //Controlador Balas
    public Bullet balas;

    private void Start() {
        currentGun = GetComponent<CurrentGun>();
        currentAmmonation = maxAmmonation;
        ammonationForReload = maxAmmonation;
        int recargasNecesarias = Mathf.CeilToInt((float)ammonationForReload / 10);
        enemiesCounter.SetReload(recargasNecesarias); 
    }

    private void Update() {
        if (currentAmmonation > 0 && !isReloading) {
            //animator.SetBool("reload", false);
            ClickShoot();
        } else if (ammonationForReload > 0 && !isReloading && currentAmmonation <= 0 ) {
            StartCoroutine(ReloadFunction());
        } else if (currentAmmonation <= 0  && !isReloading && ammonationForReload <= 0 ) {
            ClickShootNoAmo();  
        } 
        
        if (ammonationForReload > 0 && currentAmmonation < maxAmmonation  && !isReloading) {
            ClickReload();
        }
        
    }

    private void ClickReload() {
        if (Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(ReloadFunction());
        }
    }

    private void ClickShoot() {
        if (Input.GetButton("Fire1") & playerMove.IsFirstPerson() && Time.time - lastShot >= timeToShoot) {
            Shoot();
            lastShot = Time.time;
        }
    }


    private void ClickShootNoAmo() {
        if (Input.GetButton("Fire1") & playerMove.IsFirstPerson() && Time.time - lastShot >= timeToShoot) {
            audioSourceNoAmmo.Play();
            lastShot = Time.time;
        }
    }

    private IEnumerator ReloadFunction() {
        isReloading = true;
        animator.SetBool("reload", true);

        if (!playerMove.IsFirstPerson()) {
            audioSourceReload.Play();
        }
        if (ammonationForReload >= maxAmmonation) {
            yield return new WaitForSeconds(reloadTime);
            ammonationForReload -= maxAmmonation;
            currentAmmonation = maxAmmonation;
            balas.ActivarBalas(currentAmmonation);
        } else if (ammonationForReload > 0) {
            yield return new WaitForSeconds(reloadTime);
            currentAmmonation = ammonationForReload;
            balas.ActivarBalas(currentAmmonation);
            ammonationForReload = 0;
        }
        int recargasNecesarias = Mathf.CeilToInt((float)ammonationForReload / 10);
        enemiesCounter.SetReload(recargasNecesarias);
        
        
        animator.SetBool("reload", false);
        isReloading = false;
    }

    public void SetAmmonation(int quantity) {
        ammonationForReload += quantity;
        currentGun.ammo[currentGun.currentGunIndex] += quantity;
        int recargasNecesarias = Mathf.CeilToInt((float)ammonationForReload / 10);
        enemiesCounter.SetReload(recargasNecesarias);
    }

    public void SetAmmonationNew(int quantity) {
        ammonationForReload = quantity;
        int recargasNecesarias = Mathf.CeilToInt((float)ammonationForReload / 10);
        enemiesCounter.SetReload(recargasNecesarias);
    }

   

    private void Shoot() {
        animator.SetTrigger("shoot");
        currentAmmonation--;
        currentGun.ammo[currentGun.currentGunIndex]--;
        balas.DesactivarBalas(currentAmmonation);
       
        partSystem.Play();
        audioSource.Play();
        RaycastHit hit;
        if (Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out hit, range)) {
            //Debug.Log(hit.transform.name);
            /*Enemigo enemy = hit.transform.GetComponent<Enemigo>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
                if (hit.rigidbody != null) {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            } else  {
                GameObject impact = Instantiate(impactEffectTerrain, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }*/
            var hitBox = hit.collider.GetComponent<HitBox>();
            if (hitBox != null) {
                Vector3 rayDirection = firstPersonCamera.transform.forward;
                hitBox.OnRaycastHit(this, rayDirection);
            } else {
                GameObject impact = Instantiate(impactEffectTerrain, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }

    }


}