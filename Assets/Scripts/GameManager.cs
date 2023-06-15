using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ConfigSystem _configSystem;
    [SerializeField] private CoinsSystem _coinsSystem;
    private void Awake()
    {
        StartGame();
    }

    private void StartGame()
    {
        CoinModel[] coinModels;

        _configSystem.CreateCoinsModels();
        coinModels = _configSystem.GetCoinssModels();
        Debug.Log(coinModels.Length);

        _coinsSystem.SpawnCoins(coinModels);
    }
}