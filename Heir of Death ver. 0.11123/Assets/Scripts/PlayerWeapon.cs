using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 1;
    public float lifeTime = 3;
    public int maxAmmo = 6; // Maksymalna iloœæ pocisków
    private int currentAmmo; // Obecna iloœæ pocisków
    private bool isReloading = false;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    private AudioSource audioSource;

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        MenuPauzy.GameIsPaused = false;
    }

    void Update()
    {
        if (!MenuPauzy.GameIsPaused)
        {
            if (isReloading)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (currentAmmo > 0)
                {
                    Fire();
                }
                else
                {
                    StartCoroutine(Reload());
                }
            }
            if (Input.GetKeyDown(KeyCode.R)) // SprawdŸ, czy zosta³ naciœniêty klawisz R
            {
                StartCoroutine(Reload());
            }
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());
        bullet.transform.position = bulletSpawn.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
        //StartCoroutine("DestroyProjectile");
        currentAmmo--; // Zmniejsz liczbê dostêpnych pocisków
        audioSource.PlayOneShot(shootSound);

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator DestroyProjectile(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(1f); // Czas trwania prze³adowania (1 sekunda w tym przyk³adzie)

        currentAmmo = maxAmmo; // Ustaw liczbê pocisków na maksymaln¹

        isReloading = false;
        audioSource.PlayOneShot(reloadSound);
    }
}
