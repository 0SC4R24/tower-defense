using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    
    [Header("Settings")]
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float fireRate;
    
    [Header("Attack Range Settings")]
    [SerializeField] private LineRenderer attackRangeLine;
    [SerializeField] private float attackRange;
    [SerializeField] private int subdivisions;
    
    private bool _canFire = true;

    public void UpgradeAttackRange()
    {
        if (!(GameManager.Instance.GetCoins() >= 1)) return; 
        
        GameManager.Instance.SubtractCoins(1);
        attackRange += 1.0f;
    }
    
    private void DrawCircle()
    {
        float angleStep = 2f * Mathf.PI / subdivisions;
        
        attackRangeLine.positionCount = subdivisions;

        for (int i = 0; i < subdivisions; i++)
        {
            float xPosition = attackRange * Mathf.Cos(angleStep * i);
            float zPosition = attackRange * Mathf.Sin(angleStep * i);
            
            Vector3 pointInCircle = new Vector3(xPosition, 0f, zPosition);
            
            attackRangeLine.SetPosition(i, pointInCircle);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused()) return;
        
        DrawCircle();
        
        int maxColliders = 20;
        Collider[] hits = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, attackRange, hits);
        
        for (int i = 0; i < numColliders; i++)
        {
            if (hits[i].gameObject.CompareTag("Enemy") && _canFire) StartCoroutine(Fire(hits[i].gameObject));
        }
    }
    
    private IEnumerator Fire(GameObject enemy)
    {
        _canFire = false;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyAIStateMotor>().target = enemy.transform;
        var enemyStateMotor = enemy.GetComponent<EnemyAIStateMotor>();
        enemyStateMotor.targetRb = bullet.GetComponent<Rigidbody>();
        enemyStateMotor.target = bullet.transform;
        enemyStateMotor.stateEnum = AIState.Evade;
        bullet.transform.parent = transform.parent;
        Destroy(bullet, bulletLifeTime);
        yield return new WaitForSeconds(fireRate);
        _canFire = true;
    }
}
