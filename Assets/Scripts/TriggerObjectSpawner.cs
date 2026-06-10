using UnityEngine;

public class TriggerObjectSpawner : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField] private GameObject objectToToggle; 

    [Header("Settings")]
    [SerializeField] private bool hideOnExit = true; 

    void Start()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
        }
    }

    // Notice the "2D" added to the function name and parameter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(true);
            }
        }
    }

    // Notice the "2D" added here as well
    private void OnTriggerExit2D(Collider2D other)
    {
        if (hideOnExit && other.CompareTag("Player"))
        {
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(false);
            }
        }
    }
}