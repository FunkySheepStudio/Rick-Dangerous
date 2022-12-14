using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
    public Vector3 direction;
    public bool active = false;
    bool initialForce = false;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (active)
        {
            if (!initialForce)
            {
                rb.velocity = direction;
                //rb.AddForce(direction * 7, ForceMode.VelocityChange);
                initialForce = true;
            }

            rb.velocity = rb.velocity.normalized * 5;
            //active = false;
        }
    }
}
