using Unity.Netcode;
using UnityEngine;

public class GunSystemOnline : NetworkBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public static int bulletsLeft;
    int bulletsShot;
    public GameObject Pointer;
    bool shooting, reloading;
    bool readyToShoot;
    public Camera fpsCam;
    public static bool Teleblock;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;
    public GameObject Gun;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed;

    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();
        Teleblock = false;
    }

    private void Update()
    {
        if (!IsOwner) { return; }

        
            MyInput();
        
            
        
            
    }

    private void MyInput()
    {
        if (!MenuPauzy.GameIsPaused)
        {

            
                if (allowButtonHold)
                {
                    shooting = Input.GetKey(KeyCode.Mouse0);
                }

                if (!allowButtonHold)
                {
                    shooting = Input.GetKeyDown(KeyCode.Mouse0);
                }

                if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
                {
                    Reload();
                }

                if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
                {
                    bulletsShot = bulletsPerTap;
                    Shoot();
                }

            

        }
    }

    private void Shoot()
    {
        
            readyToShoot = false;

            RaycastHit co;
            if (Physics.Raycast(fpsCam.transform.position, transform.forward, out co, Mathf.Infinity))
            {
                //obrazeniaOK czyOK = co.collider.GetComponent<obrazeniaOK>();
                //if (czyOK != null)
                //{
                //    if (BetterAmmoScript.isBetterAmmoActivated == true)
                //    {
                //        czyOK.zadajObrazenia(damage + 3);
                //    }
                //    else
                //    {
                //        czyOK.zadajObrazenia(damage);
                //    }
                //}
            }

            GameObject bullet = Instantiate(bulletPrefab);
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());
            bullet.transform.position = bulletSpawn.position;
            Vector3 rotation = bullet.transform.rotation.eulerAngles;
            bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

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
