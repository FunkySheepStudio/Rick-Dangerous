using UnityEngine;

public class TriggerStart : MonoBehaviour
{
    public RollingBall rollingBall;

    private void OnTriggerEnter(Collider other)
    {
        rollingBall.active = true;
    }
}
