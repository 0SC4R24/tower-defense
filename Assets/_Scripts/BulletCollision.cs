using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnDestroy()
    {
        var enemy = this.gameObject.GetComponent<EnemyAIStateMotor>().target.gameObject;
        var enemyStateMotor = enemy.GetComponent<EnemyAIStateMotor>();
        
        enemyStateMotor.target = null;
        enemyStateMotor.targetRb = null;
        enemyStateMotor.stateEnum = AIState.FollowPath;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        
        EnemyManager.Instance.DamageEnemy(other.gameObject, GameManager.Instance.GetBulletDamage());
        Destroy(gameObject);
    }
}
