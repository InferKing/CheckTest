using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public bool isInPlace = true;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float y = rb.velocity.y;
        if (rb.velocity != Vector3.zero)
        {
            rb.velocity = new Vector3(0, y, 0);
        }
    }
}
