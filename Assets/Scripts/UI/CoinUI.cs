using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void Start()
    {
        CoinManager.Instance.OnCoinChanged += UpdateText;
        UpdateText(CoinManager.Instance.Coins);
    }

    private void UpdateText(int coins)
    {
        coinText.text = $"Coins: {coins}";
    }

    private void OnDestroy()
    {
        CoinManager.Instance.OnCoinChanged -= UpdateText;
    }
}
