using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigModel : MonoBehaviour
{
    public Queue<int> Coins => _coins;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Queue<int> _coins = new Queue<int>();

    public void AddNumber(int number)
    {
        _coins.Enqueue(number);
    }
}