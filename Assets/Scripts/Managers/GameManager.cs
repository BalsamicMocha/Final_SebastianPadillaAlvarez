using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public TMP_Text statusText;

    [Header("Checkpoint System")]
    public Transform lastCheckpoint;
    public AmmoManager playerAmmo;
    private HashSet<string> defeatedEnemies = new HashSet<string>();

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        AudioManager.Instance.Play("Background");
        LoadCheckpoint();
    }

    void Update()
    {
        if (gameEnded) return;

        // Verificar si quedan enemigos
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //Victory();
        }
    }
    
    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (statusText != null) statusText.text = "GAME OVER";
        AudioManager.Instance.Play("Death");
        PostProcessManager.Instance.TriggerDeathVignette();

        // Reiniciar tras unos segundos
        Invoke(nameof(ReloadScene), 3f);
    }

    public void Victory()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (victoryPanel != null) victoryPanel.SetActive(true);
        if (statusText != null) statusText.text = "VICTORY CREDITS SEBASTIAN PADILLA ALVAREZ";
        PostProcessManager.Instance.TriggerChromaticAberration();
        AudioManager.Instance.Play("Victory");

        Invoke(nameof(MainMenu), 8f);
    }

    void ReloadScene()
    {
        PostProcessManager.Instance.ResetVignette();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
   
    public void SetCheckpoint(Transform checkpoint)
    {
        lastCheckpoint = checkpoint;
        Debug.Log("Checkpoint guardado");
    }

    public void RespawnPlayer(GameObject player)
    {
        if (lastCheckpoint != null)
        {
            player.transform.position = lastCheckpoint.position;
            player.transform.rotation = lastCheckpoint.rotation;
        }
        else
        {
            Debug.Log("No hay checkpoint, reiniciando escena");
            ReloadScene();
        }
    }

    public void LoadCheckpoint()
    {     
        SaveData data = SaveSystem.LoadGame();
        if (data != null)
        {
            // Restaurar posicion/balas del jugador
            if (playerAmmo != null && playerAmmo.gameObject != null)
            {
                playerAmmo.transform.position = data.playerPosition;
                playerAmmo.currentAmmo = data.currentAmmo;
                playerAmmo.UpdateUI();
            }

            // Guardar enemigos muertos en GameManager
            defeatedEnemies = new HashSet<string>(data.defeatedEnemies);

            // Eliminar enemigos ya derrotados de la escena
            Enemy[] allEnemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in allEnemies)
            {
                if (defeatedEnemies.Contains(enemy.enemyID))
                {
                    Destroy(enemy.gameObject);
                }
            }
        }
    }
// Enemigos muertos (temporal)
public void RegisterEnemyDeath(string enemyID)
    {
        if (!defeatedEnemies.Contains(enemyID))
            defeatedEnemies.Add(enemyID);
    }

    public bool IsEnemyDead(string enemyID)
    {
        return defeatedEnemies.Contains(enemyID);
    }

    public List<string> GetDefeatedEnemiesList()
    {
        return new List<string>(defeatedEnemies);
    }
}
