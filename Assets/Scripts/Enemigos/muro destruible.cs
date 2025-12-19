using UnityEngine;

public class MuroFinal : MonoBehaviour
{
    void Update()
    {
        if (BossDorado.bossDoradoMuerto)
        {
            Destroy(gameObject);
        }
    }
}
