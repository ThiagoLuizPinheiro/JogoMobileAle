using UnityEngine;
using UnityEngine.EventSystems;

public class BallPreviewRotate : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Bola")]
    public Transform bola;

    [Header("Velocidade")]
    public float velocidade = 150f;

    private bool girando = false;

    void Update()
    {
        if (girando && bola != null)
        {
            bola.Rotate(
                Vector3.up,
                velocidade * Time.deltaTime,
                Space.World
            );
        }
    }

    // =========================
    // MOUSE
    // =========================

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Mouse entrou na área
        girando = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Mouse saiu da área
        girando = false;
    }

    // =========================
    // CELULAR / TOUCH
    // =========================

    public void OnPointerDown(PointerEventData eventData)
    {
        // Dedo tocou na bola
        girando = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Dedo saiu da bola
        girando = false;
    }
}