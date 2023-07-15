using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject CannonballPrefab;
    private GameObject cannonTip;
    public GameObject ShotParticles;
    public Slider forceSlider;
    private float rotation;

    private GameObject shotSound;
    private AudioSource shotSource;

    public CannonControls cannonControls;

    private InputAction aim;
    private InputAction shoot;
    private InputAction modifyForce;

    private void Awake()
    {
        cannonControls = new CannonControls();
    }
    private void Start()
    {
        cannonTip = transform.Find("Cannon Tip").gameObject;
        shotSound = GameObject.Find("Gunshot Sound");
        shotSource = shotSound.GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        aim = cannonControls.Cannon.Aim;
        shoot = cannonControls.Cannon.Shoot;
        modifyForce = cannonControls.Cannon.ModifyForce;

        aim.Enable();
        shoot.Enable();
        modifyForce.Enable();

        shoot.performed += Shoot;
    }
    void Update()
    {
        if (GameManager.Blocked) return;

        rotation += aim.ReadValue<float>() * GameManager.rotationSpeed;

        if (rotation <= 90 && rotation >= 0)
        {
            transform.eulerAngles = new Vector3(rotation, 90.0f, 0.0f);
        }

        if (rotation > 90) rotation = 90;
        if (rotation < 0) rotation = 0;

        GameManager.cannonballSpeed += modifyForce.ReadValue<float>() * 0.1f;

        if (GameManager.cannonballSpeed > 50) GameManager.cannonballSpeed = 50;
        if (GameManager.cannonballSpeed < 20) GameManager.cannonballSpeed = 20;

        forceSlider.value = GameManager.cannonballSpeed;
    }
    private void Shoot(InputAction.CallbackContext context)
    {
        if (GameManager.ShootsPerGame <= 0 || GameManager.Blocked) return;

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
        Debug.Log(GameManager.cannonballSpeed);
    }
}