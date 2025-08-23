using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerDetection : MonoBehaviour
{
    private EnemyController controller;

    private void Start()
    {
        controller = GetComponentInParent<EnemyController>();
    }
    private void Awake()
    {
        SphereCollider col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 20f; // radio de detección del jugador
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.StartChasing(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.StopChasing();
        }
    }
}

