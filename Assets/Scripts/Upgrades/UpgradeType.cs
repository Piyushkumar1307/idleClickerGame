using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeDefinition", menuName = "Game/UpgradeDefinition")]
public class UpgradeDefinition : ScriptableObject
{
    public string upgradeName;
    public int baseCost = 50;
    public float costMultiplier = 1.5f;
    public float increaseRate = 1f;

    private int currentLevel = 0;
    public int CurrentLevel => currentLevel;

    public void SetLevel(int level)
    {
        currentLevel = level;
    }

    public void ApplyUpgrade(GameController gameController)
    {
        int cost = Mathf.CeilToInt(baseCost * Mathf.Pow(costMultiplier, currentLevel));

        if (CoinManager.Instance.HasEnoughCoins(cost))
        {
            CoinManager.Instance.DeductCoins(cost);
            currentLevel++;

            switch (upgradeName)
            {
                case "Auto Collector":
                    gameController.autoCollectorRate += increaseRate;
                    break;
                case "Tap Multiplier":
                    gameController.tapMultiplier += increaseRate;
                    CoinManager.Instance.TapMultiplier = Mathf.FloorToInt(gameController.tapMultiplier);
                    break;
                case "Offline Collector":
                    gameController.offlineCollectorRate += increaseRate;
                    break;
                default:
                    Debug.LogWarning("Unknown upgrade name: " + upgradeName);
                    break;
            }
        }
        else
        {
            Debug.Log($"Not enough coins for {upgradeName} upgrade. Needed: {cost}, Available: {CoinManager.Instance.Coins}");
        }
    }
}
