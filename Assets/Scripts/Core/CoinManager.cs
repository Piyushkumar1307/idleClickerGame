using UnityEngine;
using System.IO;
using System;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    private const string coinDataFile = "coinData.json";

    public int Coins { get; private set; }
    public int TapMultiplier = 1;

    public event Action<int> OnCoinChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCoins();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        SaveAndNotify();
    }

    public void DeductCoins(int amount)
    {
        Coins = Mathf.Max(0, Coins - amount);
        SaveAndNotify();
    }

    public bool HasEnoughCoins(int amount)
    {
        return Coins >= amount;
    }

    public void ResetCoins()
    {
        Coins = 0;
        SaveAndNotify();
    }

    private void SaveAndNotify()
    {
        SaveCoins();
        OnCoinChanged?.Invoke(Coins);
    }

    private void SaveCoins()
    {
        string path = Path.Combine(Application.persistentDataPath, coinDataFile);
        CoinData data = new CoinData { coins = Coins };
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    private void LoadCoins()
    {
        string path = Path.Combine(Application.persistentDataPath, coinDataFile);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            CoinData data = JsonUtility.FromJson<CoinData>(json);
            Coins = data.coins;
        }
        else
        {
            Coins = 0;
        }
        OnCoinChanged?.Invoke(Coins);
    }

    [Serializable]
    public class CoinData
    {
        public int coins;
    }
}
