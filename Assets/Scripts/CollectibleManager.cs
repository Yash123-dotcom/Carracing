using System.Collections;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public GameObject[] collectibles; // Array of all collectibles in the scene
    public float respawnTime = 5f; // Time before a collectible respawns
    public CoinCounter coinCounter; // Reference to the CoinCounter script

    void Start()
    {
        foreach (GameObject collectible in collectibles)
        {
            collectible.SetActive(true); // Ensure all collectibles are active at the start
        }
    }

    public void Collect(GameObject collectible)
    {
        collectible.SetActive(false); // Disable the collected object
        if (coinCounter != null)
        {
            coinCounter.AddCoin(); // Increment the coin count
        }
        StartCoroutine(RespawnCollectible(collectible)); // Start respawn process
    }

    private IEnumerator RespawnCollectible(GameObject collectible)
    {
        yield return new WaitForSeconds(respawnTime); // Wait for the respawn time
        collectible.SetActive(true); // Reactivate the collectible
    }
}
