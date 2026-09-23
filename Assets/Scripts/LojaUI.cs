using UnityEngine;

public class LojaUI : MonoBehaviour
{
    public GameObject lojaPanel;

    public void AbrirLoja()
    {
        lojaPanel.SetActive(true);
    }

    public void FecharLoja()
    {
        lojaPanel.SetActive(false);
    }
}