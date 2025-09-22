// using UnityEngine;

// public class PickupSpeedBoost : MonoBehaviour
// {
//     public float speedMultiplier = 1.5f; // multiplicador da velocidade
//     public float duration = 5f; // duração do efeito em segundos
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Player player = other.GetComponent<Player>();
//             if (player != null)
//             {
//                 player.ActivateSpeedBoost(speedMultiplier, duration);
//             }
//             Destroy(gameObject);
//         }
//     }
// }
