using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LowHealthWarning : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float lowHealthThreshold = 0.25f; // 25%
    [SerializeField] private AudioClip warningSound;

    private Slider healthSlider;
    private AudioSource audioSource;
    private bool warningPlayed = false;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float healthNormalized = healthSlider.value;

        if (healthNormalized <= lowHealthThreshold)
        {
            if (!warningPlayed)
            {
                PlayWarning();
                warningPlayed = true;
            }
        }
        else
        {
            // Se resetea si el jugador se cura
            warningPlayed = false;
        }
    }

    private void PlayWarning()
    {
        if (audioSource != null && warningSound != null)
        {
            audioSource.PlayOneShot(warningSound);
        }
    }
}
