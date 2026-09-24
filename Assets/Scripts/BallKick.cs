using UnityEngine;

public class BallKick : MonoBehaviour
{
    [Header("Força")]
    public float forcaFrente = 20f;
    public float forcaCima = 0.4f;

    private Rigidbody rb;
    private bool chutada = false;

    public bool Chutada => chutada;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ChutarComForca(float forca)
    {
        if (chutada)
            return;

        chutada = true;

        // SOM DO CHUTE
        if (AudioManager.instance != null)
        {
            AudioManager.instance.TocarChute();
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        float forcaVertical = forca * forcaCima;

        Vector3 direcao =
            Vector3.forward * forca +
            Vector3.up * forcaVertical;

        rb.AddForce(direcao, ForceMode.Impulse);
    }

    public void ResetarBola(Vector3 posicao)
    {
        transform.position = posicao;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        chutada = false;
    }
}