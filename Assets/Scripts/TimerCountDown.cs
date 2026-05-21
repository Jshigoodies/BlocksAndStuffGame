using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TimerCountDown : MonoBehaviour
{
    
    public float timeRemaining = 120f; // Set the countdown time in seconds
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component to display the timer
    public string nextSceneName; // Name of the next scene to load when the timer reaches zero

    void Start()
    {
        
    }


    void Update()
    {
        if (timeRemaining > 0)
        {
            // Subtract the time passed since the last frame
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            LoadNextScene();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Formatting the float to look like 00:00
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
