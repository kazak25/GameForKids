using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Coins", menuName = "Coins")]
public class CoinsConfigCollection : ScriptableObject
{
   public ConfigCoinsModel[] ConfigCoinsModels => _coinsConfigCollection;
   
   [SerializeField] private ConfigCoinsModel[] _coinsConfigCollection;
}
