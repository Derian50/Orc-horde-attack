using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wave : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TextMeshProUGUI _waveCount;
    [SerializeField] private TextMeshProUGUI _enemyHp;
    [SerializeField] private TextMeshProUGUI _enemyDamage;

    private string _waveName;
    private string _enemyHpName;
    private string _enemyDamageName;
    void Start()
    {
        _waveName = _waveCount.text;
        _enemyHpName = _enemyHp.text;
        _enemyDamageName = _enemyDamage.text;

        _waveCount.text = _waveName + ": " + GameStats.instance.wave;
        _enemyHp.text = _enemyHpName + ": " + GameStats.instance.enemyHp;
        _enemyDamage.text = _enemyDamageName + ": " + GameStats.instance.enemyDamage;
    }

    private void OnEnable()
    {
        EventBus.OnNewWave += UpdateWaveInfo;
    }
    private void OnDisable()
    {
        EventBus.OnNewWave -= UpdateWaveInfo;
    }

    private void UpdateWaveInfo()
    {
        _waveCount.text = _waveName + ": " +  GameStats.instance.wave;
        _enemyHp.text = _enemyHpName + ": " + Math.Round(GameStats.instance.enemyHp, 2);
        _enemyDamage.text = _enemyDamageName + ": " + Math.Round(GameStats.instance.enemyDamage, 2);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
