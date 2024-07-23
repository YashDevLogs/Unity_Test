using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float timeLimit = 120f; // 2 minutes

    [Header("UI Manager")]
    [SerializeField] private UIManager uiManager; // Reference to the UIManager

    [Header("Player and Gravity Settings")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 initialGravity; // Store the initial gravity settings

    private float timeRemaining;
    private bool isGameOver;

    [SerializeField] private List<CubePickup> allCubes; // List of all cubes in the game
    private int totalCubes;
    private int collectedCubes;

    private void Start()
    {
        // Store the initial gravity settings
        initialGravity = Physics.gravity;

        timeRemaining = timeLimit;
        isGameOver = false;

        // Initialize cubes
        totalCubes = allCubes.Count;
        collectedCubes = 0;

        uiManager.UpdateCubeCount(collectedCubes, totalCubes);

        // Subscribe to events
        EventManager.OnCubeCollected += HandleCubeCollected;
        EventManager.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        EventManager.OnCubeCollected -= HandleCubeCollected;
        EventManager.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
        }
    }

    // Update the timer and check for game over condition
    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            uiManager.UpdateTimer(timeRemaining);
        }
        else
        {
            GameOver();
        }
    }

    // Handle game over condition
    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        uiManager.ShowGameOverPanel();
        EventManager.PlayerDeath();
    }

    // Handle game won condition
    private void GameWon()
    {
        isGameOver = true;
        Debug.Log("You Win!");
        uiManager.ShowGameWonPanel();
        EventManager.AllCubesCollected();
    }

    // Handle cube collected event
    private void HandleCubeCollected()
    {
        collectedCubes++;
        uiManager.UpdateCubeCount(collectedCubes, totalCubes);

        if (collectedCubes >= totalCubes)
        {
            GameWon();
        }
    }

    // Handle player death event
    private void HandlePlayerDeath()
    {
        isGameOver = true;
        uiManager.ShowGameOverPanel();
    }

    // Restart the game by reloading the current scene
    private void RestartGame()
    {
        // Reset the gravity to its initial value
        Physics.gravity = initialGravity;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
