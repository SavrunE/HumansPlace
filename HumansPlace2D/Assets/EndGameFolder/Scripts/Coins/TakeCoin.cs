using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeCoin : MonoBehaviour
{
    [SerializeField] Text coinText;
    void Start() => Player.Singleton.CoinTake += OnPlayerCoinsChanged;
    public void OnPlayerCoinsChanged(int changedCoins) => coinText.text = changedCoins.ToString();
}
