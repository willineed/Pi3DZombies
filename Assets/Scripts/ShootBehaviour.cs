using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Audio;

// This script has been made with the help of Github Copilot
public class ShootBehaviour : MonoBehaviour
{
    public int maxAmmo = 30;
    public int ammoInMag = 10;
    public int damage = 10;
    public float range = 100f;
    public float reloadTime = 2f;
    public float fireRate = 0.3f;
    //public Transform shootPoint;
    public Camera fpsCamera;
    public LayerMask playerLayer;
    public AudioClip pistolShot;
    public AudioSource audioSource;
    public ParticleSystem muzzleFlash;

    private Animator anim;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        // Check if the player is reloading, if so return
        if (isReloading)
        {
            return;
        }
            
        // Check if the player is out of ammo and has ammo to reload
        if (ammoInMag <= 0 && maxAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Check if the player is shooting
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
        
        // Check if the player is reloading
        if (Input.GetKeyDown(KeyCode.R) && ammoInMag < 10 && maxAmmo > 0)
        {
            StartCoroutine(Reload());
        }

    }

    // Shoot method
    void Shoot()
    {
        // Check if the player has ammo in the magazine
        if (ammoInMag <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }
        // Play the shooting sound and muzzle flash
        audioSource.PlayOneShot(pistolShot);
        muzzleFlash.Play();
        // Decrease the ammo in the magazine
        ammoInMag--;
        // Update the bullets in the magazine text
        UIManager.Instance.UpdateBulletsInMagText(ammoInMag);
        // Play the shooting animation
        anim.SetTrigger("shoot");

        // Check if the player hits an enemy
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, playerLayer);

            // Deal damage to the target if it has an Enemy component
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }


    // Reload method
    IEnumerator Reload()
    {
        // set isReloading to true
        isReloading = true;
        Debug.Log("Reloading...");

        //anim.SetTrigger("Reload");
        // Wait for the reload time
        yield return new WaitForSeconds(reloadTime);
        // Calculate the amount of ammo to reload
        int ammoToReload = Mathf.Min(maxAmmo, 10 - ammoInMag);
        // Update the ammo in the magazine and overall ammo
        ammoInMag += ammoToReload;
        maxAmmo -= ammoToReload;
        // Update the bullets in the magazine text and overall ammo text
        UIManager.Instance.UpdateBulletsInMagText(ammoInMag);
        UIManager.Instance.UpdateOverallAmmoText(maxAmmo);
        // set isReloading to false
        isReloading = false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(fpsCamera.transform.position, fpsCamera.transform.position + fpsCamera.transform.forward * range);
        
    }

    }