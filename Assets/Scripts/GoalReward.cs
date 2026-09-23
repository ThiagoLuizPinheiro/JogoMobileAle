using UnityEngine;

public class GoalReward : MonoBehaviour
{
    [Header("Multiplicador do gol")]
    public float multiplicador = 2f;

    private void OnTriggerEnter(Collider other)
    {
        BallDistance ballDistance =
            other.GetComponent<BallDistance>();

        if (ballDistance != null)
        {
            ballDistance.AtivarMultiplicadorGol();

            Debug.Log("? GOL! Dinheiro em dobro!");
        }
    }
}