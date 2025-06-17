using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false;

    private Animator animator;
    private EnemySpawner spawner;

    [Header("Rewards")]
    public int goldReward = 5;
    public bool isBoss = false;

    [Header("Health Bar")]
    public GameObject healthBarPrefab;
    private HealthBar healthBarUI;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;

        // Setup health bar using this transform (no anchor)
        if (healthBarPrefab != null)
        {
            GameObject bar = Instantiate(healthBarPrefab);
            healthBarUI = bar.GetComponent<HealthBar>();
            healthBarUI.Initialize(transform);
            healthBarUI.UpdateHealthBar(1f);
        }
    }

    public void SetSpawner(EnemySpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        float percent = Mathf.Clamp01((float)currentHealth / maxHealth);
        healthBarUI?.UpdateHealthBar(percent);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Damage");
        }
        else
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(1.2f);

        PlayerStats.Instance.GainGold(goldReward);

        if (spawner != null)
            spawner.NotifyEnemyDied();

        if (healthBarUI != null)
            Destroy(healthBarUI.gameObject);

        Destroy(gameObject);
    }
}
