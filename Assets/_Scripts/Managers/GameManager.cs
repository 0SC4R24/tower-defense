using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Settings")]
    [SerializeField] private int coins;
    [SerializeField] private int bulletDamage;
    [SerializeField] private int enemyHealth;
    [SerializeField] private int enemyReward;
    
    private bool _isPaused = false;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public void AddCoins(int amount)
    {
        coins += amount;
        UIController.Instance.UpdateScore(coins);
    }
    
    public void SubtractCoins(int amount)
    {
        coins -= amount;
        UIController.Instance.UpdateScore(coins);
    }
    
    public int GetCoins()
    {
        return coins;
    }
    
    public int GetBulletDamage()
    {
        return bulletDamage;
    }
    
    public int GetEnemyHealth()
    {
        return enemyHealth;
    }
    
    public int GetEnemyReward()
    {
        return enemyReward;
    }
    
    public void UpgradeBulletDamage(int amount)
    {
        if (!(GameManager.Instance.GetCoins() >= 1)) return;
        
        GameManager.Instance.SubtractCoins(1);
        bulletDamage += amount;
    }
    
    public void UpgradeEnemyHealth(int amount)
    {
        enemyHealth += amount;
    }
    
    public void UpgradeEnemyReward(int amount)
    {
        enemyReward += amount;
    }

    public void ChangePaused()
    {
        _isPaused = !_isPaused;
        UIController.Instance.ShowPauseMenu(_isPaused);
    }
    
    public bool IsPaused()
    {
        return _isPaused;
    }
}
