
using UnityEngine;

public class CarroAutomatico : MonoBehaviour
{
    [Header("Velocidade")]
    public float velocidadeInicial = 2f;
    public float velocidadeMaxima = 15f;
    public float aceleracao = 2f;

    [Header("Percurso")]
    public Transform inicio;
    public Transform fim;

    private float velocidadeAtual;

    void Start()
    {
        velocidadeAtual = velocidadeInicial;

        if (inicio != null)
        {
            transform.position = inicio.position;
        }
    }

    void Update()
    {
        if (inicio == null || fim == null) return;

        // Aumenta a velocidade gradualmente
        velocidadeAtual += aceleracao * Time.deltaTime;
        velocidadeAtual = Mathf.Min(velocidadeAtual, velocidadeMaxima);

        // Anda em direção ao final da rua
        transform.position = Vector3.MoveTowards(
            transform.position,
            fim.position,
            velocidadeAtual * Time.deltaTime
        );

        // Chegou ao final: volta ao início
        if (Vector3.Distance(transform.position, fim.position) < 0.05f)
        {
            transform.position = inicio.position;
            velocidadeAtual = velocidadeInicial;
        }
    }
}
