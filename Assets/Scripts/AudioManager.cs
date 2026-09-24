using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Sons")]
    public AudioClip somClique;
    public AudioClip somCompra;
    public AudioClip somChute;
    public AudioClip somGol;
    public AudioClip somDinheiro;
    public AudioClip somErro;
    public AudioClip somEquipar;
    public AudioClip somAbrirLoja;
    public AudioClip somQuique;

    [Header("Velocidade dos Sons")]
    [Range(0.1f, 3f)]
    public float velocidadeClique = 1f;

    [Range(0.1f, 3f)]
    public float velocidadeCompra = 1f;

    [Range(0.1f, 3f)]
    public float velocidadeChute = 1f;

    [Range(0.1f, 3f)]
    public float velocidadeGol = 1f;

    [Range(0.1f, 3f)]
    public float velocidadeDinheiro = 1f;

    [Range(0.1f, 3f)]
    public float velocidadeErro = 1f;

    [Range(0.1f, 3f)]
    public float velocidadeEquipar = 1f;

    [Range(0.1f, 3f)]
    public float velocidadeAbrirLoja = 1f;

    [Range(0.1f, 3f)]
    public float velocidadeQuique = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TocarClique()
    {
        Tocar(somClique, velocidadeClique);
    }

    public void TocarCompra()
    {
        Tocar(somCompra, velocidadeCompra);
    }

    public void TocarChute()
    {
        Tocar(somChute, velocidadeChute);
    }

    public void TocarGol()
    {
        Tocar(somGol, velocidadeGol);
    }

    public void TocarDinheiro()
    {
        Tocar(somDinheiro, velocidadeDinheiro);
    }

    public void TocarErro()
    {
        Tocar(somErro, velocidadeErro);
    }

    public void TocarEquipar()
    {
        Tocar(somEquipar, velocidadeEquipar);
    }

    public void TocarAbrirLoja()
    {
        Tocar(somAbrirLoja, velocidadeAbrirLoja);
    }

    public void TocarQuique()
    {
        Tocar(somQuique, velocidadeQuique);
    }

    private void Tocar(AudioClip som, float velocidade)
    {
        if (audioSource != null && som != null)
        {
            audioSource.pitch = velocidade;
            audioSource.PlayOneShot(som);
        }
    }
}