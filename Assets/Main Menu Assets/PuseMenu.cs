using UnityEngine;
using UnityEngine.SceneManagement;

public class PuseMenu : MonoBehaviour
{

    [Header("UI Panels")]
    [SerializeField] private GameObject MenuSystems;
    [SerializeField] private GameObject PauseMenu;
    [Header("Gameplay Core")]
    [SerializeField] private GameObject Player_HP_Canvas;
    
    public GameObject container;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeButton()
    {
        container.SetActive(false);
            Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
}
