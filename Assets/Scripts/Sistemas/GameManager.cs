using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public int nivelActual = 1;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void CargarSiguienteNivel()
    {
        nivelActual++;
        SceneManager.LoadScene("Nivel_" + nivelActual);
    }
}
