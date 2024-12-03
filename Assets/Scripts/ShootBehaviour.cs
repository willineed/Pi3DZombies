using UnityEngine;
using System.Collections;

// This script has been made with the help of Github Copilot
public class ShootBehaviour : MonoBehaviour
{
    public int maxAmmo = 30;
    public int ammoInMag = 10;
    public float reloadTime = 2f;
    public float fireRate = 0.3f;
    public Animator animator;

    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    void Update()
    {
        if (isReloading)
        {
            return;
        }
            

        if (ammoInMag <= 0 && maxAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && ammoInMag < 10 && maxAmmo > 0)
        {
            StartCoroutine(Reload());
        }

    }

    void Shoot()
    {
        if (ammoInMag <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        ammoInMag--;
        animator.SetTrigger("Shoot");

        // Add shooting logic here (e.g., instantiate bullet, play sound, etc.)
        Debug.Log("Bang!");
    }



    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetTrigger("Reload");

        yield return new WaitForSeconds(reloadTime);

        int ammoToReload = Mathf.Min(maxAmmo, 10 - ammoInMag);
        ammoInMag += ammoToReload;
        maxAmmo -= ammoToReload;

        isReloading = false;
    }
}