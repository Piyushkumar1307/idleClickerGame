using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI autoCollectorText;
    public TextMeshProUGUI tapMultiplierText;
    public TextMeshProUGUI offlineCollectorText;

    public float autoCollectorRate = 0f;
    public float tapMultiplier = 1f;
    public float offlineCollectorRate = 0f;

    public UpgradeDefinition autoCollectorUpgrade;
    public UpgradeDefinition tapMultiplierUpgrade;
    public UpgradeDefinition offlineCollectorUpgrade;

    public Button autoCollectorButton;
    public Button tapMultiplierButton;
    public Button offlineCollectorButton;

    private void Start()
    {
        CoinManager.Instance.OnCoinChanged += UpdateCoinDisplay;

        autoCollectorButton.onClick.AddListener(UpgradeAutoCollector);
        tapMultiplierButton.onClick.AddListener(UpgradeTapMultiplier);
        offlineCollectorButton.onClick.AddListener(UpgradeOfflineCollector);

        autoCollectorUpgrade?.SetLevel(0);
        tapMultiplierUpgrade?.SetLevel(0);
        offlineCollectorUpgrade?.SetLevel(0);

        UpdateCoinDisplay(CoinManager.Instance.Coins);
        UpdateUpgradeTexts();
    }

    private void Update()
    {
        if (autoCollectorRate > 0f)
            CoinManager.Instance.AddCoins(Mathf.FloorToInt(autoCollectorRate * Time.deltaTime));

        if (offlineCollectorRate > 0f)
            CoinManager.Instance.AddCoins(Mathf.FloorToInt(offlineCollectorRate * Time.deltaTime));
    }

    void UpdateCoinDisplay(int coins)
    {
        coinText.text = "Coins: " + coins.ToString();
    }

    void UpdateUpgradeTexts()
    {
        autoCollectorText.text = "Auto Collector Level: " + autoCollectorUpgrade.CurrentLevel;
        tapMultiplierText.text = "Tap Multiplier Level: " + tapMultiplierUpgrade.CurrentLevel;
        offlineCollectorText.text = "Offline Collector Level: " + offlineCollectorUpgrade.CurrentLevel;
    }

    void UpgradeAutoCollector()
    {
        autoCollectorUpgrade.ApplyUpgrade(this);
        UpdateUpgradeTexts();
    }

    void UpgradeTapMultiplier()
    {
        tapMultiplierUpgrade.ApplyUpgrade(this);
        UpdateUpgradeTexts();
    }

    void UpgradeOfflineCollector()
    {
        offlineCollectorUpgrade.ApplyUpgrade(this);
        UpdateUpgradeTexts();
    }

    public void CollectCoins()
    {
        int amount = Mathf.FloorToInt(tapMultiplier);
        CoinManager.Instance.AddCoins(amount);
    }
}
