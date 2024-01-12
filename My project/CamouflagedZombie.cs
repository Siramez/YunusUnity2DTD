using System;
using UnityEngine;

public class CamouflagedZombie : MonoBehaviour
{
    public float detectionRadius = 5f; // Adjust the detection radius as needed
    public LayerMask sniperTowerLayer; // Set the Sniper Tower layer in the Inspector

    private bool isDetected = false;

    void Update()
    {
        if (!isDetected)
        {
            // Check for Sniper Towers in the detection radius
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, sniperTowerLayer);

            // If a Sniper Tower is detected, become visible
            if (colliders.Length > 0)
            {
                isDetected = true;
                // Implement your logic to make the zombie visible
                // For example, change sprite color or material
            }
        }

        // If the zombie is detected, it remains visible; otherwise, implement your normal behavior
        if (isDetected)
        {
            // Implement your normal behavior for the Camouflaged Zombie
            // This can include movement or other actions
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        int collidedLayer = other.gameObject.layer;

        // Check if the collided object is on the CamouflagedZombie layer
        if (collidedLayer == LayerMask.NameToLayer("CamouflagedZombie"))
        {
            // Only take damage if collided with another Camouflaged Zombie
            TakeDamage();
        }
        // Check if the collided object is on the SniperTower layer
        else if (collidedLayer == LayerMask.NameToLayer("SniperTower"))
        {
            // Damage the Camouflaged Zombie only if it's hit by the Sniper Tower
            DamageFromSniperTower();
        }
    }

    private void DamageFromSniperTower()
    {
        throw new NotImplementedException();
    }

    void TakeDamage()
    {
        // Implement your logic for taking damage from Sniper Tower
        // For example, reduce health, play an animation, etc.
    }
}
