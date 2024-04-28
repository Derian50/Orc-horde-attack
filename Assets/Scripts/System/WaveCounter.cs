using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] private float _waveCooldown;

    [SerializeField] private float _upgradeEnemyHp;
    [SerializeField] private float _upgradeEnemyDamage;
    void Start()
    {
        StartCoroutine(newWave());
    }

    IEnumerator newWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(_waveCooldown);
            GameStats.instance.wave++;
            GameStats.instance.enemyHp *= _upgradeEnemyHp;
            GameStats.instance.enemyDamage *= _upgradeEnemyDamage;
            EventBus.OnNewWave?.Invoke();
        }
    }
}
