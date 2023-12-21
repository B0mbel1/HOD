
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

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
    //public Transform attackPoint;
    //public RaycastHit rayHit;
    //public LayerMask whatIsEnemy;

    //Graphics
    //public GameObject muzzleFlash, bulletHoleGraphic;
    //public CamShake camShake;
    //public float camShakeMagnitude, camShakeDuration;
    //public TextMeshProUGUI text;

    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        MyInput();

        //SetText
        //text.SetText(bulletsLeft + " / " + magazineSize);
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
    private void Shoot()
    {
        readyToShoot = false;

    //Spread
    //float x = Random.Range(-spread, spread);
    //float y = Random.Range(-spread, spread);

    //Calculate Direction with Spread
    //Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

    //RayCast
    
            RaycastHit co;
            if (Physics.Raycast(fpsCam.transform.position, transform.forward, out co, Mathf.Infinity))
            {
                obrazeniaOK czyOK = co.collider.GetComponent<obrazeniaOK>();
                if (czyOK != null)
                {
                    czyOK.zadajObrazenia(damage);
                }
            }
                GameObject bullet = Instantiate(bulletPrefab);
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());
                bullet.transform.position = bulletSpawn.position;
                Vector3 rotation = bullet.transform.rotation.eulerAngles;
                bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
                bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);


                audioSource.PlayOneShot(shootSound);
            
    

        //ShakeCamera
        //camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);

        if(bulletsLeft == 0)
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
