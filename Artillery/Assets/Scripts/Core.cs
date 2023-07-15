using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    public UnityEvent GameWon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explotion")
        {
            Destroy(gameObject);
            GameWon.Invoke();
        }
    }
}
