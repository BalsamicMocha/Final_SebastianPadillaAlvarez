using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Spawn spawner;
    private GameManager manager;

    public void Initialize(Spawn spawner, GameManager manager)
    {
        this.spawner = spawner;
        this.manager = manager;
    }
        

    public void Die()
    {
        spawner.BackToQueue(gameObject);
        manager.EnemyKilled();

    }
    
}

