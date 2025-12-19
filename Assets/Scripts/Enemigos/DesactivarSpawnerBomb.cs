using UnityEngine;

public class DesactivarSpawnerBomb : MonoBehaviour
{
    public BombaSpawner spawner;

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Nave"))
        {
            spawner.DesactivarSpawner();
        }
    }
}
