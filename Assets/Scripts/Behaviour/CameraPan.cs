using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
    [Header("Cinematic Timing")]
    public float panInDuration = 1.2f;   
    public float preUpdateDelay = 0.7f;  
    public float stopDuration = 2.0f;    
    public float panOutDuration = 1.5f;  

    [Header("Positioning")]
    [Tooltip("Increase this to move the camera higher when looking at the horse")]
    public float verticalOffsetAtHorse = 2.5f; 

    private CameraFollow cameraFollowScript; 
    private Coroutine currentPanCoroutine;

    private void Awake()
    {
        // Automatically links to the CameraFollow component on the Main Camera[cite: 1, 2]
        cameraFollowScript = GetComponent<CameraFollow>();
    }

    public void StartPanSequence(Transform horseTransform, HorseProgression horseScript)
    {
        if (currentPanCoroutine != null) StopCoroutine(currentPanCoroutine);
        currentPanCoroutine = StartCoroutine(PanSequence(horseTransform, horseScript));
    }

    private IEnumerator PanSequence(Transform horseTransform, HorseProgression horseScript)
    {
        Player player = Object.FindAnyObjectByType<Player>();
        if (player == null) yield break;

        // 1. Disable player-follow and freeze Jerick[cite: 1, 2]
        if (cameraFollowScript != null) cameraFollowScript.enabled = false;
        player.SetCinematicMode(true);

        // 2. Pan to Horse (Applying the Vertical Offset)[cite: 1]
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(horseTransform.position.x, horseTransform.position.y + verticalOffsetAtHorse, startPos.z);

        float elapsed = 0;
        while (elapsed < panInDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / panInDuration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        transform.position = endPos;

        // 3. Anticipation Pause[cite: 1]
        yield return new WaitForSeconds(preUpdateDelay);

        // 4. Visual Update (Swap sprite while camera is focused)[cite: 1]
        horseScript.AdvanceStage();

        // 5. Appreciation Pause[cite: 1]
        yield return new WaitForSeconds(stopDuration);

        // 6. Pan back to Player[cite: 1, 2]
        elapsed = 0;
        Vector3 returnStart = transform.position;
        while (elapsed < panOutDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / panOutDuration);
            Vector3 currentPlayerPos = new Vector3(player.transform.position.x, player.transform.position.y, startPos.z);
            transform.position = Vector3.Lerp(returnStart, currentPlayerPos, t);
            yield return null;
        }

        // 7. Resume Gameplay[cite: 1, 2]
        if (cameraFollowScript != null) cameraFollowScript.enabled = true;
        player.SetCinematicMode(false);
        currentPanCoroutine = null;
    }
}