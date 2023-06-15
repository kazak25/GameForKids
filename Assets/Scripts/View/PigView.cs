using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Events;
using SimpleEventBus.Disposables;


public class PigView : MonoBehaviour
{
    private CompositeDisposable _subscription;

    private void Awake()
    {
        _subscription = new CompositeDisposable()
        {
            EventStream.Game.Subscribe<GetPigShakeEvent>(PigShake),
        };
    }

    private void PigShake(GetPigShakeEvent data)
    {
        transform.DOShakePosition(2, 0.4f);
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}