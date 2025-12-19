using UnityEngine;
using UnityEngine.EventSystems;
public class ControlDisparo : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDeDisparo;
    
    [Header("Audio SFX")]
    public AudioClip sonidoDisparo;

    private AudioSource audioSource;

    void Update()
    {
        audioSource = GetComponent<AudioSource>();
        // Dispara con la BARRA ESPACIADORA
        if (Input.GetKeyDown(KeyCode.Space) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (sonidoDisparo != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoDisparo, 0.7f);
                Disparar();
            }
        }
    }

    void Disparar()
    {
        Instantiate(balaPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);
    }
}