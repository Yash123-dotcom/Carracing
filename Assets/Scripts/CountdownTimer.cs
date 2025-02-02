using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text countdownText;
    public GameObject car;
    public float countdownTime = 3f;

    public AudioClip countdownBeep;  // Beep sound for "3", "2", "1"
    public AudioClip goSound;         // Sound for "GO!"

    private AudioSource audioSource;

    private void Start()
    {
        car.GetComponent<CarController>().enabled = false;

        // Add or get the AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        for (int i = (int)countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();  // Display "3", "2", "1"

            // Play beep sound
            if (countdownBeep != null)
            {
                audioSource.PlayOneShot(countdownBeep);
            }

            yield return new WaitForSeconds(1f);  // Wait for 1 second
        }

        // Display "GO!"
        countdownText.text = "GO!";

        // Play "GO!" sound
        if (goSound != null)
        {
            audioSource.PlayOneShot(goSound);
        }

        yield return new WaitForSeconds(1f);  // Wait for "GO!" to finish

        countdownText.gameObject.SetActive(false);  // Hide text
        car.GetComponent<CarController>().enabled = true;  // Enable car movement
    }
}
