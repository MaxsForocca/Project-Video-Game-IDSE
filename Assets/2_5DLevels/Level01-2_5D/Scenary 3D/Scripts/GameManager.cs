using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class GameManager : MonoBehaviour
{
    // Singleton: Esto permite llamar a este script desde cualquier otro lado fácilmente
    public static GameManager instancia;

    public GameObject panelVictoria; // Arrastra aquí tu cartel de "Nivel Terminado"

    private int enemigosRestantes;

    void Awake()
    {
        // Configuramos el Singleton
        if (instancia == null)
        {
            instancia = this;
        }
    }

    void Start()
    {
        // TRUCO: Cuenta automáticamente cuántos objetos tienen el tag "Enemigo"
        // Así no tienes que poner el número a mano.
        enemigosRestantes = GameObject.FindGameObjectsWithTag("Enemigo").Length;
        Debug.Log("Misión: Eliminar a " + enemigosRestantes + " enemigos.");
    }

    public void RestarEnemigo()
    {
        enemigosRestantes--;

        // Si ya no quedan enemigos...
        if (enemigosRestantes <= 0)
        {
            GanarNivel();
        }
    }

    void GanarNivel()
    {
        Debug.Log("¡NIVEL COMPLETADO!");

        // 1. Mostrar el cartel
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }

        // 2. Esperar 3 segundos antes de cambiar de escena
        Invoke("CargarSiguienteNivel", 3f);
    }

    void CargarSiguienteNivel()
    {
        // Carga la siguiente escena en la lista de Build Settings
        // Si estás en la escena 0, cargará la 1.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}