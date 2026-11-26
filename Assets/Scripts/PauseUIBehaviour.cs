using UnityEngine;
using UnityEngine.InputSystem;

public class PauseUIBehaviour : MonoBehaviour
{
    [SerializeField] private InputActionAsset InputActions;
    [SerializeField] private Canvas settingsUI;
    [SerializeField] private Canvas mainUI;
    [SerializeField] private Canvas interactUI;
    private bool gamePaused = false;
    private float pauseCooldown = 0;
    private bool pressed = false;
    private bool onSettings = false;
    private InputAction pauseAction;
    private Canvas pauseCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pauseCanvas = gameObject.GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        pauseAction = InputActions.FindActionMap("Player").FindAction("PauseGame");
    }

    private void OpenPauseUI()
    {
        pauseCanvas.enabled = true;
        mainUI.enabled = false;
        interactUI.enabled = false;

        gamePaused = true;
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ClosePauseUI(string btnType)
    {

        pauseCanvas.enabled = false;

        if (btnType == "Resume")
        {
            mainUI.enabled = true;
            interactUI.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gamePaused = false;
            Time.timeScale = 1;
        }
        else if (btnType == "Settings")
        {
            pauseCanvas.enabled = false;
            settingsUI.enabled = true;
            onSettings = true;
        }
        else if (btnType == "Quit")
        {
            // Voltar menu principal
        }
        

    }

    public void DisableSettings()
    {
        onSettings = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pressed && !onSettings)
        {
            if (pauseAction.WasPressedThisFrame() && gamePaused == false)
            {
                pressed = true;
                gamePaused = true;
                pauseCooldown = 0;
                OpenPauseUI();

            }
            else if (pauseAction.WasPressedThisFrame() && gamePaused == true)
            {
                pressed = true;
                gamePaused = false;
                pauseCooldown = 0;
                ClosePauseUI("Resume");

            }
            
        }
        else
        {
            pressed = false;
            pauseCooldown = 0;
        }


        pauseCooldown += Time.deltaTime;
    }
}
