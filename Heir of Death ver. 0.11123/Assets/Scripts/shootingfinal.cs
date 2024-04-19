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
    public Transform bulletSpawn;

    void Start()
    {

        bulletsLeft1 = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();


    }

    void Update()
    {
        if (!IsOwner) { return; }

        if (IsHost)
        {
            MyInput();
        }
        if (!IsHost)
        {
            MyInput();
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    [ServerRpc]
    private void BulletSpawnServerRpc(Vector3 vector)
    {
        NetworkObject bulletInstance = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();
        StartCoroutine(MoveBulletToPosition(bulletRigidbody, vector, bulletSpeed));

    }

    [ClientRpc]
    private void BulletSpawnClientRpc(Vector3 vector, ClientRpcParams clientRpcParams)
    {
        NetworkObject bulletInstance = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();
        StartCoroutine(MoveBulletToPosition(bulletRigidbody, vector, bulletSpeed));

    }

    [ServerRpc]
    private void SynchroServerRpc(Vector3 vector)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { 1 }
            }
        };

        BulletSpawnClientRpc(vector, clientRpcParams);
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

        Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 newSpawn = bulletSpawn.position;

            if (IsHost)
            {
                BulletSpawnClientRpc(targetPosition, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                BulletSpawnServerRpc(targetPosition);
            }
            if (!IsHost)
            {
                BulletSpawnServerRpc(targetPosition);
                SynchroServerRpc(targetPosition);
            }
        }
        else
        {
            Vector3 targetPosition = hit.point;

            if (IsHost)
            {
                BulletSpawnServerRpc(targetPosition);
                BulletSpawnClientRpc(targetPosition, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
            }
            if (!IsHost)
            {
                BulletSpawnServerRpc(targetPosition);
                SynchroServerRpc(targetPosition);
            }
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

        while (bulletRigidbody != null && Vector3.Distance(bulletRigidbody.position, targetPosition) > 0.1f)
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
