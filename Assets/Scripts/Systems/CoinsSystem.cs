using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Spine.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinsSystem : MonoBehaviour
{
    [SerializeField] private CoinController _businessPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _target;

    private List<CoinController> _coinControllers = new List<CoinController>();

    private void Start()
    {
        StartCoroutine(WaitTwoSeconds());
    }

    IEnumerator WaitTwoSeconds()
    {
        while (_coinControllers.Count > 1)
        {
            int randomIndex = Random.Range(0, _coinControllers.Count);

            _coinControllers[randomIndex].CanFall();
            _coinControllers.RemoveAt(randomIndex);
            yield return new WaitForSeconds(3f);
            if (_coinControllers.Count == 1)
            {
                StartCoroutine(WaitOneSeconds());
                var eventDataRequest = new GetLastCoinEvent();
                EventStream.Game.Publish(eventDataRequest);
            }
        }
    }

    IEnumerator WaitOneSeconds()
    {
        var eventDataRequest = new GetPigShakeEvent();
        EventStream.Game.Publish(eventDataRequest);
        yield return new WaitForSeconds(2f);
    }

    public void SpawnCoins(CoinModel[] coinModels)
    {
        Debug.Log("Spawn");
        foreach (var coinModel in coinModels)
        {
            var coin = Instantiate(_businessPrefab);
            coin.transform.position = _parent.transform.position;
            coin.Initialize(coinModel, _target);
            coin.GetSprite(coinModel.Coin);

            _coinControllers.Add(coin);
        }
    }
}