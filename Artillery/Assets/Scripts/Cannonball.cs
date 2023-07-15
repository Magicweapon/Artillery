using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public GameObject explosionParticles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Invoke("Explode", 3);
        }
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Core") Explode();
    }
    public void Explode()
    {
        GameObject particles = Instantiate(explosionParticles, transform.position, Quaternion.identity) as GameObject;
        GameManager.Blocked = false;
        FollowCamera.target = null;
        Destroy(this.gameObject);
    }
}
