using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SettingsUIBehaviour : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI fovDisplay;
    [SerializeField] private Settings settings;
    [SerializeField] private Canvas pauseUI;

    private Canvas settingsUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsUI = GetComponent<Canvas>();
    }

    public void FOVDisplayChange()
    {
        fovDisplay.text = "" + settings.GetFOVValue();
    }

    public void CloseSettings()
    {

        settingsUI.enabled = false;
        pauseUI.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
