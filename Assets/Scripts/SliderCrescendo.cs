using UnityEngine;
using UnityEngine.UI;

public class SliderCrescendo : MonoBehaviour
{
    [Header("Slider")]
    public Slider slider;

    [Header("Tamanho")]
    public float tamanhoMinimo = 0.8f;
    public float tamanhoMaximo = 1.3f;

    [Header("Tremor")]
    [Range(0f, 1f)]
    public float inicioTremor = 0.75f;

    public float intensidadeTremor = 5f;
    public float velocidadeTremor = 25f;

    private RectTransform rectTransform;
    private Vector3 escalaOriginal;
    private Vector3 posicaoOriginal;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        escalaOriginal = rectTransform.localScale;
        posicaoOriginal = rectTransform.localPosition;
    }

    void Update()
    {
        if (slider == null)
            return;

        float valor = slider.value;

        // =========================================
        // CRESCIMENTO DA BARRA INTEIRA
        // =========================================

        float tamanho = Mathf.Lerp(
            tamanhoMinimo,
            tamanhoMaximo,
            valor
        );

        rectTransform.localScale =
            escalaOriginal * tamanho;

        // =========================================
        // TREMOR
        // =========================================

        if (valor >= inicioTremor)
        {
            float intensidade =
                Mathf.Lerp(
                    0f,
                    intensidadeTremor,
                    Mathf.InverseLerp(
                        inicioTremor,
                        1f,
                        valor
                    )
                );

            float tremorX =
                (Mathf.PerlinNoise(
                    Time.time * velocidadeTremor,
                    0f
                ) - 0.5f) * intensidade;

            float tremorY =
                (Mathf.PerlinNoise(
                    0f,
                    Time.time * velocidadeTremor
                ) - 0.5f) * intensidade;

            rectTransform.localPosition =
                posicaoOriginal +
                new Vector3(tremorX, tremorY, 0f);
        }
        else
        {
            rectTransform.localPosition =
                posicaoOriginal;
        }
    }
}