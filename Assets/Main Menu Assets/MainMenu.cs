using UnityEngine;
public class MainMenu : MonoBehaviour
{

    [Header("UI Panels")]
    [SerializeField] private GameObject MenuSystems;
    [SerializeField] private GameObject PauseMenu;
    [Header("Gameplay Core")]
    [SerializeField] private GameObject Player_HP_Canvas;

    void Start()
    {
        Time.timeScale = 0;
        // When you hit Play in Unity, force the game into "Menu Mode"
        MenuSystems.SetActive(true);
        // Hide the active game world and the pause menu completely
        Player_HP_Canvas.SetActive(false);
     
        PauseMenu.SetActive(false);

    }
    // Connect this function to your Main Menu's PLAY button

    public void OnPlayButtonPressed()

    {
        Time.timeScale = 1f;
        // Hide the main menu
        MenuSystems.SetActive(false);
        // Wake up the player, enemies, level, and background music instantly!
        Player_HP_Canvas.SetActive(true);
        PauseMenu.SetActive(true);
    }

    public void QuitGame()

    {
        Debug.Log("Quit Button Pressed!"); // This lets you know it works in the Editor
        Application.Quit(); // This shuts down the actual built application
    }

}

