using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Player Stats")]
    public int gold = 0;
    public int weaponDamage = 1;

    [Header("Upgrade System")]
    public int upgradeCost = 10;
    public int damageIncrease = 1;
    public int costIncrease = 10;

    [Header("UI References")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI upgradeCostText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void GainGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    public void UpgradeDamage()
    {
        if (gold >= upgradeCost)
        {
            gold -= upgradeCost;
            weaponDamage += damageIncrease;
            upgradeCost += costIncrease;

            Debug.Log($"🗡️ Upgraded! Damage: {weaponDamage}, Gold: {gold}, Next Cost: {upgradeCost}");
            UpdateUI();
        }
        else
        {
            Debug.Log("❌ Not enough gold to upgrade!");
        }
    }

    private void UpdateUI()
    {
        if (goldText) goldText.text = "Gold: " + gold;
        if (damageText) damageText.text = "Damage: " + weaponDamage;
        if (upgradeCostText) upgradeCostText.text = "Upgrade Cost: " + upgradeCost;
    }
}
