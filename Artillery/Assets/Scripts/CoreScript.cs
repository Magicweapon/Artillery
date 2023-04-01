using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreScript : MonoBehaviour
{
    public UnityEvent CoreReached;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            CoreReached.Invoke();
        }
    }
}
