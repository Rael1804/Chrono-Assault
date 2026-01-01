using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu; 
    public GameObject UIGame; 
    public AudioSource ClickButton;
    public AudioSource audioSource; 
    public GameObject buttonRestart;
    public GameObject buttonExit;

    public void GameOverOn() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        audioSource.Play();
        gameOverMenu.SetActive(true);   
        UIGame.SetActive(false);   
        buttonRestart.GetComponent<Button>().interactable = true;
        buttonExit.GetComponent<Button>().interactable = true;
        Time.timeScale = 0f;
    }

    public void RestartGame() {
        StartCoroutine(RestartGameCoroutine());
    }

   private IEnumerator RestartGameCoroutine() {
        ClickButton.Play();
        yield return new WaitForSecondsRealtime(ClickButton.clip.length);
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu() {
        StartCoroutine(ExitToMenuCoroutine());
    }

    private IEnumerator ExitToMenuCoroutine() {
        ClickButton.Play();
        yield return new WaitForSecondsRealtime(ClickButton.clip.length);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
