using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour
{
    private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float whenToShoot = 3f;
    private float shootCounter;
    [SerializeField] private float shootTime = 10;
    private bool isShooted;


    private void Start()
    {
        shootPoint = transform.Find("Shoot Point");
    }


    private void Update()
    {
        shootCounter += Time.deltaTime;

        if(shootCounter >= whenToShoot && !isShooted)
        {
            Shoot();
            isShooted = true;
        }
        
        if(shootCounter >= shootTime)
        {
            shootCounter = 0;
            isShooted = false;
        }

    }

    void Shoot()
    {
        GameObject BulletIns = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * bulletSpeed, ForceMode2D.Impulse);
        Destroy(BulletIns,3f);
    }

}
