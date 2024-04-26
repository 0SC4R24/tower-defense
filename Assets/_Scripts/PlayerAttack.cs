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
    
    private bool _canFire = true;
    
    private GameObject nearestEnemy;

    private void Update()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0, -10), Color.cyan, 2.5f);
        if (Physics.SphereCast(transform.position, 10.0f, transform.forward, out hit, 10.0f) && _canFire)
        {
            nearestEnemy = hit.collider.gameObject;
            StartCoroutine(Fire());
        }
    }
    
    private IEnumerator Fire()
    {
        Debug.Log("Fire");
        _canFire = false;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyAIStateMotor>().target = nearestEnemy.transform;
        Destroy(bullet, bulletLifeTime);
        yield return new WaitForSeconds(fireRate);
        _canFire = true;
    }
    
    private Transform GetNearestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var nearestEnemy = enemies[0];
        var nearestDistance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
        
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }
        
        return nearestEnemy.transform;
    }
}
