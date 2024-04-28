using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI _text;
    private string _textStart;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textStart = _text.text;
        UpdateMoneyText();
    }
    private void OnEnable()
    {
        EventBus.onLootMoney += LootMoney;
        EventBus.onSpendMoney += SpendMoney;
    }
    private void OnDisable()
    {
        EventBus.onLootMoney -= LootMoney;
        EventBus.onSpendMoney -= SpendMoney;
    }
    
    private void UpdateMoneyText()
    {
        _text.text = _textStart + ": " + Math.Round(GameStats.instance.money) + "$";
    }
    private void LootMoney(float money)
    {
        GameStats.instance.money += money;
        UpdateMoneyText();
    }
    private void SpendMoney(float money)
    {
        GameStats.instance.money -= money;
        UpdateMoneyText();
    }

}
