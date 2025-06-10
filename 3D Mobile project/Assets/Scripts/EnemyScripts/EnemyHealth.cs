using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    private EnemySpawner spawner;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSpawner(EnemySpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    public void TriggerDeath()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(1.2f); // Match animation length
        spawner.NotifyEnemyDied();
        Destroy(gameObject); // Destroy this slime
    }
}
