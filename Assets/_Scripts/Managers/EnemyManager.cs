using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject enemyPrefab;
    
    [Header("Enemy Settings")]
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxEnemies;
    [SerializeField] private Vector3 position;
    
    [Header("Player Target")]
    [SerializeField] private Transform playerTarget;
    
    private bool _isSpawning = false;

    private void Awake() 
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    public IEnumerator SpawnEnemy()
    {
        var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.transform.parent = transform;
        enemy.GetComponent<EnemyAIStateMotor>().target = playerTarget;
        _isSpawning = true;
        yield return new WaitForSeconds(spawnRate);
        _isSpawning = false;
    }

    private void Update()
    {
        if (transform.childCount < maxEnemies && !_isSpawning) StartCoroutine(SpawnEnemy());
    }
}
