using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public bool IsGrounded = false;

    private void OnTriggerEnter(Collider other)
    {
        IsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsGrounded = true;
    }

    private void OnTriggerStay(Collider other)
    {
        IsGrounded = false;
    }
}
