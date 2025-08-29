using UnityEngine;
public class PickupHeal : MonoBehaviour
{
    public float healAmount = 20f; // quanto reduzir da porcentagem de dano
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDamage playerDamage = other.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.Heal(healAmount);
            }
            Destroy(gameObject);
        }
    }
}