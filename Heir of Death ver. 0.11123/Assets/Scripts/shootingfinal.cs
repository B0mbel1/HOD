using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class shootingfinal : NetworkBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public static int bulletsLeft1;
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
        bulletsLeft1 = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }
        MyInput();  //blad
    }

    private void MyInput()
    {
        if (!MenuPauzy.GameIsPaused)
        {
            if (!FreeTeleportation.teleportState)
            {
                if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
                else shooting = Input.GetKeyDown(KeyCode.Mouse0);

                if (Input.GetKeyDown(KeyCode.R) && bulletsLeft1 < magazineSize && !reloading) Reload();

                //Shoot
                if (readyToShoot && shooting && !reloading && bulletsLeft1 > 0)
                {
                    bulletsShot = bulletsPerTap;
                    Shoot();    //blad
                }
            }
        }
    }
    void Shoot()
    {
        readyToShoot = false;
        // Utwórz pocisk na pozycji bulletSpawn
        GameObject bullet = NetworkManager.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Pobierz pozycjê myszy w przestrzeni œwiata
        Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);                //blad
        RaycastHit hit;

        // SprawdŸ, czy raycast trafi³ w coœ
        if (Physics.Raycast(ray, out hit))
        {
            // Oblicz kierunek do miejsca trafienia
            Vector3 targetPosition = hit.point;
            Rigidbody bulletRigidbody =  bullet.GetComponent<Rigidbody>();

            // Przemieszczaj pocisk stopniowo w kierunku celu
            StartCoroutine(MoveBulletToPosition(bulletRigidbody, targetPosition, bulletSpeed));
        }
        else
        {
            // Jeœli raycast nie trafi³ w nic, pocisk bêdzie lecia³ w kierunku kursora myszy
            Vector3 directionToMouse = (ray.origin + ray.direction * 100f - bulletSpawn.position).normalized;
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            // Oblicz docelow¹ pozycjê pocisku
            Vector3 targetPosition = bulletSpawn.position + directionToMouse * 100f;

            // Przemieszczaj pocisk stopniowo w kierunku celu
            StartCoroutine(MoveBulletToPosition(bulletRigidbody, targetPosition, bulletSpeed));
        }

        audioSource.PlayOneShot(shootSound);

        bulletsLeft1--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft1 > 0)
            Invoke("Shoot", timeBetweenShots);
        if (bulletsLeft1 == 0)
        {
            Reload();
        }
    }

    IEnumerator MoveBulletToPosition(Rigidbody bulletRigidbody, Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(bulletRigidbody.position, targetPosition) > 0.1f)
        {
            // Oblicz kierunek do celu
            Vector3 direction = (targetPosition - bulletRigidbody.position).normalized;

            // Przemieszczaj pocisk w kierunku celu
            bulletRigidbody.MovePosition(bulletRigidbody.position + direction * speed * Time.deltaTime);

            yield return null;
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
        bulletsLeft1 = magazineSize;
        reloading = false;
    }
}
