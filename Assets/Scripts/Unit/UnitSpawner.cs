using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] _unitsPrefabs;
    [SerializeField] private float _spawnCooldown = 2;
    [SerializeField] private GameObject[] _spawnCorner;
    private PoolBase<GameObject> _unitPool;
    private const int UNIT_PRELOAD_COUNT = 10;
    [SerializeField] private const int TIME_BEFORE_FIRST_ATTACK = 10;
    void Awake()
    {
        _unitPool = new PoolBase<GameObject>(Preload, GetAction, ReturnAction, UNIT_PRELOAD_COUNT);
        StartCoroutine(SpawnUnit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    IEnumerator SpawnUnit()
    {
        yield return new WaitForSeconds(TIME_BEFORE_FIRST_ATTACK);
        while (true)
        {
            yield return new WaitForSeconds(_spawnCooldown);
            if (_spawnCooldown >= 0.3f) _spawnCooldown -= 0.003f;
            GameObject unit = _unitPool.Get();
            unit.transform.position = new Vector3(_spawnCorner[0].transform.position.x, Random.Range(_spawnCorner[0].transform.position.y, _spawnCorner[1].transform.position.y));
            unit.GetComponent<Unit>().Spawn();
            unit.GetComponent<Unit>().DiePool(ReturnObject);
            unit.GetComponent<BoxCollider2D>().enabled = true;
            void ReturnObject() => _unitPool.Return(unit);
        }
       

    }

    public GameObject Preload() => Instantiate(_unitsPrefabs[Random.Range(0,_unitsPrefabs.Length)]);
    public void GetAction(GameObject unit) => unit.gameObject.SetActive(true);
    public void ReturnAction(GameObject unit) => unit.gameObject.SetActive(false);
}
