using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelManager : MonoBehaviour
{
    // Cargar escena por índice
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning("Índice de escena inválido");
        }
    }

    // Cargar escena por nombre (opcional pero recomendable)
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Salir del juego
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
