using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HandUI : MonoBehaviour
{
    public Transform maoContainer;           // painel para as cartas
    public GameObject cartaPrefab;           // prefab com Image para carta
    public Transform numeroContainer;        // painel para os dígitos
    public GameObject numeroPrefab;          // prefab com Image para número
    public Sprite[] spritesNumeros;          // 0–9

    public void AtualizarMao(List<int> mao, Sprite[] spritesCartas)
    {
        // limpa cartas antigas
        foreach (Transform child in maoContainer) Destroy(child.gameObject);

        for (int i = 0; i < mao.Count; i++)
        {
            GameObject c = Instantiate(cartaPrefab, maoContainer);
            c.GetComponent<Image>().sprite = spritesCartas[mao[i] - 1];
        }
    }

    public void MostrarValor(int total)
    {
        foreach (Transform child in numeroContainer) Destroy(child.gameObject);

        string t = total.ToString();
        foreach (char c in t)
        {
            int digito = c - '0';
            GameObject d = Instantiate(numeroPrefab, numeroContainer);
            d.GetComponent<Image>().sprite = spritesNumeros[digito];
        }
    }
}
