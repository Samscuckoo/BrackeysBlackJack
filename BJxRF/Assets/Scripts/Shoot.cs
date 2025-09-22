using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{

    [Header("Referências")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    //Enum com os fire modes
    public enum FireMode { Single, TripleShot }

    [Header("Shooting Settings")]
    public float bulletForce = 20f;
    public float fireRate = 0.5f;
    public float lifeTime = 2f;
    public int bulletDamage = 10;
    public FireMode currentFireMode = FireMode.TripleShot;

    [Header("Card System")]
    public List<int> playerHand = new List<int>();
    public float damagePerCard = 2f;
    public float special21Multiplier = 2f;
    public int maxCards = 10;
    public float cardBonusDuration = 5f;
    private bool canShoot = true;
    private float cardBonusTimer = 0f;
    private List<int> deck = new List<int>();
    private float nextFireTime = 0f;

    public int sum = 0; // <-- Torna sum público

    private SpriteRenderer spriteRenderer;
    private Color originalColor = Color.white;
    private Coroutine flashCoroutine;

    void Start()
    {
        for (int i = 1; i <= 13; i++)
        {
            for (int j = 0; j < 4; j++)
                deck.Add(i);
        }
        ShuffleDeck();

        // Corrigido: busca SpriteRenderer no filho "Corpo"
        Transform corpoTransform = transform.Find("Corpo");
        if (corpoTransform != null)
            spriteRenderer = corpoTransform.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }


    void Update()
    {
        if (!GameManager.Instance.canPlayersMove) return;
        if (cardBonusTimer > 0f)
        {
            cardBonusTimer -= Time.deltaTime;
            if (cardBonusTimer <= 0f)
            {
                bulletDamage = 10;
                playerHand.Clear(); // Limpa a mão quando o tempo acaba
                canShoot = true;
                sum = 0;
            }
        }
    }

    public void Fire()
    {
        if (!GameManager.Instance.canPlayersMove) return;
        if (!canShoot) return;
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        switch (currentFireMode)
        {
            case FireMode.Single:
                SpawnBullet(firePoint.position, firePoint.rotation);
                break;

            case FireMode.TripleShot:
                SpawnBullet(firePoint.position, firePoint.rotation);
                SpawnBullet(firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 30));
                SpawnBullet(firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 15));
                break;
        }
    }
    public void ActivateTripleShot(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(TripleShotRoutine(duration));
    }
    private IEnumerator TripleShotRoutine(float duration)
    {
        currentFireMode = FireMode.TripleShot;
        yield return new WaitForSeconds(duration);
        currentFireMode = FireMode.Single;
    }
    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);

        // pega o script Bullet
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.damage = bulletDamage;
            b.lifeTime = lifeTime;
            b.owner = gameObject;
        }

        // movimento da bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = bullet.transform.right * bulletForce;
        }

    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int rnd = Random.Range(0, deck.Count);
            int temp = deck[i];
            deck[i] = deck[rnd];
            deck[rnd] = temp;
        }
    }

    public void OnDrawCard(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.canPlayersMove) return;
        if (context.started)
            DrawCard();
    }

    private void DrawCard()
    {
        if (!canShoot) return; // Impede comprar cartas se estourado
        if (deck.Count == 0) return;

        int card = deck[0];
        deck.RemoveAt(0);
        playerHand.Add(card);

        sum = 0;
        foreach (int c in playerHand)
            sum += Mathf.Min(c, 10);

        if (sum > 21)
        {
            canShoot = false;
            bulletDamage = 0;
            if (spriteRenderer != null)
            {
                if (flashCoroutine != null)
                    StopCoroutine(flashCoroutine);
                flashCoroutine = StartCoroutine(FlashRed(cardBonusDuration));
            }
            playerHand.Clear();
        }
        else
        {
            canShoot = true;
            bulletDamage = 10 + sum * (int)damagePerCard;
            if (sum == 21)
                bulletDamage = (int)(bulletDamage * special21Multiplier);

            cardBonusTimer = cardBonusDuration;
        }

        Debug.Log($"Card drawn: {card}. Hand sum: {sum}. Bullet damage: {bulletDamage}");
    }

    private IEnumerator FlashRed(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
            timer += 0.2f;
        }
        sum = 0;
        spriteRenderer.color = originalColor;
    }
}
