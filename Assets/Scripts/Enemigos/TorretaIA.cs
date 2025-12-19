using UnityEngine;
using System.Collections;

public class TorretaIA : MonoBehaviour
{
    [Header("Fire Points")]
    public Transform firePointL;
    public Transform firePointR;
    public GameObject balaPrefabTorreta;

    [Header("Player Target")]
    public string nombrePuntoDisparoPlayer = "PuntoDisparo";

    [Header("Disparo")]
    public float rangoDeteccion = 300f;
    public float velocidadBala = 90f;
    public float cooldown = 0.08f;
    public int daño = 10;

    private Transform player;
    private Transform puntoObjetivoPlayer;
    private bool disparando;

    void Update()
    {
        BuscarPlayer();

        if (player == null || puntoObjetivoPlayer == null)
            return;

        if (!disparando && Vector3.Distance(transform.position, player.position) <= rangoDeteccion)
            StartCoroutine(DisparoContinuo());
    }

    void BuscarPlayer()
    {
        if (player != null) return;

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj == null) return;

        player = obj.transform;
        puntoObjetivoPlayer = obj.transform.Find(nombrePuntoDisparoPlayer);

        if (puntoObjetivoPlayer == null)
            Debug.LogError("No se encontró el PuntoDisparo del Player");
    }

    IEnumerator DisparoContinuo()
    {
        disparando = true;

        while (player != null && puntoObjetivoPlayer != null)
        {
            Disparar(firePointL);
            Disparar(firePointR);

            yield return new WaitForSeconds(cooldown);
        }

        disparando = false;
    }

    void Disparar(Transform firePoint)
    {
        if (firePoint == null || balaPrefabTorreta == null)
            return;

        Vector3 direccion = (puntoObjetivoPlayer.position - firePoint.position).normalized;

        GameObject bala = Instantiate(
            balaPrefabTorreta,
            firePoint.position,
            Quaternion.LookRotation(direccion)
        );

        Rigidbody rb = bala.GetComponent<Rigidbody>() ?? bala.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.linearVelocity = direccion * velocidadBala;

        SphereCollider col = bala.GetComponent<SphereCollider>() ?? bala.AddComponent<SphereCollider>();
        col.isTrigger = false;        // 🔴 MUY IMPORTANTE
        col.radius = 0.12f;

        BalaTorretaLogica logica = bala.GetComponent<BalaTorretaLogica>() ?? bala.AddComponent<BalaTorretaLogica>();
        logica.daño = daño;
    }

}
