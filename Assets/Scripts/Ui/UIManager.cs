using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Arrastra aquí tus componentes de UI")]
    public Image barraVida;
    public Image barraCombustible;
    public TMP_Text textoScore;

    private int scoreTotal = 0;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Función para actualizar la Vida, recibe valor entre 0 y 1
    public void ActualizarVida(float vidaActual, float vidaMaxima)
    {
        if (barraVida != null)
        {
            barraVida.fillAmount = vidaActual / vidaMaxima;
        }
    }

    // Función para actualizar el Combustible
    public void ActualizarCombustible(float combActual, float combMaximo)
    {
        if (barraCombustible != null)
        {
            barraCombustible.fillAmount = combActual / combMaximo;
        }
    }

    // Función para sumar puntos
    public void SumarPuntos(int puntos)
    {
        scoreTotal += puntos;
        if (textoScore != null)
        {
            textoScore.text = scoreTotal.ToString("D7");
        }
    }
}