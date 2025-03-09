using UnityEngine;
using UnityEngine.InputSystem;

public class OpenAudioSettingsInGame : MonoBehaviour
{
    public GameObject audioSettings;
    InputAction openPauseMenu;
    private bool menuIsOpen = false;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        openPauseMenu = InputSystem.actions.FindAction("OpenPauseMenu");
    }

    public void Update()
    {
        if(player != null && WinnerCondition.instance.stageEnd == false)
        {
            if (openPauseMenu.WasPressedThisFrame())
            {
                ToggleMenu();
            }
        }
    }

    void ToggleMenu()
    {
        menuIsOpen = !menuIsOpen;
        audioSettings.SetActive(menuIsOpen);
        
        if (menuIsOpen)
        {
            CursorEffect.instance.SetMenuState(true);
            Time.timeScale = 0;
        }
        else
        {
            CursorEffect.instance.SetMenuState(false);
            Time.timeScale = 1;
        }
    }
}
