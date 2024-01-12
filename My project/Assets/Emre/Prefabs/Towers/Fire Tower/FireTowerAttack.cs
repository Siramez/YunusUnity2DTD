using System.Collections;
using UnityEngine;

public class FireTowerAttack : MonoBehaviour
{
    float nextFire;
    public float fireRate;
    public int bulletCount; // Number of bullets fired in a single volley
    public float bulletInterval; // Time interval between each bullet in a volley
    public GameObject bullet;
    public float Distance;
    public Transform closestEnemyPostion;
    public Transform ShootingPosition;

    public int bulletDamage = 1;

    // Define a layer mask that includes all layers except "CamouflagedZombie"
    public LayerMask targetLayers;

    void Update()
    {
        FindClosestEnemy();
        if (GetComponent<Tower>().Placed && closestEnemyPostion != null)
            fire();
    }

    void gun_face_enemy()
    {
        if (closestEnemyPostion != null)
        {
            Vector3 EnemyPosition = closestEnemyPostion.position;
            Vector2 directionToLookAt = new Vector2(
                EnemyPosition.x - transform.position.x,
                EnemyPosition.y - transform.position.y
            );
            transform.up = directionToLookAt;
        }
    }

    void fire()
    {
        if (Time.time > nextFire && closestEnemyPostion != null)
        {
            StartCoroutine(FireBullets());
            nextFire = Time.time + fireRate;
        }
    }

    IEnumerator FireBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject EvolutionBullett = Instantiate(bullet, ShootingPosition.position, Quaternion.identity);

            EvolutionBullett.GetComponent<FireBullet>().damage = bulletDamage;
            EvolutionBullett.GetComponent<FireBullet>().EnemyPosition = closestEnemyPostion;

            yield return new WaitForSeconds(bulletInterval);
        }
    }

    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Transform closestEnemyTransform = null;

        // Find all GameObjects with the tag "enemy"
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject currentEnemyObject in allEnemies)
        {
            // Check if the enemy's layer is included in the target layers
            if (((1 << currentEnemyObject.layer) & targetLayers) != 0)
            {
                float distanceToEnemy = (currentEnemyObject.transform.position - transform.position).sqrMagnitude;

                if (distanceToEnemy < distanceToClosestEnemy && distanceToEnemy < Distance * 10)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemyTransform = currentEnemyObject.transform;
                }
            }
        }

        closestEnemyPostion = closestEnemyTransform;

        if (closestEnemyTransform != null)
        {
            Debug.DrawLine(transform.position, closestEnemyTransform.position);
        }
    }
}
