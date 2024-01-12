using UnityEngine;

public class DiggerZombie : MonoBehaviour
{
    public float disableTime = 10.0f;
    public float detectionRadius = 5.0f;

    private void Update()
    {
        // Check for Minefield Tower within the detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (var collider in colliders)
        {
            MinefieldTower minefieldTower = collider.GetComponent<MinefieldTower>();

            if (minefieldTower != null)
            {
                // Disable the Minefield Tower
                minefieldTower.DisableTower(disableTime);

                // Optionally, you can add logic to handle other behaviors when the Digger zombie is close to the tower
            }
        }
    }
}
