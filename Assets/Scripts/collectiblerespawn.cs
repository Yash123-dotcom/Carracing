using UnityEngine;

public class collectiblerespawn : MonoBehaviour
{
    public float respawnTime = 5f; // Time to respawn the coin
    private Vector3 originalPos;
    private bool isRespawning = false;

    void Start()
    {
        originalPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isRespawning)
        {
            gameObject.SetActive(false);
            isRespawning = true;
            Invoke(nameof(Respawn), respawnTime);
        }
    }

    void Respawn()
    {
        transform.position = originalPos; // Reset to the original position
        gameObject.SetActive(true);
        isRespawning = false; // Allow respawning again
    }
}
