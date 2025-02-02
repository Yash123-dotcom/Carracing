using UnityEngine;

public class Collectible : MonoBehaviour
{
    private CollectibleManager manager;

    void Start()
    {
        manager = FindObjectOfType<CollectibleManager>(); // Find the manager in the scene
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure only the player can collect
        {
            manager.Collect(gameObject); // Notify the manager to handle collection
        }
    }
}
