using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public float dinheiro = 0f;

    public void AdicionarDinheiro(float valor)
    {
        dinheiro += valor;

        Debug.Log("Dinheiro: R$ " + dinheiro);
    }
}