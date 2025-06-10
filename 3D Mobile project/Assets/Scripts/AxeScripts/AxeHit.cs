using UnityEngine;

public class AxeHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit detected with: " + other.name);

        if (other.CompareTag("Enemy"))
        {
            // Look upward to get the main slime object (Slime_Green)
            EnemyHealth health = other.GetComponentInParent<EnemyHealth>();
            if (health != null)
            {
                health.TriggerDeath();
            }
            else
            {
                Debug.LogWarning("No EnemyHealth script found in parents of: " + other.name);
            }
        }
    }
}
