using UnityEngine;

public class Puerta : MonoBehaviour
{
    public bool estaAbierta = false;

    public void Abrir()
    {
        estaAbierta = true;
        gameObject.SetActive(false);
    }
}
