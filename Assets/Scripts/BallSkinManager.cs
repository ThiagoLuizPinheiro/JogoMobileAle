using UnityEngine;

public class BallSkinManager : MonoBehaviour
{
    [Header("Modelos das bolas")]
    public GameObject[] modelos;

    [Header("Modelo atualmente equipado")]
    public int modeloAtual = 0;

    void Start()
    {
        EquiparModelo(modeloAtual);
    }

    public void EquiparModelo(int indice)
    {
        if (modelos == null || modelos.Length == 0)
            return;

        if (indice < 0 || indice >= modelos.Length)
            return;

        for (int i = 0; i < modelos.Length; i++)
        {
            if (modelos[i] != null)
            {
                modelos[i].SetActive(i == indice);
            }
        }

        modeloAtual = indice;
    }
}