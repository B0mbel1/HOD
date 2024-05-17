using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class shootingfinal_2 : NetworkBehaviour
{
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots, bulletSpeed;
    public static int bulletsLeft2;
    public int magazineSize, bulletsPerTap, damage;
    int bulletsShot;
    public bool allowButtonHold;
    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;
    public GameObject pozycjaAS;
    public NetworkObject bulletPrefab;
    public Transform bulletSpawn;

    void Start()
    {
        bulletsLeft2 = magazineSize;
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

    private void Metoda(AudioSource audioSource)
    {
        audioSource.gameObject.transform.position = pozycjaAS.transform.position;
        if (IsHost)
        {
            AudioSource.PlayClipAtPoint(shootSound, audioSource.gameObject.transform.position);
        }
        if (!IsHost && IsClient)
        {
            AudioSource.PlayClipAtPoint(shootSound, audioSource.gameObject.transform.position);
        }
    }

    [ClientRpc]
    public void ClientMetodaClientRpc(ClientRpcParams clientRpcParams)
    {
        Metoda(audioSource);
    }

    [ServerRpc]
    public void HostMetodaServerRpc()
    {
        Metoda(audioSource);
    }

    [ServerRpc]
    public void SynchroServerRpc(ulong player)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { player }
            }
        };
        ClientMetodaClientRpc(clientRpcParams);
    }

    private void MyInput()
    {
        if (!MenuPauzy.GameIsPaused)
        {
            if (!FreeTeleportation.teleportState)
            {
                if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
                else shooting = Input.GetKeyDown(KeyCode.Mouse0);

                if (Input.GetKeyDown(KeyCode.R) && bulletsLeft2 < magazineSize && !reloading) Reload();

                if (readyToShoot && shooting && !reloading && bulletsLeft2 > 0)
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

                HostMetodaServerRpc();
                SynchroServerRpc(1);
            }
            if (!IsHost && IsClient)
            {
                BulletSpawnServerRpc(targetPosition);
                SynchroServerRpc(targetPosition);

                HostMetodaServerRpc();
                SynchroServerRpc(0);
            }
        }
        else
        {
            Vector3 targetPosition = hit.point;

            if (IsHost)
            {
                BulletSpawnServerRpc(targetPosition);
                BulletSpawnClientRpc(targetPosition, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });


                HostMetodaServerRpc();
                SynchroServerRpc(1);
            }

            if (!IsHost && IsClient)
            {
                BulletSpawnServerRpc(targetPosition);
                SynchroServerRpc(targetPosition);

                HostMetodaServerRpc();
                SynchroServerRpc(0);
            }
        }

        audioSource.PlayOneShot(shootSound);

        bulletsLeft2--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft2 > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        if (bulletsLeft2 == 0)
        {
            Reload();
        }
    }

    IEnumerator MoveBulletToPosition(Rigidbody bulletRigidbody, Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(bulletRigidbody.position, targetPosition) > 0.1f)
        {
            Vector3 direction = (targetPosition - bulletRigidbody.position).normalized;
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
        bulletsLeft2 = magazineSize;
        reloading = false;
    }
}
