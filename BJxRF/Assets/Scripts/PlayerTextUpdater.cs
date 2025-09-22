using UnityEngine;
using TMPro;

public class PlayerTextUpdater : MonoBehaviour
{
    public TMP_Text cardsText; // arraste seu TextMeshPro 3D aqui
    public TMP_Text HealthText;
    public Shoot shootScript;  // arraste o script do player
    public PlayerDamage playerDamageScript; // arraste o script do player

    void Update()
    {
        cardsText.text = shootScript.sum.ToString();
        HealthText.text = playerDamageScript.damagePercent.ToString() + "%";
    }
}
