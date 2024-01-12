using UnityEngine;

public class SniperTower : MonoBehaviour
{
    public float fireRate;
    public float shootingRange;
    public int bulletDamage = 1;
    public GameObject bulletPrefab;
    public Transform shootingPosition;

    private float nextFire;

    void Update()
    {
        if (Time.time > nextFire)
        {
            Transform closestEnemy = FindClosestEnemy();

            if (closestEnemy != null)
            {
                Fire(closestEnemy);
                nextFire = Time.time + fireRate;
            }
        }
    }

    void Fire(Transform target)
    {
        if (GetComponent<Tower>().Placed && shootingPosition != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, target.position);

            if (distanceToEnemy <= shootingRange)
            {
                GameObject bulletInstance = Instantiate(bulletPrefab, shootingPosition.position, Quaternion.identity);

                SniperBullet sniperBullet = bulletInstance.GetComponent<SniperBullet>();
                sniperBullet.damage = bulletDamage;
                sniperBullet.EnemyPosition = target;
            }
        }
    }

    Transform FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;

            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }

        if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.transform.position) <= shootingRange)
        {
            Debug.DrawLine(transform.position, closestEnemy.transform.position);
            return closestEnemy.transform;
        }

        return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CamouflagedZombie"))
        {
            // Only damage the Camouflaged Zombie if it is in the layer "CamouflagedZombie"
            other.GetComponent<CamouflagedZombie>().TakeDamage(bulletDamage);
        }
    }
}
