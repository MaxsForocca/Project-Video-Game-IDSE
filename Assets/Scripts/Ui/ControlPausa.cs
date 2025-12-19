using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPausa : MonoBehaviour
{
    [Header("Configuración UI")]
    public GameObject menuPausaUI;

    [Header("Configuración de Escenas")]
    [Tooltip("Escribe aquí el nombre EXACTO de tu escena del Menú Principal")]
    public string nombreEscenaMenu = "Inicio"; // Nombre por defecto

    private bool juegoPausado = false;

    void Start()
    {
        Reanudar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado) Reanudar();
            else Pausar();
        }
    }

    public void Reanudar()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;

        // Bloqueamos cursor para seguir jugando
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;

        // Liberamos cursor para usar el menú
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CargarMenuPrincipal()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(nombreEscenaMenu);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}