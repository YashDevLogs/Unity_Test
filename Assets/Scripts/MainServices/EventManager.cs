using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public static event Action OnCubeCollected;
    public static event Action OnAllCubesCollected;

    public static void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void CubeCollected()
    {
        OnCubeCollected?.Invoke();
    }

    public static void AllCubesCollected()
    {
        OnAllCubesCollected?.Invoke();
    }
}
