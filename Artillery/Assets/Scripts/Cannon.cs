using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject CannonballPrefab;
    private GameObject cannonTip;
    private float rotation;

    private void Start()
    {
        cannonTip = transform.Find("Cannon Tip").gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        rotation += Input.GetAxis("Horizontal") * GameManager.rotationSpeed;

        if (rotation <= 90 && rotation >= 0)
        {
            transform.eulerAngles = new Vector3(rotation, 90.0f, 0.0f);
        }

        if (rotation > 90) rotation = 90;
        if (rotation < 0) rotation = 0;

        if (Input.GetButtonDown("Fire1") && !GameManager.Blocked)
        {
            if (GameManager.ShootsPerGame > 0)
            {
                GameObject cannonball = Instantiate(CannonballPrefab, cannonTip.transform.position, cannonTip.transform.rotation);
                Rigidbody rigidbody = cannonball.GetComponent<Rigidbody>();
                FollowCamera.target = cannonball;
                Vector3 shotDirection = transform.rotation.eulerAngles;
                shotDirection.y = 90 - shotDirection.x;
                rigidbody.velocity = shotDirection.normalized * GameManager.cannonballSpeed;
                GameManager.ShootsPerGame--;
                GameManager.Blocked = true;
            }
        }
    }
}
