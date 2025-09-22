using UnityEngine;

public class Reset : MonoBehaviour
{
    // Função para resetar todos os PlayerPrefs
    public void ResetarPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // garante que a limpeza seja salva imediatamente
        Debug.Log("Todos os PlayerPrefs foram resetados!");
    }
}
