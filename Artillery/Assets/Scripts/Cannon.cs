using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
/**
 * This class deals with the behaviour of the cannon object via code in
 * order for the player to play the game. 
 * 
 * It contains
 * CannonballPrefab  the cannonball prefab to use it as a projectile
 * cannonTip         the child object of the cannon whose coordinates are used for spawning the cannonballs
 * rotation          private variable to control the rotation of the cannon with the player's input
 * shotSound         object used to play a shot audio clip
 * shotSource        reference to the shot audio clip used by shotSound object
 * cannonControls    reference to the CannonControls input action map to deal with the player's input
 * aim               input action variable used in conjunction with "rotation" variable to deal with the cannon's rotation
 * shoot             input action variable used to trigger cannon shot with specific player's input
 * modifyForce       input action variable to be able to increase or decrease the force used in the next shot
 * 
*/
public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject CannonballPrefab;
    private GameObject cannonTip;

    /**
     * A reference to the particle system object of the cannonball shot.
    */
    public GameObject ShotParticles;
    /**
     * Used to indicate the player the speed the cannonball is going to start with when shot.
    */
    public Slider forceSlider;
    private float rotation;

    private GameObject shotSound;
    private AudioSource shotSource;

    /**
     * Reference to the CannonControls input action map to deal with the player's input.
    */
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
        modifyForce.performed += Test;
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
    /**
     * A function that shoots the cannonball from the cannon's tip with the corresponding direction and speed.
     * It also sets the camera target to be the shot cannonball, creates the particles for the visual effects
     * and plays the shot audio clip.
    */
    public void Shoot(InputAction.CallbackContext context)
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
    }

    private void Test(InputAction.CallbackContext context)
    {

    }
}