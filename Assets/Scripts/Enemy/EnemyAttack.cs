using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string playerTag = "Player";

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {        
        GameManager.Instance.GameOver();
    }
}

