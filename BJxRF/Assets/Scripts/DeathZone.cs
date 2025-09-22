using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    [Header("Nome da cena do menu")]
    public string menuSceneName = "MainMenu"; // coloque o nome exato da sua cena de menu

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log($"{other.name} saiu da arena! Voltando para o menu...");
            SceneManager.LoadScene(menuSceneName);
        }
    }
}
