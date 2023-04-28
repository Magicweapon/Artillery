using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager SingletonGameManager;
    public static int cannonballSpeed = 40;
    public static int shootsPerGame = 5;
    public static float rotationSpeed = 0.6f;
    
    private void Awake()
    {
        if (SingletonGameManager == null)
        {
            SingletonGameManager = this;
        }
        else
        {
            Debug.LogError("Ya existe una instancia de esta clase");
        }
    }
}
