using System;
using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using Spine.Unity;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _animation;

    private CompositeDisposable _subscription;

    private void Awake()
    {
        _subscription = new CompositeDisposable()
        {
            EventStream.Game.Subscribe<GetWinEvent>(Win),
            EventStream.Game.Subscribe<GetLoseEvent>(Lose),
        };
    }

    private void Win(GetWinEvent data)
    {
        _animation.AnimationName = "win";
    }

    private void Lose(GetLoseEvent data)
    {
        _animation.AnimationName = "wrong";
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}