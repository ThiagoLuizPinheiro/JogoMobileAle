using UnityEngine;
using TMPro;

public class BallShop : MonoBehaviour
{
    [System.Serializable]
    public class Skin
    {
        public string nome;
        public GameObject modelo;
        public float preco;
        public bool desbloqueada;
    }

    [Header("Dinheiro")]
    public PlayerMoney playerMoney;

    [Header("Skins")]
    public Skin[] skins;

    [Header("Skin equipada")]
    public int skinEquipada = 0;

    [Header("Texto do botão Beach Ball")]
    public TMP_Text textoBeachBall;

    void Start()
    {
        if (skins != null && skins.Length > 0)
        {
            skins[0].desbloqueada = true;
        }

        AtualizarModelos();
        AtualizarTexto();
    }

    public void ComprarOuEquiparBeachBall()
    {
        int indice = 1;

        if (skins.Length <= indice)
            return;

        // Se ainda não comprou
        if (!skins[indice].desbloqueada)
        {
            if (playerMoney == null)
                return;

            if (playerMoney.dinheiro < skins[indice].preco)
            {
                Debug.Log("Dinheiro insuficiente!");
                return;
            }

            playerMoney.dinheiro -= skins[indice].preco;

            skins[indice].desbloqueada = true;

            Debug.Log("Beach Ball comprada!");
        }

        // Equipa
        skinEquipada = indice;

        AtualizarModelos();
        AtualizarTexto();
    }

    public void EquiparNormal()
    {
        if (skins == null || skins.Length == 0)
            return;

        skinEquipada = 0;

        AtualizarModelos();
        AtualizarTexto();
    }

    void AtualizarModelos()
    {
        if (skins == null)
            return;

        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i].modelo != null)
            {
                skins[i].modelo.SetActive(
                    i == skinEquipada
                );
            }
        }
    }

    void AtualizarTexto()
    {
        if (textoBeachBall == null)
            return;

        if (!skins[1].desbloqueada)
        {
            textoBeachBall.text = "COMPRAR";
        }
        else if (skinEquipada == 1)
        {
            textoBeachBall.text = "EQUIPADA";
        }
        else
        {
            textoBeachBall.text = "EQUIPAR";
        }
    }
}