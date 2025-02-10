using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenmanager : MonoBehaviour
{
    public Slider progressBar;
    public float fakeLoadingTime = 3f;  // Add extra delay in seconds

    void Start()
    {
        StartCoroutine(LoadSceneWithDelay());
    }

    IEnumerator LoadSceneWithDelay()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");
        operation.allowSceneActivation = false; // Prevent scene from loading immediately

        float elapsedTime = 0f;

        while (!operation.isDone)
        {
            elapsedTime += Time.deltaTime;

            // Simulate progress
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            if (elapsedTime >= fakeLoadingTime && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true; // Now allow scene transition
            }

            yield return null;
        }
    }
}
