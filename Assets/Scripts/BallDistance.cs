using UnityEngine;

public class BallDistance : MonoBehaviour
{
    public PlayerMoney playerMoney;

    [Header("Painéis de Upgrade")]
    public GameObject upgradeForcaPanel;
    public GameObject upgradeDinheiroPanel;

    [Header("Configuração do Reset")]
    public float velocidadeParaConsiderarParada = 0.3f;
    public float tempoParada = 0.5f;
    public float tempoAntesDeResetar = 1f;

    private Vector3 posicaoInicial;
    private float maiorDistancia = 0f;

    private Rigidbody rb;
    private BallKick ballKick;

    private bool recebeuDinheiro = false;
    private float tempoComVelocidadeBaixa = 0f;

    // Controla se os painéis já foram escondidos neste chute
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

        // COMEÇA COM OS UPGRADES APARECENDO
        MostrarPaineisUpgrade();
    }

    void Update()
    {
        // =====================================
        // QUANDO A BOLA FOR CHUTADA
        // =====================================

        if (ballKick.Chutada)
        {
            // Esconde os upgrades assim que o chute acontece
            if (!paineisEscondidos)
            {
                EsconderPaineisUpgrade();
                paineisEscondidos = true;
            }
        }
        else
        {
            return;
        }

        // =====================================
        // CALCULA A DISTÂNCIA
        // =====================================

        float distancia = Vector3.Distance(
            new Vector3(posicaoInicial.x, 0, posicaoInicial.z),
            new Vector3(transform.position.x, 0, transform.position.z)
        );

        // Guarda a maior distância
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

        // =====================================
        // VERIFICA SE A BOLA PAROU
        // =====================================

        float velocidade = rb.linearVelocity.magnitude;

        if (velocidade < velocidadeParaConsiderarParada)
        {
            tempoComVelocidadeBaixa += Time.deltaTime;
        }
        else
        {
            tempoComVelocidadeBaixa = 0f;
        }

        // =====================================
        // BOLA PAROU
        // =====================================

        if (!recebeuDinheiro &&
            tempoComVelocidadeBaixa >= tempoParada &&
            maiorDistancia > 1f)
        {
            recebeuDinheiro = true;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // =================================
            // CALCULA O DINHEIRO
            // =================================

            float dinheiroGanho = maiorDistancia;

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
                "m | Dinheiro ganho: R$ " +
                dinheiroGanho.ToString("0")
            );

            // Espera antes de renascer
            Invoke(
                nameof(Resetar),
                tempoAntesDeResetar
            );
        }
    }

    // =====================================
    // RENASCER
    // =====================================

    void Resetar()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Volta a bola para o começo
        ballKick.ResetarBola(posicaoInicial);

        maiorDistancia = 0f;
        recebeuDinheiro = false;
        tempoComVelocidadeBaixa = 0f;

        // Permite esconder novamente no próximo chute
        paineisEscondidos = false;

        // MOSTRA OS UPGRADES NOVAMENTE
        MostrarPaineisUpgrade();
    }

    // =====================================
    // MOSTRAR UPGRADES
    // =====================================

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

    // =====================================
    // ESCONDER UPGRADES
    // =====================================

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