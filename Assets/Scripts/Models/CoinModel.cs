using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinModel : MonoBehaviour
{
    public int Value => _value;
    public Sprite Coin => _coin;

    private int _value;
    private Sprite _coin;

    public CoinModel(int value, Sprite coin)
    {
        _value = value;
        _coin = coin;
    }
}