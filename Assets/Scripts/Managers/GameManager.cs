using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Spawn spawner;

    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public TMP_Text statusText;
    public TMP_Text kills;

    public AmmoManager playerAmmo;
    private int totalKills;
    [SerializeField]private int victoryKills =5;

    private bool gameEnded = false;

    void Awake()
    {
        totalKills = 0;
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EnemyKilled()
    {
        totalKills++;
        UpdateKills();

        if (totalKills >= victoryKills)
            spawner.Victory();
            Victory();

    }


    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (statusText != null) statusText.text = "GAME OVER";

        // Reiniciar tras unos segundos
        Invoke(nameof(ReloadScene), 3f);
    }

    public void Victory()
    {
        

        if (victoryPanel != null) victoryPanel.SetActive(true);
        if (statusText != null) statusText.text = "VICTORIA";

        Invoke(nameof(MainMenu), 8f);
    }

    public void UpdateKills()
    {
        if (kills != null)
            kills.text = "Eliminados: " + totalKills;
    }

    void ReloadScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

   

    public void RespawnPlayer(GameObject player)
    {       
            ReloadScene();        
    }
}
