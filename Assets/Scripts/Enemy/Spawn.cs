using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float spawnRate;

    [SerializeField] private int poolSize;
    [SerializeField] private int maxObjectsInScene;
    [SerializeField] private int activeObjects;

    Queue<GameObject> pool;

    private GameManager gameManager;

    private void Start()
    {
        pool = new Queue<GameObject>();

        for(int i = 0; i<poolSize; i++)
        {
            GameObject instance = Instantiate(objectToSpawn);
            instance.SetActive(false);
            pool.Enqueue(instance);
        }

        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        for (int i = activeObjects; i < maxObjectsInScene; i++)
        {
            yield return new WaitForSeconds(spawnRate);
            GameObject objeto = pool.Dequeue();
            objeto.transform.position = GetRandomSpawn().position;
            objeto.SetActive(true);

            var enemy = objeto.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Initialize(this, GameManager.Instance);
            }
        }
    }

    public void BackToQueue(GameObject objeto)
    {
        objeto.SetActive(false);
        pool.Enqueue(objeto);
    }

    private Transform GetRandomSpawn()
    {
        int randomSpawn = Random.Range(0, spawnPoints.Length);

        return spawnPoints[randomSpawn];
    }

    public void Victory()
    {
        StopAllCoroutines();        
    }

    
}