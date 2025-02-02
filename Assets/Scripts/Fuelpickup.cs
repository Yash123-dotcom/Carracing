using UnityEngine;
using System.Collections;

public class Fuelpickup : MonoBehaviour
{
    public float fuelAmount = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CarController carFuel= other.GetComponent<CarController>();
            if (carFuel != null)
            {
                carFuel.Refuel(fuelAmount);
                StartCoroutine(RespawnFuel());
            }
        }
    }

    IEnumerator RespawnFuel()
    {
        gameObject.SetActive(false);  // Hide fuel pickup
        yield return new WaitForSeconds(5f);  // Wait for 5 seconds
        gameObject.SetActive(true);  // Show fuel pickup again
    }
}
