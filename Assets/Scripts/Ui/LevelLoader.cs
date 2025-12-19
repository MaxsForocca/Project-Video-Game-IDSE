using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void CargarEscenaPorNombre(string nombreDeLaEscena)
    {
        if (!string.IsNullOrEmpty(nombreDeLaEscena))
        {
            SceneManager.LoadScene(nombreDeLaEscena);
        }
        else
        {
            Debug.LogError("¡Error! Has intentado cargar una escena sin nombre.");
        }
    }
}