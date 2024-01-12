using System.Collections;
using UnityEngine;

public class ElectricBullet : MonoBehaviour
{
    public float speed = 8.0f;
    public int damage = 1; // Make this public to adjust in the Inspector
    public Transform EnemyPosition;
    public bool Slow;
    Vector3 direction;

    void Start()
    {
        direction = (EnemyPosition.position - transform.position).normalized;
    }

    void Update()
    {
        if (EnemyPosition == null)
        {
            Destroy(gameObject);
            return;
        }

        if (Vector3.Distance(transform.position, EnemyPosition.position) < 0.1f)
        {
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject, 3);
        transform.position += direction * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            if (Slow)
            {
                col.gameObject.GetComponent<IDamageable>().SlowDown();
            }

            Destroy(gameObject, 0.1f);
        }
    }

}

