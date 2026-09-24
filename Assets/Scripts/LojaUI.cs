using UnityEngine;

public class LojaUI : MonoBehaviour
{
    public GameObject lojaPanel;

    public void AbrirLoja()
    {
        lojaPanel.SetActive(true);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.TocarAbrirLoja();
        }
    }

    public void FecharLoja()
    {
        lojaPanel.SetActive(false);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.TocarClique();
        }
    }
}