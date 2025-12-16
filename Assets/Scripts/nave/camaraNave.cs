using UnityEngine;

public class CamaraNave : MonoBehaviour
{
    public Transform nave;
    public float suavidad = 0.15f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    private Vector3 velocidad = Vector3.zero;

    void Start()
    {
        if (nave == null)
        {
            GameObject objetoNave = GameObject.FindGameObjectWithTag("Nave");
            if (objetoNave != null)
            {
                nave = objetoNave.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (nave == null) return;

        Vector3 posicionDeseada = nave.position + offset;
        Vector3 posicionSuavizada = Vector3.SmoothDamp(transform.position, posicionDeseada, ref velocidad, suavidad);

        transform.position = posicionSuavizada;
    }
}