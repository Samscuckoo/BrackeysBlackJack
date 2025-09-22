using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Nome da cena que será carregada
    public string sceneName;

    // Esta função pode ser ligada ao botão no Inspector
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
