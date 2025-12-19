using UnityEngine;

public class ActivarSpawnBomb : MonoBehaviour
{
    public BombaSpawner spawner;

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Nave"))
        {
            spawner.ActivarSpawner();
        }
    }
}

