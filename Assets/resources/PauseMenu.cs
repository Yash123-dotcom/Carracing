using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; 

    }

    public void home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;


    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;



    }
}
