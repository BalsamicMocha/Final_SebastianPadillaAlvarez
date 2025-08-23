using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string gameSceneName = "Level"; 

    
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
