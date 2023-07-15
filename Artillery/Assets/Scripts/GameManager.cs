using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool Blocked;
    public static GameManager SingletonGameManager;
    public static float cannonballSpeed = 40f;
    private static int shootsPerGame = 15;
    public static float rotationSpeed = 0.25f;

    public GameObject WinCanvas;
    public GameObject LoseCanvas;
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
    private void Update()
    {
        if (shootsPerGame < 0) LoseGame();
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
    public void WinGame()
    {
        WinCanvas.SetActive(true);
    }
    public void LoseGame()
    {
        LoseCanvas.SetActive(true);
    }
}
