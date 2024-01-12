using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionTime = 3.0f;
    public float explosionRadius = 5.0f;
    public int explosionDamage = 10;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(explosionTime);
        Explode();
    }

    void Explode()
    {
        // Find all colliders in the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Check if the collider has the IDamageable interface
                IDamageable damageable = collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    // Damage enemies within the explosion radius using IDamageable interface
                    damageable.TakeDamage(explosionDamage);
                }
            }
        }

        // Destroy the bomb
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        // Draw a wire sphere to visualize the explosion radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
