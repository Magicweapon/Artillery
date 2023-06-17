using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject CannonballPrefab;
    private GameObject cannonTip;
    public GameObject ShotParticles;
    private float rotation;

    private GameObject shotSound;
    private AudioSource shotSource;

    private void Start()
    {
        cannonTip = transform.Find("Cannon Tip").gameObject;
        shotSound = GameObject.Find("Gunshot Sound");
        shotSource = shotSound.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Blocked) return;

        rotation += Input.GetAxis("Horizontal") * GameManager.rotationSpeed;

        if (rotation <= 90 && rotation >= 0)
        {
            transform.eulerAngles = new Vector3(rotation, 90.0f, 0.0f);
        }

        if (rotation > 90) rotation = 90;
        if (rotation < 0) rotation = 0;

        if (Input.GetButtonDown("Fire1"))
        {
            if (GameManager.ShootsPerGame > 0)
            {
                GameObject cannonball = Instantiate(CannonballPrefab, cannonTip.transform.position, cannonTip.transform.rotation);
                Rigidbody rigidbody = cannonball.GetComponent<Rigidbody>();
                FollowCamera.target = cannonball;
                Vector3 shotDirection = transform.rotation.eulerAngles;
                shotDirection.y = 90 - shotDirection.x;
                Vector3 particlesDirection = new Vector3(-90 + shotDirection.x, 90, 0);
                GameObject particles = Instantiate(ShotParticles, cannonTip.transform.position, Quaternion.Euler(particlesDirection), transform);
                rigidbody.velocity = shotDirection.normalized * GameManager.cannonballSpeed;
                GameManager.ShootsPerGame--;
                shotSource.Play();
                GameManager.Blocked = true;
            }
        }
    }
}
