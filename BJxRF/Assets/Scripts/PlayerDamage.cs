using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDamage : MonoBehaviour
{
    [Header("Smash-like")]
    public float damagePercent = 0f;
    public float knockbackMultiplier = 0.1f;
    public float baseHitstun = 0.20f;
    public float iFrameDuration = 0.75f;
    public float horizontalKnockbackMultiplier = 2f;
    public float verticalKnockbackMultiplier = 1f;
    public float minHoriz = 0.6f;
    public float minVert = 0.1f;
    public float maxVert = 0.5f;
    public float knockbackDrag = 8f;

    [Header("Opcional[material de atrito baixo]")]
    public PhysicsMaterial2D zeroFrictionMaterial;

    public Movement MovementCt;

    Rigidbody2D rb;
    Collider2D col;
    float defaultDrag = 0f;
    PhysicsMaterial2D defaultMaterial;
    bool isInvincible = false;

    public bool InHitstun { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        defaultDrag = rb.linearDamping;
        if (col) defaultMaterial = col.sharedMaterial;
    }

    public void TakeHit(int damage, Vector2 hitDirection, float baseForce)
    {
        Debug.Log($"{name} took hit: {damage} damage, dir {hitDirection}, baseForce {baseForce}");
        if (isInvincible) return;

        MovementCt.cantMove = true;
        damagePercent += damage;

        float force = baseForce + (damagePercent * knockbackMultiplier);

        Vector2 dir = hitDirection.normalized;

        StartCoroutine(TemporaryDisableCollider(0.45f));

        // Garante mínimo de horizontal
        float horiz = Mathf.Clamp(dir.x, -1f, 1f);
        if (Mathf.Abs(horiz) < minHoriz)
            horiz = minHoriz * Mathf.Sign(dir.x != 0 ? dir.x : 1f);
        horiz *= horizontalKnockbackMultiplier;

        // Garante mínimo e máximo no vertical
        float vert = Mathf.Clamp(dir.y, -maxVert, maxVert);
        if (Mathf.Abs(vert) < minVert)
            vert = minVert * Mathf.Sign(dir.y != 0 ? dir.y : 1f);
        vert *= verticalKnockbackMultiplier;

        dir = new Vector2(horiz, vert);

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
        MovementCt.cantMove = false;
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        // Busca o SpriteRenderer no filho chamado "corpo"
        SpriteRenderer sr = null;
        Transform corpoTransform = transform.Find("Corpo");
        if (corpoTransform != null)
            sr = corpoTransform.GetComponent<SpriteRenderer>();

        float t = 0f;
        while (t < iFrameDuration)
        {
            if (sr) sr.enabled = !sr.enabled;
            t += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        if (sr) sr.enabled = true;

        isInvincible = false;
    }
    // Novo método para curar (reduzir danoPercent)
    public void Heal(float amount)
    {
        damagePercent -= amount;
        if (damagePercent < 0f)
            damagePercent = 0f;
        Debug.Log($"{gameObject.name} curado! Dano atual: {damagePercent}%");
    }

    public IEnumerator TemporaryDisableCollider(float duration)
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = false;                 // desativa colisão
        yield return new WaitForSeconds(duration);
        col.enabled = true;                  // reativa colisão
    }

}
