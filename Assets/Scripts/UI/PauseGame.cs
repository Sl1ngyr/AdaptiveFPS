using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class PauseGame : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private GameObject pauseGameUI;
        
        private bool isGamePaused = false;
        private PlayerInputActions playerInputActions;

        private float time = 3;
        private int repeatTime = 3;
        
        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
        }

        private void OnEnable()
        {
            resumeButton.onClick.AddListener(ButtonResumeGame);
            
            playerInputActions.Player.Pause.performed += Pause;
            playerInputActions.Menu.Resume.performed += ResumeGame;
        }

        private void OnDisable()
        {
            resumeButton.onClick.RemoveListener(ButtonResumeGame);
            
            playerInputActions.Player.Pause.performed -= Pause;
            playerInputActions.Menu.Resume.performed -= ResumeGame;
        }

        private void ButtonResumeGame()
        {
            if (isGamePaused)
            {
                Time.timeScale = 1;
                pauseGameUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                isGamePaused = false;
                
                playerInputActions.Menu.Disable();
                playerInputActions.Player.Enable();
            }
        }
        
        private void Pause(InputAction.CallbackContext obj)
        {
            if (isGamePaused == false)
            {
                pauseGameUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                isGamePaused = true;
                
                playerInputActions.Menu.Enable();
                playerInputActions.Player.Disable();
            }
        }

        private void ResumeGame(InputAction.CallbackContext obj)
        {
            if (isGamePaused)
            {
                Time.timeScale = 1;
                pauseGameUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                isGamePaused = false;
                
                playerInputActions.Menu.Disable();
                playerInputActions.Player.Enable();
            }
        }
    }
}