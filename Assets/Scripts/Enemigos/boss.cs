using System.Collections;
using UnityEngine;

public class BossFinal : MonoBehaviour
{
    public float jumpForceX = 28f;
    public float jumpForceY = 18f;

    public GameObject bombPrefab;
    public Transform bombSpawn;

    Rigidbody2D rb;

    bool grounded;
    bool canAct = true;

    enum Side { Left, Right }
    Side currentSide;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (!grounded || !canAct) return;

        canAct = false;
        StartCoroutine(Act());
    }

    IEnumerator Act()
    {
        ThrowBombs();
        yield return new WaitForSeconds(0.5f);
        JumpToOtherSide();
    }

    void JumpToOtherSide()
    {
        grounded = false;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        float dir = currentSide == Side.Left ? 1f : -1f;

        rb.AddForce(new Vector2(dir * jumpForceX, jumpForceY), ForceMode2D.Impulse);
    }

    void ThrowBombs()
    {
        int count = Random.Range(1, 6);

        for (int i = 0; i < count; i++)
        {
            GameObject b = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
            Rigidbody2D brb = b.GetComponent<Rigidbody2D>();
            if (!brb) continue;

            float dir = currentSide == Side.Left ? 1f : -1f;
            brb.AddForce(new Vector2(dir * 6f, 6f), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Ground")) return;

        grounded = true;
        canAct = true;

        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("LeftLimit"))
            currentSide = Side.Left;
        else if (col.CompareTag("RightLimit"))
            currentSide = Side.Right;
    }
}



