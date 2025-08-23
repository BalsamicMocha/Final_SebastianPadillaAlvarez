using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.Play("Spikes");
            KillPlayer();
        }
    }

    private void KillPlayer()
    {        
        GameManager.Instance.GameOver();
    }
}
