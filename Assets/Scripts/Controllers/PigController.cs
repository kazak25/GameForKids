using System;
using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class PigController : MonoBehaviour
{
    [SerializeField] private PigModel _pigModel;
    [SerializeField] private CoinChoiceSystem _coinChoiceSystem;

    private CompositeDisposable _subscription;

    private void Start()
    {
        var eventDataRequest = new GetSequenceCoinsEvent(this);
        EventStream.Game.Publish(eventDataRequest);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var coin = collision.gameObject;
        if (coin.CompareTag("Coin"))
        {
            var coinController = coin.GetComponent<CoinController>();

            var coinValue = coinController.GetValue();
            _pigModel.AddNumber(coinValue);
            _coinChoiceSystem.AddCoinControllers(coinController);
        }
    }

    public Queue<int> GetCoinsSequence()
    {
        return _pigModel.Coins;
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}