using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class KickTiming : MonoBehaviour
{
    [Header("UI")]
    public Slider barraForca;

    [Header("Força do chute")]
    public float forcaMinima = 10f;
    public float forcaMaxima = 40f;

    [Header("Timing")]
    public float velocidadeBarra = 1.5f;

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
        // Se a bola já foi chutada, não mexe mais na barra
        if (ballKick != null && ballKick.Chutada)
            return;

        // Movimento da barra
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
            barraForca.value = valor;

        // Aperta E para chutar
        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            Chutar();
        }
    }

    void Chutar()
    {
        float forca = Mathf.Lerp(
            forcaMinima,
            forcaMaxima,
            valor
        );

        ballKick.ChutarComForca(forca);
    }
}