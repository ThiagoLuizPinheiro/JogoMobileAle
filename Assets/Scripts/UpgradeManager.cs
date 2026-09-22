using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Referências")]
    public PlayerMoney playerMoney;
    public KickTiming kickTiming;

    [Header("Upgrade de Força")]
    public int nivelForca = 1;
    public float precoForcaInicial = 100f;
    public float aumentoForca = 5f;
    public float aumentoPrecoForca = 1.5f;

    [Header("UI - Força")]
    public TMP_Text nivelForcaText;
    public TMP_Text precoForcaText;

    [Header("Upgrade de Dinheiro")]
    public int nivelMultiplicador = 1;
    public float multiplicadorDinheiro = 1f;
    public float aumentoMultiplicador = 0.5f;
    public float precoMultiplicadorInicial = 150f;
    public float aumentoPrecoMultiplicador = 1.5f;

    [Header("UI - Dinheiro")]
    public TMP_Text nivelMultiplicadorText;
    public TMP_Text precoMultiplicadorText;

    private float precoForcaAtual;
    private float precoMultiplicadorAtual;

    void Start()
    {
        precoForcaAtual = precoForcaInicial;
        precoMultiplicadorAtual = precoMultiplicadorInicial;

        AtualizarUI();
    }

    // =========================
    // COMPRAR FORÇA
    // =========================

    public void ComprarForca()
    {
        if (playerMoney == null || kickTiming == null)
            return;

        if (playerMoney.dinheiro >= precoForcaAtual)
        {
            playerMoney.dinheiro -= precoForcaAtual;

            nivelForca++;

            kickTiming.forcaMaxima += aumentoForca;

            precoForcaAtual *= aumentoPrecoForca;

            AtualizarUI();

            Debug.Log("Upgrade de força comprado!");
        }
        else
        {
            Debug.Log("Dinheiro insuficiente!");
        }
    }

    // =========================
    // COMPRAR MULTIPLICADOR
    // =========================

    public void ComprarMultiplicador()
    {
        if (playerMoney == null)
            return;

        if (playerMoney.dinheiro >= precoMultiplicadorAtual)
        {
            playerMoney.dinheiro -= precoMultiplicadorAtual;

            nivelMultiplicador++;

            multiplicadorDinheiro += aumentoMultiplicador;

            precoMultiplicadorAtual *= aumentoPrecoMultiplicador;

            AtualizarUI();

            Debug.Log(
                "Multiplicador atual: x" +
                multiplicadorDinheiro
            );
        }
        else
        {
            Debug.Log("Dinheiro insuficiente!");
        }
    }

    // =========================
    // ATUALIZAR UI
    // =========================

    void AtualizarUI()
    {
        if (nivelForcaText != null)
        {
            nivelForcaText.text =
                "FORÇA: LV " + nivelForca;
        }

        if (precoForcaText != null)
        {
            precoForcaText.text =
                "R$ " +
                precoForcaAtual.ToString("0");
        }

        if (nivelMultiplicadorText != null)
        {
            nivelMultiplicadorText.text =
                "DINHEIRO: x" +
                multiplicadorDinheiro.ToString("0.0");
        }

        if (precoMultiplicadorText != null)
        {
            precoMultiplicadorText.text =
                "R$ " +
                precoMultiplicadorAtual.ToString("0");
        }
    }
}