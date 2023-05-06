using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool Blocked;
    public static GameManager SingletonGameManager;
    public static int cannonballSpeed = 40;
    private static int shootsPerGame = 15;
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

    public static int ShootsPerGame
    {
        get
        {
            return shootsPerGame;
        }
        set
        {
            shootsPerGame = value;
        }
    }
}
