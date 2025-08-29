using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    [HideInInspector] public int damage;     
    [HideInInspector] public float lifeTime; 
    [HideInInspector] public GameObject owner;

    public float baseKnockback = 5f; // força mínima

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;
        if (collision.GetComponent<Bullet>() != null) return;

        PlayerDamage player = collision.GetComponent<PlayerDamage>();
        if (player != null)
        {
            // direção da colisão = da bala
            Vector2 hitDir = transform.right;
            player.TakeHit(damage, hitDir, baseKnockback);
        }

        Destroy(gameObject);
    }
}
