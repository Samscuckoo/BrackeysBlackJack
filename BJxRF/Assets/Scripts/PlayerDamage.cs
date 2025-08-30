using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDamage : MonoBehaviour
{
<<<<<<< HEAD
    [Header("Smash Bros. Style")]
    public float damagePercent = 0f;  // começa em 0%
    public float knockbackMultiplier = 0.1f; // controla o quanto a % aumenta o empurrão
    private Rigidbody2D rb;
    private void Awake()
=======
    [Header("Smash-like")]
    public float damagePercent = 0f;
    public float knockbackMultiplier = 0.1f; 
    public float baseHitstun = 0.20f;        
    public float iFrameDuration = 0.75f;    
    public float minHoriz = 0.6f;            
    public float maxVert = 0.5f;            
    public float knockbackDrag = 8f;        

    [Header("Opcional[material de atrito baixo]")]
    public PhysicsMaterial2D zeroFrictionMaterial;

    Rigidbody2D rb;
    Collider2D col;
    float defaultDrag = 0f;
    PhysicsMaterial2D defaultMaterial;
    bool isInvincible = false;

    public bool InHitstun { get; private set; }

    void Awake()
>>>>>>> b3a321ef8a98b4c59f6c0691eebd93dddbeff92b
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        defaultDrag = rb.linearDamping;
        if (col) defaultMaterial = col.sharedMaterial;
    }
<<<<<<< HEAD
    // chamado quando leva um tiro/golpe
    public void TakeHit(int damage, Vector2 hitDirection, float baseForce)
    {
        damagePercent += damage;
        float knockbackForce = baseForce + (damagePercent * knockbackMultiplier);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(hitDirection.normalized * knockbackForce, ForceMode2D.Impulse);
        Debug.Log($"{gameObject.name} agora tem {damagePercent}% de dano!");
=======

    public void TakeHit(int damage, Vector2 hitDirection, float baseForce)
    {
        if (isInvincible) return;

        damagePercent += damage;

        float force = baseForce + (damagePercent * knockbackMultiplier);

        Vector2 dir = hitDirection.normalized;

        float signX = Mathf.Approximately(dir.x, 0f) ? 1f : Mathf.Sign(dir.x);
        dir.x = Mathf.Clamp(Mathf.Abs(dir.x), minHoriz, 1f) * signX;

        dir.y = Mathf.Clamp(dir.y, -maxVert, maxVert);

      
        StartCoroutine(ApplyHit(dir, force));
        StartCoroutine(InvincibilityFrames());

        Debug.Log($"{name}: {damagePercent}%");
    }

    IEnumerator ApplyHit(Vector2 dir, float force)
    {
        InHitstun = true;

        rb.linearVelocity = Vector2.zero;

        rb.linearDamping = knockbackDrag;

        if (col && zeroFrictionMaterial) col.sharedMaterial = zeroFrictionMaterial;
        
        rb.AddForce(dir * force, ForceMode2D.Impulse);

       
        float duration = baseHitstun + Mathf.Clamp(force * 0.01f, 0f, 0.35f);
        yield return new WaitForSeconds(duration);

        
        rb.linearDamping = defaultDrag;
        if (col) col.sharedMaterial = defaultMaterial;
        InHitstun = false;
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

    
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float t = 0f;
        while (t < iFrameDuration)
        {
            if (sr) sr.enabled = !sr.enabled;
            t += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        if (sr) sr.enabled = true;

        isInvincible = false;
>>>>>>> b3a321ef8a98b4c59f6c0691eebd93dddbeff92b
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
