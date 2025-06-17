using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int currentWave = 1;
    public int maxWave = 10;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AdvanceWave()
    {
        if (currentWave < maxWave)
        {
            currentWave++;
        }

        Debug.Log("Wave: " + currentWave);
    }

    public bool IsBossWave()
    {
        return currentWave == maxWave;
    }

    public int GetEnemyHealthForWave()
    {
        if (IsBossWave())
            return 50; // Boss HP
        else
            return 5 + (currentWave * 2); // Regular slime scaling
    }
}
