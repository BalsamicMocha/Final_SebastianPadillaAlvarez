using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyID;
    public int health = 100;

    private void Start()
    {       
        if (GameManager.Instance != null && GameManager.Instance.IsEnemyDead(enemyID))
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        AudioManager.Instance.Play("Damaged");
        if (health <= 0)
        {            
            GameManager.Instance.RegisterEnemyDeath(enemyID);
            Destroy(gameObject);
        }
    }
}

