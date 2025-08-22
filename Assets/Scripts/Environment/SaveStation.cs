using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour
{
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;
            Debug.Log("Checkpoint activado");
          
            GameManager.Instance.SetCheckpoint(this.transform);           
            SaveData data = new SaveData
            {
                playerPosition = other.transform.position,
                currentAmmo = GameManager.Instance.playerAmmo.currentAmmo,
                defeatedEnemies = GameManager.Instance.GetDefeatedEnemiesList()
            };

            SaveSystem.SaveGame(data);
            Destroy(this.gameObject);
        }
    }
}