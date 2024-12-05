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
            nextTimeToFire = Time.time + fireRate;
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

        audioSource.PlayOneShot(pistolShot);
        muzzleFlash.Play();
        ammoInMag--;
        UIManager.Instance.UpdateBulletsInMagText(ammoInMag);
        anim.SetTrigger("shoot");

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, playerLayer);

            // Deal damage to the target if it has a health component
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }



    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        //anim.SetTrigger("Reload");

        yield return new WaitForSeconds(reloadTime);

        int ammoToReload = Mathf.Min(maxAmmo, 10 - ammoInMag);
        ammoInMag += ammoToReload;
        maxAmmo -= ammoToReload;
        UIManager.Instance.UpdateBulletsInMagText(ammoInMag);
        UIManager.Instance.UpdateOverallAmmoText(maxAmmo);

        isReloading = false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(fpsCamera.transform.position, fpsCamera.transform.position + fpsCamera.transform.forward * range);
        
    }

    }