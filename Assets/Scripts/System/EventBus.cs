using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBus
{
    public static Action OnNewWave;

    public static Action<float> onAttackCastle;

    public static Action onGameOver;

    public static Action<float> onLootMoney;

    public static Action<float> onSpendMoney;

    public static Action<float> onUpgradeMaxHp;
}
