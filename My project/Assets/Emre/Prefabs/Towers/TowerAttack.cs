using System.Collections;
using UnityEngine;

public class TowerAttack : MonoBehaviour
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

            EvolutionBullett.GetComponent<TowerBullet>().damage = bulletDamage;
            EvolutionBullett.GetComponent<TowerBullet>().EnemyPosition = closestEnemyPostion;

            yield return new WaitForSeconds(bulletInterval);
        }
    }

    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        CamouflagedZombie closestEnemy = null;
        CamouflagedZombie[] allEnemies = GameObject.FindObjectsOfType<CamouflagedZombie>();

        foreach (CamouflagedZombie currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy && distanceToEnemy < Distance * 10)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;

                closestEnemyPostion = closestEnemy.transform;
            }
        }

        if (closestEnemy != null)
        {
            Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        }
    }
}
