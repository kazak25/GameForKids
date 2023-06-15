using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigCoinsModel
{
   public int Value => _value;
   public Sprite Coin => _coin;
   
   [SerializeField] private int _value;
   [SerializeField] private Sprite _coin;
}
