using UnityEngine;
using UnityEngine.UI;

public class KickTiming : MonoBehaviour
{
    [Header("UI")]
    public Slider barraForca;

    [Header("Força do chute")]
    public float forcaMinima = 10f;
    public float forcaMaxima = 40f;

    [Header("Velocidade da barra")]
    public float velocidadeBarra = 3f;

    private float valor = 0f;
    private int direcao = 1;

    private BallKick ballKick;

    void Start()
    {
        ballKick = GetComponent<BallKick>();

        if (barraForca != null)
        {
            barraForca.minValue = 0f;
            barraForca.maxValue = 1f;
            barraForca.value = 0f;
        }
    }

    void Update()
    {
        if (ballKick != null && ballKick.Chutada)
            return;

        // Movimento automático da barra
        valor += direcao * velocidadeBarra * Time.deltaTime;

        if (valor >= 1f)
        {
            valor = 1f;
            direcao = -1;
        }

        if (valor <= 0f)
        {
            valor = 0f;
            direcao = 1;
        }

        if (barraForca != null)
        {
            barraForca.value = valor;
        }
    }

    // Esse método será chamado quando clicar na barra
    public void ChutarPeloSlider()
    {
        if (ballKick == null)
            return;

        if (ballKick.Chutada)
            return;

        float forca = Mathf.Lerp(
            forcaMinima,
            forcaMaxima,
            valor
        );

        ballKick.ChutarComForca(forca);
    }
}