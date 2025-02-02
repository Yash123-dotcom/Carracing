using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 20f;
    public float steeringSpeed = 150f;
    public float drag = 0.9f;

    public float maxFuel = 100f;
    private float currentFuel;
    public float fuelConsumptionRate = 5f; // Fuel lost per second
    public Slider fuelBar;
    public TMP_Text fuelText;

    public GameObject gameOverPanel; // Assign in Unity Inspector

    private Rigidbody2D rb;
    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        currentFuel = maxFuel;
        UpdateFuelUI();

        gameOverPanel.SetActive(false); // Hide game over panel at start
    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            if (currentFuel > 0)
            {
                float moveInput = Input.GetAxis("Vertical");
                float steerInput = Input.GetAxis("Horizontal");

                MoveCar(moveInput);
                SteerCar(steerInput);
                ApplyDrag();
                ConsumeFuel(moveInput);
            }
            else
            {
                GameOver();
            }
        }
    }

    void MoveCar(float moveInput)
    {
        if (moveInput != 0)
        {
            rb.AddForce(transform.up * moveInput * acceleration);
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void SteerCar(float steerInput)
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            float rotationAmount = -steerInput * steeringSpeed * Time.fixedDeltaTime;
            rb.rotation += rotationAmount;
        }
    }

    void ApplyDrag()
    {
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity *= drag;
        }
    }

    void ConsumeFuel(float moveInput)
    {
        if (moveInput != 0)
        {
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
            UpdateFuelUI();
        }
    }

    void UpdateFuelUI()
    {
        fuelBar.value = currentFuel / maxFuel;
        fuelText.text = "Fuel: " + currentFuel.ToString("0") + " / " + maxFuel.ToString("0");
    }

    void GameOver()
    {
        isGameOver = true;
        rb.velocity = Vector2.zero;
        this.enabled = false; // Disable player movement
        gameOverPanel.SetActive(true); // Show Game Over UI
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart level
    }

    public void Refuel(float amount)
    {
        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
        UpdateFuelUI();
    }

    public void exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
