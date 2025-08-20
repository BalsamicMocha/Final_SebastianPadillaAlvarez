using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Asegúrate de que el Player tenga este tag

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

