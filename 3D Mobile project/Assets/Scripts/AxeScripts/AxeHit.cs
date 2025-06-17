using UnityEngine;

public class AxeHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth health = other.GetComponentInParent<EnemyHealth>();
            if (health != null)
            {
                int damage = PlayerStats.Instance.weaponDamage;
                health.TakeDamage(damage);
            }
        }
    }
}
