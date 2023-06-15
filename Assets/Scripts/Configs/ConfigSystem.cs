using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigSystem : MonoBehaviour
{
    public CoinModel[] CoinModel;
    
    [SerializeField] private CoinsConfigCollection _coinsConfigCollection;
    
    public void CreateCoinsModels()
    {
        CoinModel = new CoinModel[_coinsConfigCollection.ConfigCoinsModels.Length];

        for (var i = 0; i < _coinsConfigCollection.ConfigCoinsModels.Length; i++)
        {
            var coins = _coinsConfigCollection.ConfigCoinsModels[i];
            CoinModel[i] = new CoinModel(coins.Value, coins.Coin);
        }
    }

    public CoinModel[] GetCoinssModels()
    {
        return CoinModel;
    }
}