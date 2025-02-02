using UnityEngine;
using TMPro;

public class Lapmanager : MonoBehaviour
{
    public int totalLaps = 3;  // Set total laps
    private int currentLap = 1; // Start counting from Lap 1
    public TMP_Text lapText;    // Assign a UI Text element to show lap info
    public GameObject finishPanel;  // Assign the finish UI panel
    private GameObject playerCar;  // Store the reference to the player's car

    private bool raceFinished = false;

    private void Start()
    {
        UpdateLapUI();
        finishPanel.SetActive(false); // Hide finish panel at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (raceFinished) return; // Prevent counting after finishing

        if (other.CompareTag("Player"))
        {
            playerCar = other.gameObject;
            if (currentLap < totalLaps)
            {
                currentLap++;
                UpdateLapUI();
            }
            else
            {
                FinishRace();
            }
        }
    }

    private void UpdateLapUI()
    {
        lapText.text = "Lap: " + currentLap + " / " + totalLaps;
    }

    private void FinishRace()
    {
        raceFinished = true;
        lapText.text = "Race Finished!";

        if (finishPanel != null)
        {
            finishPanel.SetActive(true);
        }

        if (playerCar != null)
        {
            playerCar.GetComponent<CarController>().enabled = false;
        }
    }

}
