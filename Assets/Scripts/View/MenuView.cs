using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _loseMenu;

    private Button _restartButton;
    private CompositeDisposable _subscription;

    private void Awake()
    {
        _subscription = new CompositeDisposable()
        {
            EventStream.Game.Subscribe<GetWinEvent>(WinMenuOn),
            EventStream.Game.Subscribe<GetLoseEvent>(LoseMenuOn),
        };
    }

    [UsedImplicitly]
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
        _loseMenu.SetActive(false);
        _winMenu.SetActive(false);
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3f);
        _winMenu.SetActive(true);
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(3f);
        _loseMenu.SetActive(true);
    }

    private void WinMenuOn(GetWinEvent data)
    {
        StartCoroutine(Win());
    }

    private void LoseMenuOn(GetLoseEvent data)
    {
        StartCoroutine(Lose());
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}