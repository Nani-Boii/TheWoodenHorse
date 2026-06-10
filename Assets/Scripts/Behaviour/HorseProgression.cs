using UnityEngine;
using UnityEngine.SceneManagement; // Required for reloading the game

public class HorseProgression : MonoBehaviour
{
    [System.Serializable]
    public struct HorseStage
    {
        public string stageName;
        public Sprite sprite;
        public Vector2 positionOffset; 
        public Vector3 scale;
    }

    [Header("Progression Data")]
    public int currentStageIndex = 0;
    public HorseStage[] stages;
    private SpriteRenderer spriteRenderer;

    [Header("Victory UI")]
    public GameObject YouWonCanvas;   // Drag your YouWonCanvas here
    public GameObject Player_HP_Canvas; // Drag your gameplay UI here

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RefreshVisuals();
    }

    // Called by the CameraPan script after the pan is complete
    public void AdvanceStage()
    {
        if (stages != null && currentStageIndex < stages.Length - 1)
        {
            currentStageIndex++;
            RefreshVisuals();

            // If the horse has reached the final stage index (e.g., index 3 of a 4-item array)
            if (currentStageIndex == stages.Length - 1)
            {
                TriggerVictory();
            }
        }
    }

    public void RefreshVisuals()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (stages == null || stages.Length == 0) return;

        // Apply the visual data for the current stage
        HorseStage current = stages[Mathf.Clamp(currentStageIndex, 0, stages.Length - 1)];
        spriteRenderer.sprite = current.sprite;
        transform.localScale = current.scale;
        
        // Apply the specific position offset for this stage
        transform.localPosition = new Vector3(current.positionOffset.x, current.positionOffset.y, 0);
    }

    private void TriggerVictory()
    {
        Debug.Log("The horse has fully evolved! You Won!");

        // 1. Show the "You Won" screen
        if (YouWonCanvas != null) YouWonCanvas.SetActive(true);

        // 2. Hide the active gameplay UI
        if (Player_HP_Canvas != null) Player_HP_Canvas.SetActive(false);

        // 3. Freeze the game world so enemies stop moving
        Time.timeScale = 0f; 
    }

    // Link this to the button inside your You Won Canvas
    public void ReturnToMainMenu()
    {
        // Unfreeze time before reloading so the game can run again
        Time.timeScale = 1f;

        // Reset index for the next run
        currentStageIndex = 0;

        // Reload the scene completely fresh
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}