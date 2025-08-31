using UnityEngine;
using UnityEngine.UI;

public class VictoryCount : MonoBehaviour
{
    private int vitorias = 0;

    public Text textoVitorias;

    void Start()
    {
        AtualizarTexto();
    }
    public void AdicionarVitoria()
    {
        vitorias++;
        AtualizarTexto();
    }

    private void AtualizarTexto()
    {
        if (textoVitorias != null)
        {
            textoVitorias.text = "Vitórias: " + vitorias;
        }
    }

    public void ResetarVitorias()
    {
        vitorias = 0;
        AtualizarTexto();
    }
}