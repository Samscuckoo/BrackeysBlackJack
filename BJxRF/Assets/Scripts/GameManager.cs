using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake() { Instance = this; }

    public bool canPlayersMove = false;
    public float prepTime = 3f;
    public CanvasGroup fadeCanvas; // CanvasGroup do painel preto

    void Start()
    {
        StartCoroutine(StartMatchRoutine());
    }

    private IEnumerator StartMatchRoutine()
    {
        canPlayersMove = false;

        float timer = 0f;
        float blinkSpeed = 1f; // controla a velocidade do piscar

        while (timer < prepTime)
        {
            if (fadeCanvas != null)
            {
                // Faz o alpha oscilar entre 0 e 0.3
                fadeCanvas.alpha = Mathf.PingPong(Time.time * blinkSpeed, 0.3f);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        canPlayersMove = true;

        if (fadeCanvas != null)
        {
            fadeCanvas.alpha = 0f; // volta a tela ao normal
        }
    }
}
