using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuSettings;
    public GameObject MenuTutorial;
    private bool state = false;
    private bool closeFirstTime = false;
    public AudioSource clickSound;

    private void Start() {
        Invoke("OpenMenuTutorial", 1f);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleMenu();
        }
    }

    public void ToggleMenu() {
        state = !state;
        if (state == true) {
            OpenMenu();
        } else {
            CloseMenu();
        }
        clickSound.Play();
        MenuSettings.SetActive(state);
    }

    private void OpenMenu() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        Time.timeScale = 0f;
    }

    private void CloseMenu() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        Time.timeScale = 1f;
    }  

    public void TutorialOpen() {
        clickSound.Play();
        MenuTutorial.SetActive(true);
    }

    private void OpenMenuTutorial() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        Time.timeScale = 0f;
        MenuTutorial.SetActive(true);
    }

    private void TutorialClose() {
        clickSound.Play();
        MenuTutorial.SetActive(false);
    }

    private void TutorialClose1Time() {
        clickSound.Play();
        MenuTutorial.SetActive(false);
        CloseMenu();
    }

    public void ChooseCloseTutorial() {
        if (closeFirstTime) {
            TutorialClose();
        } else {
            closeFirstTime = true;
            TutorialClose1Time();
        }
    }

    
}
