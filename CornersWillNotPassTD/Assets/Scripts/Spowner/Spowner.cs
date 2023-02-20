using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpownMods
{
    Fixed,
    Random
}


public class Spowner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpownMods spownMod = SpownMods.Fixed;
    [SerializeField] int numberEnemyToSpown;
    [SerializeField] private float delayBtwWaves = 1f;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    private float _spawnTimer;
    private int _enemesSpowned;
    private int _enemiesRamaining;

    private ObgectPooler _pooler;
    private WayPoint _wayPoint;

    private void Start()
    {
        _pooler = GetComponent<ObgectPooler>();
        _wayPoint = GetComponent<WayPoint>();

        _enemiesRamaining = numberEnemyToSpown;
    }

    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpownDelay(); ;
            if(_enemesSpowned < numberEnemyToSpown)
            {
                _enemesSpowned++;
                SpownEnemy();
            }
        }
    }

    private void SpownEnemy()
    {
        GameObject newInstanse = _pooler.GetInstanseFromPool();
        Enemy enemy = newInstanse.GetComponent<Enemy>();
        enemy.WayPoint = _wayPoint;
        enemy.ResetEnemy();

        enemy.transform.position = transform.position;

        newInstanse.SetActive(true);
    }

    private float GetSpownDelay()
    {
        float delay = 0f;
        if(spownMod == SpownMods.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }
        return delay;
    }

    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRamaining = numberEnemyToSpown;
        _spawnTimer = 0;
        _enemesSpowned = 0;
    }

    private void RecordEnemy(Enemy enemy)
    {
        _enemiesRamaining--;
        if (_enemiesRamaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }

}
