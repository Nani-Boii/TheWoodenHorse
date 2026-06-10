using UnityEngine;

public class ShowCanvasOnStart : MonoBehaviour
{
    [SerializeField] private GameObject canvasToEnable;

    void Awake()
    {
        // Awake runs before Start(), ensuring the canvas appears instantly
        if (canvasToEnable != null)
        {
            canvasToEnable.SetActive(true);
        }
    }
}