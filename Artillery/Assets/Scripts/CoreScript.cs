using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreScript : MonoBehaviour
{
    public UnityEvent CoreReached;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explotion")
        {
            Destroy(gameObject);
            CoreReached.Invoke();
        }
    }
}
