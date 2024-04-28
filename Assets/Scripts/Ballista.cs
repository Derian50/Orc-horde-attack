using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ballista : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;
    [SerializeField] TextMeshProUGUI _textHp;

    private float _health;

    private void Start()
    {
        _health = GameStats.instance.maxHp;

        _textHp.text = _health + "/" + GameStats.instance.maxHp;
        _healthSlider.value = _health / GameStats.instance.maxHp;

        StartCoroutine(Regeneration());
    }

    IEnumerator Regeneration()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            _health += GameStats.instance.hpRegen / 10f;
            if (_health > GameStats.instance.maxHp) _health = GameStats.instance.maxHp;
        }
    }

    private void OnEnable()
    {
        EventBus.onAttackCastle += TakeDamage;
    }
    private void OnDisable()
    {
        EventBus.onAttackCastle -= TakeDamage;
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            _health = 0;
            Time.timeScale = 0f;
            EventBus.onGameOver?.Invoke();
        }
        _textHp.text = Math.Round(_health) + "/" + Math.Round(GameStats.instance.maxHp);
        _healthSlider.value = _health / GameStats.instance.maxHp;
    }

}
