using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioClip zoneMusic;
    private MusicManager manager;

    void Start()
    {
        manager = Object.FindFirstObjectByType<MusicManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // THIS IS THE TEST: If this doesn't show up in Console, physics are the problem.
        Debug.Log("<color=yellow>Something is touching " + gameObject.name + ": </color>" + other.name);

        if (other.CompareTag("Player"))
        {
            manager.RequestMusic(zoneMusic);
        }
    }
}