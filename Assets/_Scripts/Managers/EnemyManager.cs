using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject enemyPrefab;
    
    [Header("Enemy Settings")]
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxEnemies;
    [SerializeField] private Vector3 position;
    [SerializeField] private GameObject enemiesPool;
    
    [Header("Player Target")]
    [SerializeField] private Transform playerTarget;
    
    private bool _isSpawning = false;
    private bool _bringFromPool = false;
    private int _enemiesCount = 0;

    private void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
        } 
        else 
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        _enemiesCount++;
        var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.transform.parent = transform;
        enemy.GetComponent<EnemyAIStateMotor>().target = playerTarget;
        _isSpawning = true;
        yield return new WaitForSeconds(spawnRate);
        _isSpawning = false;
    }

    private void DespawnEnemy(GameObject enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.parent = enemiesPool.transform;
        GameManager.Instance.AddCoins(GameManager.Instance.GetEnemyReward());
        
        if (enemiesPool.transform.childCount >= maxEnemies && _enemiesCount >= maxEnemies)
        {
            _enemiesCount = 0;
            _bringFromPool = true;
            GameManager.Instance.UpgradeEnemyHealth(1);
            GameManager.Instance.UpgradeEnemyReward(1);
        }
    }
    
    public void RemoveEnemy(GameObject enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.parent = enemiesPool.transform;
    }

    public void DamageEnemy(GameObject enemy, int damage)
    {
        var enemyHealth = enemy.GetComponent<EnemyAIStateMotor>();
        var isDead = enemyHealth.TakeDamage(damage);
        
        if (isDead) DespawnEnemy(enemy);
    }
    
    private IEnumerator BringEnemy()
    {
        _enemiesCount++;
        var enemy = enemiesPool.transform.GetChild(0).gameObject;
        enemy.SetActive(true);
        enemy.transform.position = position;
        enemy.transform.parent = transform;
        enemy.GetComponent<EnemyAIStateMotor>().target = playerTarget;
        enemy.GetComponent<AIBehaviour>().SetNewRoute(PatrolRoute.Route01);
        _isSpawning = true;
        yield return new WaitForSeconds(spawnRate);
        _isSpawning = false;
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused()) return;

        var check = transform.childCount < maxEnemies && _enemiesCount < maxEnemies && !_isSpawning;
        if (check && !_bringFromPool) StartCoroutine(SpawnEnemy());
        else if (check && _bringFromPool) StartCoroutine(BringEnemy());
    }
}
