using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _damageText;
    private string _damageTextStart;
    [SerializeField] private TextMeshProUGUI _damageButtonText;

    [SerializeField] private TextMeshProUGUI _attackSpeedText;
    private string __attackSpeedTextStart;
    [SerializeField] private TextMeshProUGUI _attackSpeedButtonText;

    [SerializeField] private TextMeshProUGUI _maxHpText;
    private string _maxHpTextStart;
    [SerializeField] private TextMeshProUGUI _maxHpButtonText;

    [SerializeField] private TextMeshProUGUI _regenText;
    private string _regenTexStart;
    [SerializeField] private TextMeshProUGUI _regenButtonText;

    [SerializeField] private TextMeshProUGUI _moneyCoefText;
    private string _moneyCoeftStart;
    [SerializeField] private TextMeshProUGUI _moneyCoefButtonText;

    [SerializeField] private TextMeshProUGUI _multishotText;
    private string _multishotStart;
    [SerializeField] private TextMeshProUGUI _multishotButtonText;

    private void Start()
    {
        _damageTextStart = _damageText.text;
        __attackSpeedTextStart = _attackSpeedText.text;
        _maxHpTextStart = _maxHpText.text;
        _regenTexStart = _regenText.text;
        _moneyCoeftStart = _moneyCoefText.text;
        _multishotStart = _multishotText.text;
        UpdateUpgradesText();
    }

    private void UpdateUpgradesText()
    {
        _damageText.text = _damageTextStart + ": " + GameStats.instance.damage;
        _attackSpeedText.text = __attackSpeedTextStart + ": " + GameStats.instance.atackSpeed;
        _maxHpText.text = _maxHpTextStart + ": " + GameStats.instance.maxHp;
        _regenText.text = _regenTexStart + ": " + GameStats.instance.hpRegen;
        _moneyCoefText.text = _moneyCoeftStart + ": x" + GameStats.instance.moneyCoef;
        _multishotText.text = _multishotStart + ": " + GameStats.instance.multishotCount;

        _damageButtonText.text = Math.Round(GameStats.instance.damageCost) + "$";
        _attackSpeedButtonText.text = Math.Round(GameStats.instance.atackSpeedCost) + "$";
        _maxHpButtonText.text = Math.Round(GameStats.instance.maxHpCost) + "$";
        _regenButtonText.text = Math.Round(GameStats.instance.hpRegenCost) + "$";
        _moneyCoefButtonText.text = Math.Round(GameStats.instance.moneyCoefCost) + "$";
        _multishotButtonText.text = Math.Round(GameStats.instance.multishotCountCost) + "$";
    }

    public void tryBuyDamageUpgrade()
    {
        if(GameStats.instance.money >= GameStats.instance.damageCost)
        {
            EventBus.onSpendMoney?.Invoke(GameStats.instance.damageCost);
            GameStats.instance.damage += 1;
            GameStats.instance.damageCost *= 1.5f;
            UpdateUpgradesText();

        }
    }
    public void tryBuyAttackSpeedUpgrade()
    {
        if (GameStats.instance.money >= GameStats.instance.atackSpeedCost)
        {
            EventBus.onSpendMoney?.Invoke(GameStats.instance.atackSpeedCost);
            GameStats.instance.atackSpeed += 0.5f;
            GameStats.instance.atackSpeedCost *= 1.5f;
            UpdateUpgradesText();

        }
    }
    public void tryBuyMaxHpUpgrade()
    {
        if (GameStats.instance.money >= GameStats.instance.maxHpCost)
        {
            EventBus.onSpendMoney?.Invoke(GameStats.instance.maxHpCost);
            GameStats.instance.maxHp += 10;
            GameStats.instance.maxHpCost *= 1.5f;
            EventBus.onUpgradeMaxHp?.Invoke(10);
            UpdateUpgradesText();

        }
    }
    public void tryBuyRegenUpgrade()
    {
        if (GameStats.instance.money >= GameStats.instance.hpRegenCost)
        {
            EventBus.onSpendMoney?.Invoke(GameStats.instance.hpRegenCost);
            GameStats.instance.hpRegen += 1;
            GameStats.instance.hpRegenCost *= 1.5f;
            UpdateUpgradesText();

        }
    }
    public void tryBuyMoneyCoefUpgrade()
    {
        if (GameStats.instance.money >= GameStats.instance.moneyCoefCost)
        {
            EventBus.onSpendMoney?.Invoke(GameStats.instance.moneyCoefCost);
            GameStats.instance.moneyCoef += 0.1f;
            GameStats.instance.moneyCoefCost *= 1.5f;
            UpdateUpgradesText();

        }
    }
    public void tryBuyMultishotUpgrade()
    {
        if (GameStats.instance.money >= GameStats.instance.multishotCountCost)
        {
            EventBus.onSpendMoney?.Invoke(GameStats.instance.multishotCountCost);
            GameStats.instance.multishotCount += 1;
            GameStats.instance.multishotCountCost *= 1.5f;
            UpdateUpgradesText();

        }
    }
}
