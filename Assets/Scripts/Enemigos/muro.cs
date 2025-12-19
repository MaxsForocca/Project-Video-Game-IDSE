using UnityEngine;

public class Muro : MonoBehaviour
{
    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Nave"))
        {
            Destroy(otro.gameObject);
        }
    }
}
