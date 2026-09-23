using UnityEngine;

public class BallDistance : MonoBehaviour
{
    public PlayerMoney playerMoney;

    [Header("Painéis de Upgrade")]
    public GameObject upgradeForcaPanel;
    public GameObject upgradeDinheiroPanel;

    [Header("Parada da bola")]
    public float velocidadeParaConsiderarParada = 0.7f;
    public float tempoParada = 0.25f;

    [Header("Camada do chão")]
    public LayerMask camadaDoChao;

    [Header("Multiplicador do Gol")]
    public float multiplicadorGol = 2f;

    private Vector3 posicaoInicial;
    private float maiorDistancia = 0f;

    private Rigidbody rb;
    private BallKick ballKick;

    private bool recebeuDinheiro = false;
    private bool fezGol = false;
    private bool tocouNoChao = false;

    private float tempoComVelocidadeBaixa = 0f;

    private bool paineisEscondidos = false;

    public float MaiorDistancia
    {
        get { return maiorDistancia; }
    }

    public float Recorde
    {
        get { return PlayerPrefs.GetFloat("Recorde", 0f); }
    }

    void Start()
    {
        posicaoInicial = transform.position;

        rb = GetComponent<Rigidbody>();
        ballKick = GetComponent<BallKick>();

        MostrarPaineisUpgrade();
    }

    void Update()
    {
        if (ballKick == null)
            return;

        if (!ballKick.Chutada)
            return;

        // Esconde os upgrades
        if (!paineisEscondidos)
        {
            EsconderPaineisUpgrade();
            paineisEscondidos = true;
        }

        // Calcula distância
        float distancia = Vector3.Distance(
            new Vector3(posicaoInicial.x, 0, posicaoInicial.z),
            new Vector3(transform.position.x, 0, transform.position.z)
        );

        if (distancia > maiorDistancia)
        {
            maiorDistancia = distancia;

            float recordeAtual =
                PlayerPrefs.GetFloat("Recorde", 0f);

            if (maiorDistancia > recordeAtual)
            {
                PlayerPrefs.SetFloat(
                    "Recorde",
                    maiorDistancia
                );

                PlayerPrefs.Save();
            }
        }

        // Se já recebeu dinheiro, não continua
        if (recebeuDinheiro)
            return;

        // Precisa ter tocado no chão
        if (!tocouNoChao)
            return;

        // Usa somente a velocidade linear
        float velocidade =
            rb.linearVelocity.magnitude;

        if (velocidade <= velocidadeParaConsiderarParada)
        {
            tempoComVelocidadeBaixa += Time.deltaTime;
        }
        else
        {
            tempoComVelocidadeBaixa = 0f;
        }

        // Bola realmente parou
        if (tempoComVelocidadeBaixa >= tempoParada)
        {
            FinalizarChute();
        }
    }

    // =========================================================
    // GOL
    // =========================================================

    public void AtivarMultiplicadorGol()
    {
        if (recebeuDinheiro)
            return;

        float distanciaAtual = Vector3.Distance(
            new Vector3(posicaoInicial.x, 0, posicaoInicial.z),
            new Vector3(transform.position.x, 0, transform.position.z)
        );

        if (distanciaAtual > maiorDistancia)
        {
            maiorDistancia = distanciaAtual;
        }

        if (maiorDistancia <= 1f)
            return;

        fezGol = true;

        Debug.Log(
            "⚽ GOL! Aguardando a bola terminar..."
        );
    }

    // =========================================================
    // FINALIZAR
    // =========================================================

    void FinalizarChute()
    {
        if (recebeuDinheiro)
            return;

        recebeuDinheiro = true;

        // Para completamente
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        float dinheiroGanho = maiorDistancia;

        // Gol = dobro
        if (fezGol)
        {
            dinheiroGanho *= multiplicadorGol;
        }

        // Upgrade de dinheiro
        UpgradeManager upgradeManager =
            FindFirstObjectByType<UpgradeManager>();

        if (upgradeManager != null)
        {
            dinheiroGanho *=
                upgradeManager.multiplicadorDinheiro;
        }

        if (playerMoney != null)
        {
            playerMoney.AdicionarDinheiro(
                dinheiroGanho
            );
        }

        Debug.Log(
            "Distância: " +
            maiorDistancia.ToString("0.0") +
            "m | Dinheiro: R$ " +
            dinheiroGanho.ToString("0")
        );

        Resetar();
    }

    // =========================================================
    // DETECTAR CHÃO
    // =========================================================

    private void OnCollisionEnter(Collision collision)
    {
        if (ballKick == null)
            return;

        if (!ballKick.Chutada)
            return;

        // Verifica Layer
        if (((1 << collision.gameObject.layer) & camadaDoChao) != 0)
        {
            tocouNoChao = true;

            Debug.Log("⚽ Tocou no chão!");

            // Zera o contador de parada
            tempoComVelocidadeBaixa = 0f;
        }
    }

    // =========================================================
    // RESET
    // =========================================================

    void Resetar()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        ballKick.ResetarBola(posicaoInicial);

        maiorDistancia = 0f;

        recebeuDinheiro = false;
        fezGol = false;
        tocouNoChao = false;

        tempoComVelocidadeBaixa = 0f;

        paineisEscondidos = false;

        MostrarPaineisUpgrade();
    }

    // =========================================================
    // PAINÉIS
    // =========================================================

    void MostrarPaineisUpgrade()
    {
        if (upgradeForcaPanel != null)
        {
            upgradeForcaPanel.SetActive(true);
        }

        if (upgradeDinheiroPanel != null)
        {
            upgradeDinheiroPanel.SetActive(true);
        }
    }

    void EsconderPaineisUpgrade()
    {
        if (upgradeForcaPanel != null)
        {
            upgradeForcaPanel.SetActive(false);
        }

        if (upgradeDinheiroPanel != null)
        {
            upgradeDinheiroPanel.SetActive(false);
        }
    }
}