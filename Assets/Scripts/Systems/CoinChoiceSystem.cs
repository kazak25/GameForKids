using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinChoiceSystem : MonoBehaviour
{
    [SerializeField] private Image _prefab;
    [SerializeField] private Canvas _canvas;

    private Queue<int> _coinsSequence = new Queue<int>();
    private List<CoinController> _coinControllers = new List<CoinController>();
    private List<Image> _coinsForChoice = new List<Image>();
    private PigController _pigController;
    private CompositeDisposable _subscription;
    private int _countChoicesCoins;
    private bool _canRaycast = false;

    private void Awake()
    {
        _subscription = new CompositeDisposable()
        {
            EventStream.Game.Subscribe<GetLastCoinEvent>(PlaceCoins),
            EventStream.Game.Subscribe<GetLastCoinEvent>(CanRaycast),
            EventStream.Game.Subscribe<GetSequenceCoinsEvent>(Initialize)
        };
    }

    public void AddCoinControllers(CoinController coinController)
    {
        _coinControllers.Add(coinController);
    }

    private void CanRaycast(GetLastCoinEvent data)
    {
        _canRaycast = true;
    }

    private void PlaceCoins(GetLastCoinEvent data)
    {
        while (_coinControllers.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, _coinControllers.Count);
            _prefab.sprite = _coinControllers[randomIndex].GetCoinImage();
            _coinControllers.RemoveAt(randomIndex);

            var coinForChoice = Instantiate(_prefab);
            _coinsForChoice.Add(coinForChoice);
            coinForChoice.name = _prefab.sprite.name;
            coinForChoice.transform.SetParent(_canvas.transform);
        }
    }

    private void Initialize(GetSequenceCoinsEvent data)
    {
        _pigController = data.PigController;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("CoinForChoice"))
            {
                var coin = hit.collider.gameObject;
                var value = coin.name;
                int intValue = int.Parse(value);

                _coinsSequence.Enqueue(intValue);
                Destroy(coin);
                _countChoicesCoins++;

                if (_countChoicesCoins == _coinsForChoice.Count)
                {
                    CompareQueues();
                }
            }
        }
    }

    bool CompareQueues()
    {
        var arr1 = _coinsSequence.ToArray();
        var arr2 = _pigController.GetCoinsSequence().ToArray();

        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
            {
                var eventDataRequest = new GetLoseEvent();
                EventStream.Game.Publish(eventDataRequest);
                return false;
            }
        }

        var eventDataRequest2 = new GetWinEvent();
        EventStream.Game.Publish(eventDataRequest2);
        return true;
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}