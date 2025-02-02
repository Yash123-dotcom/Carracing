using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Reference to the TextMeshPro UI component
    private int coinCount = 0; // Initial coin count
    public AudioSource audiosource;
    public AudioClip coinsound;

    public void AddCoin()
    {
        coinCount++; // Increment the coin count
        UpdateCoinText(); // Update the displayed text
        playcoinsound();
    }

    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + coinCount.ToString(); // Update the text
    }

    private void playcoinsound()
    {
        if(audiosource!=null && coinsound != null)
        {
            audiosource.PlayOneShot(coinsound);
        }
    }
}
