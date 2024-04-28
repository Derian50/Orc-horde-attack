using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class DemoShooting2D : MonoBehaviour
{
    private const int BULLET_PRELOAD_COUNT = 5;
    public GameObject FirePoint;
    public Camera Cam;
    public GameObject PrefabBullet;

    [SerializeField] float _attackSpeed = 1;
    private float fireCountdown = 0f;

    private PoolBase<GameObject> _bulletPool;

    private const int ANGLE_FOR_MULTISHOT = 4;

    void Awake()
    {
        _bulletPool = new PoolBase<GameObject>(Preload, GetAction, ReturnAction, BULLET_PRELOAD_COUNT);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && fireCountdown <= 0f)
        {
            Attack();
            
            fireCountdown = 0;
            fireCountdown += 1 / GameStats.instance.atackSpeed;
        }
        fireCountdown -= Time.deltaTime;
    }
    private void Attack()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        for(int i = 0; i < GameStats.instance.multishotCount; i++)
        {
            GameObject bullet = _bulletPool.Get();
            bullet.transform.position = FirePoint.transform.position;
            bullet.transform.rotation = FirePoint.transform.rotation;
            bullet.transform.Rotate(0, ANGLE_FOR_MULTISHOT * (i - (GameStats.instance.multishotCount - 1)) , 0);
            bullet.GetComponent<ProjectileMover2D>().Shoot(ReturnObject);
            void ReturnObject() => _bulletPool.Return(bullet);
        }


    }
    public GameObject Preload() => Instantiate(PrefabBullet);
    public void GetAction(GameObject bullet) => bullet.gameObject.SetActive(true);
    public void ReturnAction(GameObject bullet) => bullet.gameObject.SetActive(false);
}
