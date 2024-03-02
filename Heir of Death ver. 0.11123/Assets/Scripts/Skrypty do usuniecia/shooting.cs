using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn; // Punktu startowy dla pocisku

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // SprawdŸ czy zosta³ wciœniêty lewy przycisk myszy
        {
            Shoot();
        }
    }

    void Shoot()
    {
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
        }
        else
        {
            // Jeœli raycast nie trafi³ w nic, pocisk bêdzie lecia³ w kierunku kursora myszy
            Vector3 directionToMouse = (ray.origin + ray.direction * 100f - bulletSpawn.position).normalized;
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = directionToMouse * bulletSpeed;
        }
    }
}
