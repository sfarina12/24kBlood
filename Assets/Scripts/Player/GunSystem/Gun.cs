using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform cam;
    public ParticleSystem muzzle;

    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;

    [SerializeField] float fireRate = 5f;

    [SerializeField] bool rapidFire = false;

    [SerializeField] int maxAmmo;
    int currentAmmo;

    [SerializeField] float reloadTime;
    WaitForSeconds reloadWait;

    WaitForSeconds rapidFireWait;

    private void Awake(){
        //Everytime the scene is loaded, this part will be called. BEWARE: this works only once after the scene is loaded
        cam = Camera.main.transform;
        rapidFireWait = new WaitForSeconds(1/fireRate);
        currentAmmo = maxAmmo;
        reloadWait = new WaitForSeconds(reloadTime);
    }
    public void Shoot(){
        currentAmmo--;
        Debug.Log("Ammo left: "+currentAmmo);
        RaycastHit hit;
        //muzzle.Play();
        if(Physics.Raycast(cam.position, cam.forward, out hit, range)){
            if(hit.collider.GetComponent<Damageable>() != null){
                hit.collider.GetComponent<Damageable>().TakeDamage(damage);
            }
        }
    }

    public IEnumerator FastFire(){
        if(CanShoot()){
            Shoot();
            if(rapidFire){
                while(CanShoot()){
                    yield return rapidFireWait;
                    Shoot();
                }
                StartCoroutine(Reload());
            }
        } else{
            StartCoroutine(Reload());
        }
    }

    public void ReloadFunction(){
        Debug.Log("Hi");
        StartCoroutine(Reload());
    }

    IEnumerator Reload(){
        if(currentAmmo == maxAmmo){
            yield return null;
        }
        Debug.Log("Reloading");
        yield return reloadWait;
        currentAmmo = maxAmmo;
        Debug.Log("Reloading completed");
    }

    bool CanShoot(){
        return currentAmmo > 0;
    }
}
