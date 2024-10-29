using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    public Rigidbody rb;
    public float impulseForce = 3f;
    private bool ignoreNextcollision;

    private void OnCollisionEnter(Collision collision)
    {

        if (ignoreNextcollision)
        {
            return;
        }

        GameManager.singleton.AddScore(1);

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextcollision = true;

        Invoke("AllownextCollision", 0.2F);
    }

    private void AllownextCollision()
    {
        ignoreNextcollision = false;
    }
}
