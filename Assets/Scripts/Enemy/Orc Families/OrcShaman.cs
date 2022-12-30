using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcShaman : MonoBehaviour
{
    private Transform shootPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
	[SerializeField] private float radius;
    [SerializeField] private float shootTime;
    private float shootTimeCounter;
    Vector2 startPosition;

    OrcShamanStats orc;

    private void Start()
    {
        orc = GetComponent<OrcShamanStats>();
        shootPoint = transform.Find("Shoot Point");
        startPosition = transform.position;
    }


    private void Update()
    {
        shootTimeCounter -= Time.deltaTime;

        if(shootTimeCounter <= 0)
        {
            // SpawnProjectiles(numberOfProjectiles);
            Shoot();
            shootTimeCounter = shootTime;
        }
    }

    void Shoot()
    {
        GameObject BulletIns = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().velocity = new Vector2(1,1) * projectileSpeed;
        GameObject BulletIns2 = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        BulletIns2.GetComponent<Rigidbody2D>().velocity = new Vector2(-1,1) * projectileSpeed;
        GameObject BulletIns3 = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        BulletIns3.GetComponent<Rigidbody2D>().velocity = new Vector2(1,-1) * projectileSpeed;
        GameObject BulletIns4 = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        BulletIns4.GetComponent<Rigidbody2D>().velocity = new Vector2(-1,-1) * projectileSpeed;
        Destroy(BulletIns,3f);
        Destroy(BulletIns2,3f);
        Destroy(BulletIns3,3f);
        Destroy(BulletIns4,3f);
    }

    public void Respawn()
    {
        transform.position = startPosition;
        orc.OnEnable();
    }

    // void SpawnProjectiles(int numberOfProjectiles)
	// {
	// 	float angleStep = 360f / numberOfProjectiles;
	// 	float angle = 0f;

	// 	for (int i = 0; i <= numberOfProjectiles - 1; i++) {
			
	// 		float projectileDirXposition = shootPoint.position.x + Mathf.Sin ((angle * Mathf.PI) / 180) * radius;
	// 		float projectileDirYposition = shootPoint.position.y + Mathf.Cos ((angle * Mathf.PI) / 180) * radius;

	// 		Vector2 projectileVector = new Vector2 (projectileDirXposition, projectileDirYposition);
	// 		Vector2 projectileMoveDirection = (projectileVector - (Vector2)shootPoint.position).normalized * projectileSpeed;

    //         Debug.Log(projectileVector +"  "+ projectileMoveDirection);

	// 		var proj = Instantiate (projectile, shootPoint.position, Quaternion.identity);
	// 		proj.GetComponent<Rigidbody2D> ().velocity = new Vector2 (projectileMoveDirection.x, projectileMoveDirection.y);
    //         Destroy(proj,3);
	// 		angle += angleStep;
	// 	}
	// }

}
