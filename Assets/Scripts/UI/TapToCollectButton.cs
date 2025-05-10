using UnityEngine;

public class TapToCollectButton : MonoBehaviour
{
    public void OnTap()
    {
        CoinManager.Instance.AddCoins(CoinManager.Instance.TapMultiplier);
    }
}
