using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cenouras : MonoBehaviour
{
    [Header("Indicadores da Fase 1")]
    public Image[] fase1Coletaveis; // 3 imagens no UI
    [Header("Indicadores da Fase 2")]
    public Image[] fase2Coletaveis;
    [Header("Indicadores da Fase 3")]
    public Image[] fase3Coletaveis;

    void Start()
    {
        AtualizarUI();
    }

    void AtualizarUI()
    {
        // Fase 1
        string[] chavesFase1 = { "Fase1_Cenoura1", "Fase1_Cenoura2", "Fase1_Cenoura3" };
        AtualizarFase(fase1Coletaveis, chavesFase1);

        // Fase 2
        string[] chavesFase2 = { "Fase2_Cenoura1", "Fase2_Cenoura2", "Fase2_Cenoura3" };
        AtualizarFase(fase2Coletaveis, chavesFase2);

        // Fase 3
        string[] chavesFase3 = { "Fase3_Cenoura1", "Fase3_Cenoura2", "Fase3_Cenoura3" };
        AtualizarFase(fase3Coletaveis, chavesFase3);
    }

    void AtualizarFase(Image[] imagens, string[] chaves)
    {
        for (int i = 0; i < imagens.Length && i < chaves.Length; i++)
        {
            if (PlayerPrefs.GetInt(chaves[i], 0) == 1)
                imagens[i].gameObject.SetActive(true);
            else
                imagens[i].gameObject.SetActive(false);
        }
    }

    // Funções para jogar cada fase
    public void PlayFase1()
    {
        SceneManager.LoadScene("Nv 1"); // nome da cena da fase 1
    }

    public void PlayFase2()
    {
        if (TodasColetadas("Fase1_Cenoura"))
            SceneManager.LoadScene("Nv 2"); // nome da cena da fase 2
        else
            Debug.Log("Você precisa coletar todas as cenouras da Fase 1!");
    }

    public void PlayFase3()
    {
        if (TodasColetadas("Fase1_Cenoura") && TodasColetadas("Fase2_Cenoura"))
            SceneManager.LoadScene("Nv 3"); // nome da cena da fase 3
        else
            Debug.Log("Você precisa coletar todas as cenouras da Fase 1 e Fase 2!");
    }

    // Função auxiliar que verifica se todas as 3 cenouras de uma fase foram coletadas
    bool TodasColetadas(string prefixo)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt(prefixo + i, 0) != 1)
                return false;
        }
        return true;
    }
}
