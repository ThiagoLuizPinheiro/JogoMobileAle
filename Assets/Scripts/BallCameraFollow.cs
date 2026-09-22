using UnityEngine;

public class BallCameraFollow : MonoBehaviour
{
    public Transform bola;

    public Vector3 offset = new Vector3(0f, 4f, -8f);

    void LateUpdate()
    {
        if (bola == null)
            return;

        transform.position = bola.position + offset;

        transform.LookAt(bola);
    }
}