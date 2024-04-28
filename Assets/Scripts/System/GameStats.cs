using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    public float damage = 1;
    public float atackSpeed = 1;
    public float multishotCount = 1;
    public float maxHp = 10;
    public float hpRegen = 0;
    public float moneyCoef = 1;


    public float damageCost;
    public float atackSpeedCost;
    public float multishotCountCost;
    public float maxHpCost;
    public float hpRegenCost;
    public float moneyCoefCost;

    public float money;

    public int wave = 1;
    public float enemyHp = 2.35f;
    public float enemyDamage = 1.13f;

    public static GameStats instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
