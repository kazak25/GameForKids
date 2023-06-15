using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float Speed = 5f;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Transform _targert;
    private bool _isDropped;
    private bool _canFall;
    private CoinModel _coinModel;

    public void Initialize(CoinModel model, Transform target)
    {
        _coinModel = model;
        _targert = target;
    }

    public void GetSprite(Sprite coin)
    {
        _spriteRenderer.sprite = coin;
    }

    public Sprite GetCoinImage()
    {
        return _spriteRenderer.sprite;
    }

    public int GetValue()
    {
        return _coinModel.Value;
    }

    public void CanFall()
    {
        _canFall = true;
    }

    private void Start()
    {
        _rigidbody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (_canFall == false)
        {
            return;
        }

        if (!_isDropped)
        {
            _rigidbody.velocity = (_targert.position - transform.position).normalized * Speed;

            if (transform.position == _targert.position)
            {
                _rigidbody.velocity = Vector3.zero;
                _isDropped = true;
                _rigidbody.isKinematic = true;
            }
        }
    }
}