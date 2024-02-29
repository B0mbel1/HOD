using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingfinal : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public static int bulletsLeft;
    int bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;

    public AudioClip shootSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        if (!MenuPauzy.GameIsPaused)
        {
            if (!FreeTeleportation.teleportState)
            {
                if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
                else shooting = Input.GetKeyDown(KeyCode.Mouse0);

                if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

                //Shoot
                if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
                {
                    bulletsShot = bulletsPerTap;
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        readyToShoot = false;
        // Utwórz pocisk na pozycji bulletSpawn
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Pobierz pozycjê myszy w przestrzeni œwiata
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // SprawdŸ, czy raycast trafi³ w coœ
        if (Physics.Raycast(ray, out hit))
        {
            // Oblicz kierunek od pocisku do miejsca, gdzie raycast trafi³
            Vector3 directionToMouse = (hit.point - bulletSpawn.position).normalized;

            // Ustaw prêdkoœæ pocisku w kierunku miejsca, gdzie raycast trafi³
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = directionToMouse * bulletSpeed;
            obrazeniaOK czyOK = hit.collider.GetComponent<obrazeniaOK>();
            if (czyOK != null)
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    czyOK.zadajObrazenia(damage + 3);
                }
                else
                {
                    czyOK.zadajObrazenia(damage);
                }
            }
        }
        else
        {
            // Jeœli raycast nie trafi³ w nic, pocisk bêdzie lecia³ w kierunku kursora myszy
            Vector3 directionToMouse = (ray.origin + ray.direction * 100f - bulletSpawn.position).normalized;
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = directionToMouse * bulletSpeed;
        }

        audioSource.PlayOneShot(shootSound);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
        if (bulletsLeft == 0)
        {
            Reload();
        }
    }




    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        audioSource.PlayOneShot(reloadSound);
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
