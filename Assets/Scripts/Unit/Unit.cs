using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UnitState
{
    Run,
    Attack,
    Die
}
public class Unit : MonoBehaviour
{
    [SerializeField] private float _unitSpeed;
    [SerializeField] private bool _isHorse;
    [SerializeField] private float _attackCooldown;
    private float _attackCountdown;

    private UnitState _state;
    private float _unitHp;
    private float _unitMaxHp;
    private float _unitDamage;


    [SerializeField] private Slider _sliderHp;
    [SerializeField] private TextMeshProUGUI _moneyLoot;
    private SPUM_Prefabs _animation;
    private Rigidbody2D _rb;

    private Action _returnObject;

    [SerializeField] private float displayTime = 3f;
    [SerializeField] private float fadeSpeed = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animation = GetComponent<SPUM_Prefabs>();
        _moneyLoot.gameObject.SetActive(false);
        //_attackCooldown *= 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (_state)
        {
            case UnitState.Run:
                _rb.MovePosition(_rb.position + new Vector2(-0.3f, 0) * _unitSpeed * Time.deltaTime);
                break;
            case UnitState.Attack:
                if(_attackCountdown <= 0f)
                {
                    EventBus.onAttackCastle?.Invoke(_unitDamage);
                    _attackCountdown = _attackCooldown;
                }
                _attackCountdown -= Time.deltaTime;
                break;
            case UnitState.Die:
                break;
        }
    }
    public void Spawn()
    {
        _unitMaxHp = GameStats.instance.enemyHp;
        _unitHp = _unitMaxHp;
        _unitDamage = GameStats.instance.enemyDamage;
        _sliderHp.value = _unitHp / _unitMaxHp;
        _state = UnitState.Run;
        _animation.PlayAnimation("1_Run");
        _sliderHp.gameObject.SetActive(true);
        _moneyLoot.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Castle")
        {
            _state = UnitState.Attack;

            _animation.PlayAnimation("2_Attack_Normal");
        }
        if(collision.gameObject.tag == "Bullet")
        {
            CollisionWithBullet();
        }
    }
    private void CollisionWithBullet()
    {
        _unitHp -= GameStats.instance.damage;
        _sliderHp.value = _unitHp / _unitMaxHp;
        if (_unitHp <= 0)
        {
            _state = UnitState.Die;
            _animation.PlayAnimation("4_Death");
            GetComponent<BoxCollider2D>().enabled = false;

            if (_isHorse)
            {
                DisplayMoneyLoot(2);
            }
            else
            {
                DisplayMoneyLoot(1);
            }
            _moneyLoot.gameObject.SetActive(true);
            _sliderHp.gameObject.SetActive(false);
            Invoke("Die", 2);

        }
    }
    private void Die()
    {
        _returnObject();
    }

    private void DisplayMoneyLoot(float money)
    {
        money *= GameStats.instance.moneyCoef;
        money = (float)Math.Round(money, 1);
        _moneyLoot.text = money.ToString() + "$";
        EventBus.onLootMoney?.Invoke(money);
    }

    public void DiePool(System.Action returnObject)
    {
        _returnObject = returnObject;
    }
}
