using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class shootingfinal : NetworkBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public static int bulletsLeft1;
    int bulletsShot;
    public float bulletSpeed;

    //bools 
    bool shooting, readyToShoot, reloading;
    public bool allowButtonHold;

    public Camera fpsCam;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;


    public NetworkObject bulletPrefab;
    public  Transform bulletSpawn;

    void Start()
    {
        bulletsLeft1 = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!IsOwner) { return; }

        //if (IsHost)
        //{
        //    MyInput();
        //}
        //if (IsClient && !IsHost)
        //{
        //    MyInput();
        //}
        if (IsHost)
        {
            MyInput();
        }
        if(IsClient && !IsHost)
        {
            MyInput();
        }
        

    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    [ServerRpc(RequireOwnership =false)]
    private void BulletSpawnServerRpc(Vector3 vector)
    {
        ////obiekt = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        //Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        //StartCoroutine(MoveBulletToPosition(bulletRigidbody, target, bulletSpeed));

        NetworkObject bulletInstance = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();
        StartCoroutine(MoveBulletToPosition(bulletRigidbody, vector, bulletSpeed));
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

                //Strzelanie
                if (readyToShoot && shooting && !reloading && bulletsLeft1 > 0)
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
        //NetworkObject bullet = NetworkManager.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //var bullet = Instantiate(bulletPrefab);
        Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // SprawdŸ, czy raycast trafi³ w coœ
        if (Physics.Raycast(ray, out hit))
        {
            //    Vector3 directionToMouse = (ray.origin + ray.direction * 100f - bulletSpawn.position).normalized;
            //    // Oblicz kierunek do miejsca trafienia
            //   Vector3 targetPosition = bulletSpawn.position + directionToMouse * 100f;
            //Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 targetPosition = hit.point;
            Vector3 newSpawn = bulletSpawn.position;

            //    // Przemieszczaj pocisk stopniowo w kierunku celu
            //    StartCoroutine(MoveBulletToPosition(bulletRigidbody, targetPosition, bulletSpeed));
            BulletSpawnServerRpc(targetPosition);
        }
        else
        {
            // Jeœli raycast nie trafi³ w nic, pocisk bêdzie lecia³ w kierunku kursora myszy
            //Vector3 directionToMouse = (ray.origin + ray.direction * 100f - bulletSpawn.position).normalized;
            //Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            //// Oblicz docelow¹ pozycjê pocisku
            //Vector3 targetPosition = bulletSpawn.position + directionToMouse * 100f;
           // Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 targetPosition = hit.point;
            //// Przemieszczaj pocisk stopniowo w kierunku celu
            //StartCoroutine(MoveBulletToPosition(bulletRigidbody, targetPosition, bulletSpeed));
            BulletSpawnServerRpc(targetPosition);
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
