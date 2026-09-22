using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text distanceText;
    public TMP_Text recordText;

    public PlayerMoney playerMoney;
    public BallDistance ballDistance;

    void Update()
    {
        if (playerMoney != null)
        {
            moneyText.text = " $ " + playerMoney.dinheiro.ToString("0");
        }

        if (ballDistance != null)
        {
            distanceText.text = "DISTÂNCIA: " + ballDistance.MaiorDistancia.ToString("0.0") + " m";
            recordText.text = "RECORDE: " + ballDistance.Recorde.ToString("0.0") + " m";
        }
    }
}