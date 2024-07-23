using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Cube Count Settings")]
    [SerializeField] private TextMeshProUGUI cubeCountText;

    [Header("Game Panels")]
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private GameObject gameOverPanel;

    public void UpdateTimer(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateCubeCount(int collectedCubes, int totalCubes)
    {
        cubeCountText.text = string.Format("{0}/{1}", collectedCubes, totalCubes);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowGameWonPanel()
    {
        gameWonPanel.SetActive(true);
    }
}
