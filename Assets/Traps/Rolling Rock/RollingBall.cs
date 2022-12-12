using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
    public Vector3 direction;
    public bool active = false;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (active)
        {
            rb.AddForce(direction * 2, ForceMode.VelocityChange);
            active = false;
        }
    }
}
