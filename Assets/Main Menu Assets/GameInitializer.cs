using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;

    [Header("Gameplay Core")]
    [SerializeField] private GameObject gameplayGroup;

    void Start()
    {
        // When you hit Play in Unity, force the game into "Menu Mode"
        mainMenuCanvas.SetActive(true);
        
        // Hide the active game world and the pause menu completely
        gameplayGroup.SetActive(false);
        pauseMenuCanvas.SetActive(false);
    }

    public void OnPlayButtonPressed()
    {
        mainMenuCanvas.SetActive(false);
        gameplayGroup.SetActive(true);
        pauseMenuCanvas.SetActive(true);
    }

    // Connect this function to your Main Menu's PLAY button
    public void OnLoadButtonPressed()
    {
        // 1. First, wake up the game world so the Player object actually exists in the scene
        mainMenuCanvas.SetActive(false);
        gameplayGroup.SetActive(true);
        pauseMenuCanvas.SetActive(true);

        // 2. Run your load logic now that the Player is active
        LoadSavedData();
    }

    // Your exact script logic placed inside the manager
    private void LoadSavedData() 
    {
        Player player = Object.FindFirstObjectByType<Player>(); 

        if (player != null) 
        {
            PlayerData data = SaveSystem.LoadPlayer();

            Health playerHP = player.GetComponent<Health>(); 
            playerHP.currentHealth = data.health; 

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            player.transform.position = position;
        }
    }

    public void GoToMainMenu()
    {
        // 1. Unpause the game time! (Crucial if your pause menu sets Time.timeScale = 0)
        Time.timeScale = 1f; 

        // 2. Hide the gameplay world and the pause menu
        gameplayGroup.SetActive(false);
        pauseMenuCanvas.SetActive(false);

        // 3. Bring back the Main Menu Canvas
        mainMenuCanvas.SetActive(true);
    }

    public void EXIT()
    {
        Application.Quit();
    }
}