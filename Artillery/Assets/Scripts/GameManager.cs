using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager SingletonGameManager;
    public static int cannonballSpeed;
    public static int shootsPerGame;
    public static float rotationSpeed;
    
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
