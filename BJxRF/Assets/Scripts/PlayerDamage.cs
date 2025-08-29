using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDamage : MonoBehaviour
{
    [Header("Smash Bros. Style")]
    public float damagePercent = 0f;  // começa em 0%
    public float knockbackMultiplier = 0.1f; // controla o quanto a % aumenta o empurrão
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // chamado quando leva um tiro/golpe
    public void TakeHit(int damage, Vector2 hitDirection, float baseForce)
    {
        damagePercent += damage;
        float knockbackForce = baseForce + (damagePercent * knockbackMultiplier);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(hitDirection.normalized * knockbackForce, ForceMode2D.Impulse);
        Debug.Log($"{gameObject.name} agora tem {damagePercent}% de dano!");
    }
    // Novo método para curar (reduzir danoPercent)
    public void Heal(float amount)
    {
        damagePercent -= amount;
        if (damagePercent < 0f)
            damagePercent = 0f;
        Debug.Log($"{gameObject.name} curado! Dano atual: {damagePercent}%");
    }
}
