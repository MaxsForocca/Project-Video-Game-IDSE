using UnityEngine;

public class FondoSimple : MonoBehaviour
{
    public float velocidadRelativa = 0.5f; // 0.5 = mitad de velocidad que la camara
    public Transform camara;

    private float startPos;
    private float length;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (camara.position.x * (1 - velocidadRelativa));
        float dist = (camara.position.x * velocidadRelativa);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}